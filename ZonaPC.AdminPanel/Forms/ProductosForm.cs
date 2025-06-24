
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Windows.Forms;
using TiendaComponentes.Shared.DTOs;

namespace ZonaPC.WinForms
{
    public partial class ProductosForm : Form
    {
        private readonly string _token;
        private List<ProductoDTO> _productos = new();
        private readonly HttpClient _httpClient;

        public ProductosForm(string token)
        {
            InitializeComponent();
            _token = token;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7144/api/")
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            btnRefrescar.Click += async (s, e) => await CargarProductos();
            dgvProductos.CellDoubleClick += DgvProductos_CellDoubleClick;
            dgvProductos.CellFormatting += DgvProductos_CellFormatting;
            btnAgregar.Click += BtnAgregar_Click;
            btnModificar.Click += BtnModificar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnExportarExcel.Click += BtnExportarExcel_Click;

            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnBuscar_Click(s, EventArgs.Empty);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            // Estilo general
            foreach (var btn in new[] { btnAgregar, btnModificar, btnEliminar, btnRefrescar })
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(240, 240, 240); // Gris claro
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                btn.Padding = new Padding(5, 2, 5, 2);
                btn.Height = 32;
                btn.Cursor = Cursors.Hand;
            }

            // Texto con emojis para cada acción
            btnAgregar.Text = "➕ Agregar";
            btnModificar.Text = "✏️ Modificar";
            btnEliminar.Text = "🗑 Eliminar";
            btnRefrescar.Text = "🔄 Refrescar";

            btnAgregar.BackColor = Color.LightGreen;
            btnModificar.BackColor = Color.LightSkyBlue;
            btnEliminar.BackColor = Color.LightCoral;
            btnRefrescar.BackColor = Color.LightGray;


        }

        private async void ProductosForm_Load(object sender, EventArgs e)
        {
            await CargarProductos();
        }

        private async Task CargarProductos()
        {
            try
            {
                _productos = await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("producto") ?? new();
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = _productos;

                foreach (var col in new[] { "ImagenesUrl", "Categoria", "ImagenUrl" })
                {
                    if (dgvProductos.Columns.Contains(col))
                        dgvProductos.Columns[col].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        private void DgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.Rows[e.RowIndex].DataBoundItem is ProductoDTO producto)
            {
                var detalleForm = new DetalleProductoForm(producto);
                detalleForm.ShowDialog();
            }
        }

        private void DgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProductos.Rows[e.RowIndex].DataBoundItem is ProductoDTO prod)
            {
                var color = prod.Subcategoria?.ToLower() switch
                {
                    "gaming" => ColorTranslator.FromHtml("#bfdbfe"),  
                    "oficina" => ColorTranslator.FromHtml("#e5e7eb"),
                    "nvidia" => ColorTranslator.FromHtml("#d1fae5"),
                    "amd" => ColorTranslator.FromHtml("#fee2e2"),
                    _ => Color.White
                };

                dgvProductos.Rows[e.RowIndex].DefaultCellStyle.BackColor = color;
            }
        }

        private async void BtnAgregar_Click(object sender, EventArgs e)
        {
            var form = new ProductoForm(); // lo creamos ahora en el siguiente paso
            if (form.ShowDialog() == DialogResult.OK)
            {
                var response = await _httpClient.PostAsJsonAsync("producto", form.Producto);
                if (response.IsSuccessStatusCode)
                    await CargarProductos();
                else
                    MessageBox.Show("Error al agregar producto.");
            }
        }

        private async void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is not ProductoDTO prod) return;

            var form = new ProductoForm(prod);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var response = await _httpClient.PutAsJsonAsync($"producto/{prod.Id}", form.Producto);
                if (response.IsSuccessStatusCode)
                    await CargarProductos();
                else
                    MessageBox.Show("Error al modificar producto.");
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is not ProductoDTO prod) return;

            if (MessageBox.Show($"¿Estás seguro de eliminar el producto con ID: {prod.Id}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var response = await _httpClient.DeleteAsync($"producto/{prod.Id}");
                if (response.IsSuccessStatusCode)
                    await CargarProductos();
                else
                    MessageBox.Show("Error al eliminar producto.");
            }
        }

        private void BtnExportarExcel_Click(object sender, EventArgs e)
        {
            if (_productos == null || _productos.Count == 0)
            {
                MessageBox.Show("No hay productos para exportar.");
                return;
            }

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files|*.csv";
                saveDialog.Title = "Guardar productos como Excel (CSV)";
                saveDialog.FileName = "productos_exportados.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var writer = new StreamWriter(saveDialog.FileName, false, Encoding.UTF8))
                        {
                            // encabezados
                            writer.WriteLine("ID,Nombre,Descripción,Precio,Stock,Categoría,Subcategoría");

                            foreach (var p in _productos)
                            {
                                writer.WriteLine($"\"{p.Id}\",\"{p.Nombre}\",\"{p.Descripcion}\",\"{p.Precio}\",\"{p.Stock}\",\"{p.Categoria}\",\"{p.Subcategoria}\"");
                            }
                        }

                        MessageBox.Show("Productos exportados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar: " + ex.Message);
                    }
                }
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = cmbBuscarPor.SelectedItem?.ToString();
            string texto = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(texto))
            {
                dgvProductos.DataSource = _productos;
                return;
            }

            IEnumerable<ProductoDTO> filtrados = _productos;

            switch (criterio)
            {
                case "ID":
                    if (int.TryParse(texto, out int id))
                        filtrados = _productos.Where(p => p.Id == id);
                    else
                        MessageBox.Show("Para buscar por ID de producto, debe ingresar un número entero.");
                    break;

                case "Nombre":
                    filtrados = _productos.Where(p => p.Nombre.ToLower().Contains(texto));
                    break;

                case "Categoría":
                    filtrados = _productos.Where(p => p.CategoriaAmigable.ToLower().Contains(texto));
                    if (!filtrados.Any())
                    {
                        MessageBox.Show("Categoría inválida. Las categorías válidas son: Notebooks y Placas de Video.");
                        return;
                    }
                    break;

                case "Subcategoría":
                    filtrados = _productos.Where(p => p.Subcategoria.ToLower().Contains(texto));
                    if (!filtrados.Any())
                    {
                        MessageBox.Show("Subcategoría inválida. Las opciones válidas son: Gaming, Oficina, Nvidia, AMD.");
                        return;
                    }
                    break;
            }

            dgvProductos.DataSource = filtrados.ToList();
        }




    }
}
