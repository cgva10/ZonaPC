namespace ZonaPC.WinForms
{
    partial class PedidosForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.Button btnActualizarEstados;
        private System.Windows.Forms.Button btnEliminarCancelados;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel bottomPanel;

        private System.Windows.Forms.Button btnEliminarCance;
        private System.Windows.Forms.Button btnExcel;

        private System.Windows.Forms.Panel panelBuscador;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cmbBuscarPor;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnRefrescar;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvPedidos = new DataGridView();
            dgvDetalles = new DataGridView();
            btnActualizarEstados = new Button();
            splitContainerMain = new SplitContainer();
            bottomPanel = new Panel();
            btnEliminarCance = new Button();
            btnExcel = new Button();
            panelBuscador = new Panel();
            txtBuscar = new TextBox();
            cmbBuscarPor = new ComboBox();
            btnBuscar = new Button();
            this.Icon = AdminPanel.Properties.Resources.icono;

            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            bottomPanel.SuspendLayout();
            panelBuscador.SuspendLayout();
            SuspendLayout();

            // panelBuscador
            panelBuscador.Dock = DockStyle.Top;
            panelBuscador.Height = 40;
            panelBuscador.Padding = new Padding(10);
            panelBuscador.BackColor = SystemColors.ControlLight;

            // cmbBuscarPor
            cmbBuscarPor.Items.AddRange(new object[] {"ID Pedido", "ID Usuario", "Nombre Usuario", "Estado" });
            cmbBuscarPor.SelectedIndex = 0;
            cmbBuscarPor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBuscarPor.SelectedIndex = 0;
            cmbBuscarPor.Width = 140;
            cmbBuscarPor.Location = new Point(10, 8);

            // txtBuscar
            txtBuscar.Width = 200;
            txtBuscar.Location = new Point(160, 8);

            // btnBuscar
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new Point(370, 6);
            btnBuscar.Size = new Size(80, 25);

            // btnRefrescar
            btnRefrescar = new Button();
            btnRefrescar.Dock = DockStyle.Left;
            btnRefrescar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefrescar.Size = new Size(160, 40);
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            bottomPanel.Controls.Add(btnRefrescar);


            // agregar controles al panel
            panelBuscador.Controls.Add(cmbBuscarPor);
            panelBuscador.Controls.Add(txtBuscar);
            panelBuscador.Controls.Add(btnBuscar);

            // dgvPedidos
            dgvPedidos.ColumnHeadersHeight = 29;
            dgvPedidos.Dock = DockStyle.Fill;
            dgvPedidos.Location = new Point(0, 0);
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.RowHeadersWidth = 51;
            dgvPedidos.Size = new Size(726, 600);
            dgvPedidos.TabIndex = 0;
            dgvPedidos.AllowUserToAddRows = false;

            // dgvDetalles
            dgvDetalles.ColumnHeadersHeight = 29;
            dgvDetalles.Dock = DockStyle.Fill;
            dgvDetalles.Location = new Point(0, 0);
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.RowHeadersWidth = 51;
            dgvDetalles.Size = new Size(170, 600);
            dgvDetalles.TabIndex = 1;

            // btnActualizarEstados
            btnActualizarEstados.Dock = DockStyle.Right;
            btnActualizarEstados.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActualizarEstados.Location = new Point(700, 0);
            btnActualizarEstados.Name = "btnActualizarEstados";
            btnActualizarEstados.Size = new Size(200, 40);
            btnActualizarEstados.TabIndex = 2;
            btnActualizarEstados.Text = "Actualizar Estados";
            btnActualizarEstados.UseVisualStyleBackColor = true;

            // splitContainerMain
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(0, 0);
            splitContainerMain.Name = "splitContainerMain";

            // splitContainerMain.Panel1
            splitContainerMain.Panel1.Controls.Add(dgvPedidos);

            // splitContainerMain.Panel2
            splitContainerMain.Panel2.Controls.Add(dgvDetalles);
            splitContainerMain.Size = new Size(900, 600);
            splitContainerMain.SplitterDistance = 726;
            splitContainerMain.TabIndex = 4;

            // bottomPanel
            bottomPanel.Controls.Add(btnExcel);
            bottomPanel.Controls.Add(btnEliminarCance);
            bottomPanel.Controls.Add(btnActualizarEstados);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 600);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(900, 40);
            bottomPanel.TabIndex = 3;

            // btnEliminarCance
            btnEliminarCance.Dock = DockStyle.Right;
            btnEliminarCance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminarCance.Location = new Point(404, 0);
            btnEliminarCance.Name = "btnEliminarCance";
            btnEliminarCance.Size = new Size(296, 40);
            btnEliminarCance.TabIndex = 3;
            btnEliminarCance.Text = "Eliminar Pedidos Cancelados";
            btnEliminarCance.UseVisualStyleBackColor = true;
            btnEliminarCance.Click += btnEliminarCance_Click;

            // btnExcel
            btnExcel.Dock = DockStyle.Right;
            btnExcel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExcel.Location = new Point(204, 0);
            btnExcel.Name = "btnExcel";
            btnExcel.Size = new Size(200, 40);
            btnExcel.TabIndex = 4;
            btnExcel.Text = "Exportar Excel";
            btnExcel.UseVisualStyleBackColor = true;
            btnExcel.Click += btnExcel_Click;

            // PedidosForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 640);
            Controls.Add(splitContainerMain);
            Controls.Add(bottomPanel);
            Controls.Add(panelBuscador);
            Name = "PedidosForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de Pedidos";
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            panelBuscador.ResumeLayout(false);
            panelBuscador.PerformLayout();
            ResumeLayout(false);
        }
    }
}
