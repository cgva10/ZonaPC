namespace ZonaPC.WinForms
{
    partial class DetalleProductoForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblNombre;
        private Label lblDescripcion;
        private Label lblPrecio;
        private Label lblStock;
        private Label lblCategoria;
        private Label lblSubcategoria;
        private PictureBox picPrincipal;
        private FlowLayoutPanel flpGaleria;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNombre = new Label();
            this.lblDescripcion = new Label();
            this.lblPrecio = new Label();
            this.lblStock = new Label();
            this.lblCategoria = new Label();
            this.lblSubcategoria = new Label();
            this.picPrincipal = new PictureBox();
            this.flpGaleria = new FlowLayoutPanel();
            this.Icon = AdminPanel.Properties.Resources.icono;

            ((System.ComponentModel.ISupportInitialize)(this.picPrincipal)).BeginInit();
            this.SuspendLayout();

            // lblNombre
            this.lblNombre.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblNombre.Location = new Point(12, 9);
            this.lblNombre.Size = new Size(500, 30);

            // lblDescripcion
            this.lblDescripcion.Location = new Point(12, 45);
            this.lblDescripcion.Size = new Size(500, 60);

            // picPrincipal
            this.picPrincipal.Location = new Point(520, 9);
            this.picPrincipal.Size = new Size(250, 250);
            this.picPrincipal.SizeMode = PictureBoxSizeMode.Zoom;
            this.picPrincipal.BorderStyle = BorderStyle.None;

            // lblPrecio
            this.lblPrecio.Location = new Point(12, 110);
            this.lblPrecio.Size = new Size(300, 25);

            // lblStock
            this.lblStock.Location = new Point(12, 140);
            this.lblStock.Size = new Size(300, 25);

            // lblCategoria
            this.lblCategoria.Location = new Point(12, 170);
            this.lblCategoria.Size = new Size(300, 25);

            // lblSubcategoria
            this.lblSubcategoria.Location = new Point(12, 200);
            this.lblSubcategoria.Size = new Size(300, 25);

            // flpGaleria
            this.flpGaleria.Location = new Point(12, 240);
            this.flpGaleria.Size = new Size(758, 120);
            this.flpGaleria.AutoScroll = true;

            // Form
            this.ClientSize = new Size(784, 380);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.lblSubcategoria);
            this.Controls.Add(this.picPrincipal);
            this.Controls.Add(this.flpGaleria);
            this.Text = "Detalle del Producto";
            this.Load += new EventHandler(this.DetalleProductoForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.picPrincipal)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
