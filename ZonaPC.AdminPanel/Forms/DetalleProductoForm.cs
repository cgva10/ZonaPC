using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiendaComponentes.Shared.DTOs;

namespace ZonaPC.WinForms
{
    public partial class DetalleProductoForm : Form
    {
        private readonly ProductoDTO _producto;

        public DetalleProductoForm(ProductoDTO producto)
        {
            InitializeComponent();
            _producto = producto;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

        }

        private async void DetalleProductoForm_Load(object sender, EventArgs e)
        {
            lblNombre.Text = _producto.Nombre;
            lblDescripcion.Text = _producto.Descripcion;
            lblPrecio.Text = $"Precio: ${_producto.Precio:N0}";
            lblStock.Text = $"Stock: {_producto.Stock}";
            lblCategoria.Text = $"Categoría: {_producto.CategoriaAmigable}";
            lblSubcategoria.Text = $"Subcategoría: {_producto.Subcategoria}";

            // Imagen principal
            await CargarImagenAsync(_producto.ImagenUrl, picPrincipal);

            // Galeria
            if (_producto.ImagenesUrl != null)
            {
                foreach (var url in _producto.ImagenesUrl)
                {
                    var pic = new PictureBox
                    {
                        Size = new Size(100, 100),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Margin = new Padding(5),
                        Cursor = Cursors.Hand, // cambia el cursor al pasar
                        Tag = url // guardamos la URL
                    };

                    pic.Click += async (s, e) =>
                    {
                        if (s is PictureBox clickedPic && clickedPic.Tag is string clickedUrl)
                        {
                            await CargarImagenAsync(clickedUrl, picPrincipal);
                        }
                    };

                    flpGaleria.Controls.Add(pic);
                    await CargarImagenAsync(url, pic);
                }

            }
        }

        private async Task CargarImagenAsync(string url, PictureBox pictureBox)
        {
            try
            {
                using var client = new HttpClient();
                var stream = await client.GetStreamAsync(url);
                pictureBox.Image = Image.FromStream(stream);
            }
            catch
            {
                pictureBox.Image = null;
            }
        }
    }
}
