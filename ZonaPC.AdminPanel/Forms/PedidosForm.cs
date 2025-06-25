using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using TiendaComponentes.Shared.DTO;
using TiendaComponentes.Shared.DTOs;
using ClosedXML.Excel;
using System.IO;


namespace ZonaPC.WinForms
{
    public partial class PedidosForm : Form
    {
        private readonly string _token;
        private List<PedidoConDetallesDTO> _pedidos = new();
        private Dictionary<int, EstadoPedido> _estadosOriginales = new();
        private BindingSource bindingSource = new();
        private FlowLayoutPanel panelImagenes;
        private Label lblCargando;
        private CancellationTokenSource _imgCts;

        public PedidosForm(string token)
        {
            InitializeComponent();
            _token = token;

            this.Load += PedidosForm_Load;
            dgvPedidos.SelectionChanged += DgvPedidos_SelectionChanged;
            dgvDetalles.SelectionChanged += async (s, e) => await DgvDetalles_SelectionChangedAsync();
            dgvPedidos.RowPrePaint += DgvPedidos_RowPrePaint;
            dgvPedidos.AllowUserToDeleteRows = false;


            dgvPedidos.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvPedidos.IsCurrentCellDirty)
                    dgvPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            dgvPedidos.DataError += (s, e) =>
            {
                MessageBox.Show("Error al mostrar el estado. Posible valor inválido para el ComboBox.");
                e.Cancel = true;
            };

            btnActualizarEstados.Click += BtnActualizarEstados_Click;
            btnRefrescar.Click += BtnRefrescar_Click;


            panelImagenes = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 220,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.White
            };
            Controls.Add(panelImagenes);

