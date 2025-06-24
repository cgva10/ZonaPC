using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TiendaComponentes.Shared.DTOs;
using System.ComponentModel;

namespace ZonaPC.WinForms
{
    public partial class ProductoForm : Form
    {


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProductoCreacionDTO Producto { get; private set; }

        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private TextBox txtPrecio;
        private TextBox txtStock;
        private ComboBox cmbCategoria;
        private ComboBox cmbSubcategoria;
        private TextBox txtImagenes;
        private Button btnGuardar;

        public ProductoForm(ProductoDTO? existente = null)
        {
            Text = existente == null ? "Agregar Producto" : "Modificar Producto";
            Size = new Size(500, 440);
            StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            txtNombre = new TextBox { PlaceholderText = "Nombre", Width = 400 };

            txtDescripcion = new TextBox
            {
                PlaceholderText = "Descripción",
                Width = 400,
                Height = 100,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            txtPrecio = new TextBox { PlaceholderText = "Precio", Width = 400 };
            txtStock = new TextBox { PlaceholderText = "Stock", Width = 400 };

            cmbCategoria = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 400 };
            cmbCategoria.Items.AddRange(new[] { "Notebook", "Placas de Video" });

            cmbSubcategoria = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 400 };
            cmbSubcategoria.Items.AddRange(new[] { "Gaming", "Oficina", "Nvidia", "AMD" });

            txtImagenes = new TextBox { PlaceholderText = "URLs de imágenes separadas por coma", Width = 400 };

            btnGuardar = new Button { Text = "Guardar", Width = 100, Height = 35 };
            btnGuardar.Click += (_, _) => Guardar();

            var panelBoton = new Panel
            {
                Width = 400,
                Height = 45,
                Padding = new Padding(0, 10, 0, 0)
            };
            panelBoton.Controls.Add(btnGuardar);
            btnGuardar.Dock = DockStyle.Right;

            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false,
                Padding = new Padding(10)
            };

            panel.Controls.AddRange(new Control[]
            {
        txtNombre, txtDescripcion, txtPrecio, txtStock,
        cmbCategoria, cmbSubcategoria, txtImagenes, panelBoton
            });

            Controls.Add(panel);

            if (existente != null)
            {
                txtNombre.Text = existente.Nombre;
                txtDescripcion.Text = existente.Descripcion;
                txtPrecio.Text = existente.Precio.ToString();
                txtStock.Text = existente.Stock.ToString();
                cmbCategoria.SelectedItem = existente.Categoria;
                cmbSubcategoria.SelectedItem = existente.Subcategoria;
                txtImagenes.Text = string.Join(",", existente.ImagenesUrl);
            }
        }


        private void Guardar()
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                cmbCategoria.SelectedItem == null ||
                cmbSubcategoria.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtImagenes.Text))
            {
                MessageBox.Show("Por favor completa todos los campos.", "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación de valores numéricos
            if (!decimal.TryParse(txtPrecio.Text, out var precio) || !int.TryParse(txtStock.Text, out var stock))
            {
                MessageBox.Show("Precio y Stock deben ser valores numéricos válidos.", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Producto = new ProductoCreacionDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Precio = precio,
                Stock = stock,
                Categoria = cmbCategoria.SelectedItem.ToString(),
                Subcategoria = cmbSubcategoria.SelectedItem.ToString(),
                ImagenesUrl = txtImagenes.Text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
            };

            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
