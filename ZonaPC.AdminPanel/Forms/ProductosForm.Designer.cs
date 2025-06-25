namespace ZonaPC.WinForms
{
    partial class ProductosForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.FlowLayoutPanel flowPanelBotones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnExportarExcel;
        private ComboBox cmbBuscarPor;
        private TextBox txtBuscar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.Icon = AdminPanel.Properties.Resources.icono;

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();

            var panelBuscador = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(10),
                BackColor = SystemColors.ControlLight
            };

            var cmbBuscarPor = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 140,
                Location = new Point(10, 8)
            };
            cmbBuscarPor.Items.AddRange(new object[] { "ID", "Nombre", "Categoría", "Subcategoría" });
            cmbBuscarPor.SelectedIndex = 0;

            var txtBuscar = new TextBox
            {
                Width = 200,
                Location = new Point(160, 8)
            };

            var btnBuscar = new Button
            {
                Text = "Buscar",
                Location = new Point(370, 6),
                Size = new Size(80, 25)
            };
            btnBuscar.Click += BtnBuscar_Click;

            panelBuscador.Controls.Add(cmbBuscarPor);
            panelBuscador.Controls.Add(txtBuscar);
            panelBuscador.Controls.Add(btnBuscar);
            this.cmbBuscarPor = cmbBuscarPor;
            this.txtBuscar = txtBuscar;

            // dgvProductos
            this.dgvProductos.Dock = DockStyle.Fill;
            this.dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.RowTemplate.Height = 29;
            this.dgvProductos.TabIndex = 0;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Botones
            this.flowPanelBotones = new FlowLayoutPanel();
            this.btnAgregar = new Button();
            this.btnModificar = new Button();
            this.btnEliminar = new Button();
            this.btnExportarExcel = new Button();

            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Size = new Size(120, 30);

            btnAgregar.Text = "Agregar";
            btnAgregar.Size = new Size(120, 30);

            btnModificar.Text = "Modificar";
            btnModificar.Size = new Size(120, 30);

            btnEliminar.Text = "Eliminar";
            btnEliminar.Size = new Size(120, 30);

            btnExportarExcel.Text = "📤 Exportar";
            btnExportarExcel.Size = new Size(120, 30);

            flowPanelBotones.Dock = DockStyle.Fill;
            flowPanelBotones.FlowDirection = FlowDirection.LeftToRight;
            flowPanelBotones.WrapContents = false;
            flowPanelBotones.Padding = new Padding(5);
            flowPanelBotones.Controls.Add(btnRefrescar);
            flowPanelBotones.Controls.Add(btnAgregar);
            flowPanelBotones.Controls.Add(btnModificar);
            flowPanelBotones.Controls.Add(btnEliminar);
            flowPanelBotones.Controls.Add(btnExportarExcel);

            // Panel inferior
            var panelInferior = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10, 8, 10, 10),
                BackColor = SystemColors.Control
            };
            panelInferior.Controls.Add(flowPanelBotones);

            // Final: ensamblar
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(784, 481);
            this.Controls.Add(dgvProductos);
            this.Controls.Add(panelInferior);
            this.Controls.Add(panelBuscador);
            this.Name = "ProductosForm";
            this.Text = "Gestión de Productos";
            this.Load += ProductosForm_Load;

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
        }

    }
}