            lblCargando = new Label
            {
                Text = "Cargando imágenes...",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.Gray,
                Dock = DockStyle.Bottom,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            Controls.Add(lblCargando);

            btnBuscar.Click += BtnBuscar_Click;
            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnBuscar_Click(s, e);
                    e.SuppressKeyPress = true;
                }
            };

        }

        private async void PedidosForm_Load(object sender, EventArgs e)
        {
            try
            {
                using var client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7144/api/")
                };
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);

                var pedidos = await client.GetFromJsonAsync<List<PedidoConDetallesDTO>>("pedidos/admin");

                if (pedidos == null || pedidos.Count == 0)
                {
                    MessageBox.Show("No se encontraron pedidos.");
                    return;
                }

                _pedidos = pedidos;
                _estadosOriginales = _pedidos.ToDictionary(p => p.Id, p => p.Estado);

                dgvPedidos.AutoGenerateColumns = false;
                dgvPedidos.Columns.Clear();

                // 👇 Columnas individuales con ReadOnly = true
                var colId = new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", ReadOnly = true };
                var colUsuarioId = new DataGridViewTextBoxColumn { HeaderText = "ID Usuario", DataPropertyName = "UsuarioId", ReadOnly = true };
                var colFecha = new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "Fecha", ReadOnly = true };
                var colUsuario = new DataGridViewTextBoxColumn { HeaderText = "Usuario", DataPropertyName = "NombreUsuario", ReadOnly = true };
                var colProvincia = new DataGridViewTextBoxColumn { HeaderText = "Provincia", DataPropertyName = "Provincia", ReadOnly = true };
                var colLocalidad = new DataGridViewTextBoxColumn { HeaderText = "Localidad", DataPropertyName = "Localidad", ReadOnly = true };
                var colTotal = new DataGridViewTextBoxColumn
                {
                    HeaderText = "Total con Envio",
                    DataPropertyName = "Total",
                    ReadOnly = true,
                    DefaultCellStyle = { Format = "C2" }
                };

                var estadoCol = new DataGridViewComboBoxColumn
                {
                    Name = "colEstado",
                    HeaderText = "Estado",
                    DataPropertyName = "Estado",
                    ValueType = typeof(EstadoPedido),
                    DataSource = Enum.GetValues(typeof(EstadoPedido)),
                    ReadOnly = false 
                };

                var colEnvio = new DataGridViewTextBoxColumn
                {
                    HeaderText = "Envío",
                    DataPropertyName = "CostoEnvio",
                    ReadOnly = true,
                    DefaultCellStyle = { Format = "C2" }
                };


                dgvPedidos.Columns.AddRange(
                    colId,
                    colUsuarioId,
                    colFecha,
                    colUsuario,
                    colProvincia,
                    colLocalidad,
                    colEnvio,
                    colTotal,
                    estadoCol
                );


                bindingSource.DataSource = _pedidos;
                dgvPedidos.DataSource = bindingSource;
                dgvPedidos.EditMode = DataGridViewEditMode.EditOnEnter;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pedidos:\n" + ex.Message);
            }
        }



        private async Task DgvDetalles_SelectionChangedAsync()
        {
            _imgCts?.Cancel();
            _imgCts = new CancellationTokenSource();
            var token = _imgCts.Token;

            panelImagenes.Controls.Clear();
            lblCargando.Visible = true;

            if (dgvDetalles.CurrentRow?.DataBoundItem is not DetallePedidoDTO detalle)
            {
                lblCargando.Visible = false;
                return;
            }

            if (detalle.ImagenesUrl == null || detalle.ImagenesUrl.Count == 0)
            {
                lblCargando.Visible = false;
                return;
            }

            try
            {
                using var client = new HttpClient();

                foreach (var url in detalle.ImagenesUrl)
                {
                    try
                    {
                        var stream = await client.GetStreamAsync(url, token);
                        var img = Image.FromStream(stream);

                        var pic = new PictureBox
                        {
                            Width = 200,
                            Height = 200,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Margin = new Padding(5),
                            Image = img
                        };

                        panelImagenes.Controls.Add(pic);
                    }
                    catch
                    {
                        // ignorar error individual
                    }
                }
            }
            catch
            {
                // global catch, sin bloquear
            }
            finally
            {
                lblCargando.Visible = false;
            }
        }

        private async void BtnActualizarEstados_Click(object sender, EventArgs e)
        {
            dgvPedidos.EndEdit();
            bindingSource.EndEdit();

            var pedidosEditados = _pedidos
                .Where(p => _estadosOriginales.ContainsKey(p.Id)
                            && _estadosOriginales[p.Id].ToString() != p.Estado.ToString())
                .ToList();

            MessageBox.Show($"Detectados cambios en {pedidosEditados.Count} pedidos.");

            if (pedidosEditados.Count == 0)
                return;

            using var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7144/api/")
            };
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token);

            int exitosos = 0;
            foreach (var pedido in pedidosEditados)
            {
                var estadoTexto = pedido.Estado.ToString();
                var content = new StringContent($"\"{estadoTexto}\"", System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"pedidos/{pedido.Id}/estado", content);
                if (response.IsSuccessStatusCode)
                {
                    exitosos++;
                    _estadosOriginales[pedido.Id] = pedido.Estado;
                }
            }

            MessageBox.Show($"Se actualizaron {exitosos} estados correctamente.");
        }

        private void DgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoConDetallesDTO pedido)
                return;

            dgvDetalles.ReadOnly = true;
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.Columns.Clear();

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "NombreProducto" });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cantidad", DataPropertyName = "Cantidad" });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio Unitario", DataPropertyName = "PrecioUnitario", DefaultCellStyle = { Format = "C2" } });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "Subtotal", DefaultCellStyle = { Format = "C2" } });

            dgvDetalles.DataSource = pedido.Detalles;
        }

        private void DgvPedidos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvPedidos.Rows[e.RowIndex].DataBoundItem is not PedidoConDetallesDTO pedido)
                return;

            var row = dgvPedidos.Rows[e.RowIndex];
            var color = pedido.Estado switch
            {
                EstadoPedido.Pendiente => Color.LightYellow,
                EstadoPedido.EnCamino => Color.LightBlue,
                EstadoPedido.Entregado => Color.LightGreen,
                EstadoPedido.Cancelado => Color.LightCoral,
                _ => Color.White
            };

            // Aplicar a toda la fila (excepto combo)
            row.DefaultCellStyle.BackColor = color;

            // Aplicar tambien a la celda de estado manualmente
            if (row.Cells["colEstado"] is DataGridViewCell estadoCell)
                estadoCell.Style.BackColor = color;
        }

        private async void btnEliminarCance_Click(object sender, EventArgs e)
        {
            var cancelados = _pedidos.Where(p => p.Estado == EstadoPedido.Cancelado).ToList();

            if (!cancelados.Any())
            {
                MessageBox.Show("No hay pedidos cancelados para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"Se encontraron {cancelados.Count} pedidos cancelados.\n¿Desea eliminarlos definitivamente?",
                                          "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            using var client = new HttpClient { BaseAddress = new Uri("https://localhost:7144/api/") };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            int eliminados = 0;

            foreach (var pedido in cancelados)
            {
                try
                {
                    var response = await client.DeleteAsync($"pedidos/{pedido.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        _pedidos.Remove(pedido);
                        eliminados++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar pedido {pedido.Id}:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Refrescar grilla
            bindingSource.DataSource = null;
            bindingSource.DataSource = _pedidos;

            MessageBox.Show($"Se eliminaron {eliminados} pedidos cancelados.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "Archivo Excel (*.xlsx)|*.xlsx",
                FileName = $"Pedidos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using var workbook = new XLWorkbook();
                var ws = workbook.Worksheets.Add("Pedidos");

                // Encabezados
                ws.Cell(1, 1).Value = "ID";
                ws.Cell(1, 2).Value = "Usuario";
                ws.Cell(1, 3).Value = "Fecha";
                ws.Cell(1, 4).Value = "Provincia";
                ws.Cell(1, 5).Value = "Localidad";
                ws.Cell(1, 6).Value = "Envío";
                ws.Cell(1, 7).Value = "Total";
                ws.Cell(1, 8).Value = "Estado";


                // Filas
                for (int i = 0; i < _pedidos.Count; i++)
                {
                    var p = _pedidos[i];
                    ws.Cell(i + 2, 1).Value = p.Id;
                    ws.Cell(i + 2, 2).Value = p.NombreUsuario;
                    ws.Cell(i + 2, 3).Value = p.Fecha.ToString("dd/MM/yyyy HH:mm");
                    ws.Cell(i + 2, 4).Value = p.Provincia;
                    ws.Cell(i + 2, 5).Value = p.Localidad;
                    ws.Cell(i + 2, 6).Value = p.CostoEnvio;
                    ws.Cell(i + 2, 7).Value = p.Total;
                    ws.Cell(i + 2, 8).Value = p.Estado.ToString();
                }

                ws.Columns().AdjustToContents();
                workbook.SaveAs(sfd.FileName);

                MessageBox.Show("Pedidos exportados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = cmbBuscarPor.SelectedItem?.ToString();
            string texto = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(texto))
            {
                bindingSource.DataSource = _pedidos;
                return;
            }

            IEnumerable<PedidoConDetallesDTO> filtrados = _pedidos;

            if (criterio == "ID Pedido")
            {
                if (!int.TryParse(texto, out int idPedido))
                {
                    MessageBox.Show("Para buscar por ID de pedido, debe ingresar un número entero.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                filtrados = _pedidos.Where(p => p.Id == idPedido);
            }
            else if (criterio == "ID Usuario")
            {
                if (!int.TryParse(texto, out int idBuscado))
                {
                    MessageBox.Show("Para buscar por ID de usuario, debe ingresar un número entero.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                filtrados = _pedidos.Where(p => p.UsuarioId == idBuscado);
            }
            else if (criterio == "Nombre Usuario")
            {
                filtrados = _pedidos.Where(p => p.NombreUsuario != null &&
                                                p.NombreUsuario.ToLower().Contains(texto));
            }
            else if (criterio == "Estado")
            {
                if (!Enum.TryParse(typeof(EstadoPedido), texto, true, out var estadoParsed))
                {
                    MessageBox.Show("Estado inválido. Usá uno de estos: Pendiente (0), EnCamino (1), Entregado (2), Cancelado (3)", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                filtrados = _pedidos.Where(p => p.Estado == (EstadoPedido)estadoParsed);
            }

            bindingSource.DataSource = filtrados.ToList();
        }

        private async void BtnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarPedidosAsync();
        }

        private async Task CargarPedidosAsync()
        {
            try
            {
                using var client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7144/api/")
                };
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);

                var pedidos = await client.GetFromJsonAsync<List<PedidoConDetallesDTO>>("pedidos/admin");

                if (pedidos == null || pedidos.Count == 0)
                {
                    MessageBox.Show("No se encontraron pedidos.");
                    return;
                }

                _pedidos = pedidos;
                _estadosOriginales = _pedidos.ToDictionary(p => p.Id, p => p.Estado);

                bindingSource.DataSource = _pedidos;
                dgvPedidos.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pedidos:\n" + ex.Message);
            }
        }


    }
}
