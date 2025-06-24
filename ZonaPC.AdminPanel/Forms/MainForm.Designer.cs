namespace ZonaPC.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnPedidos;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnCerrarSesion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnPedidos = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.Icon = AdminPanel.Properties.Resources.icono;
            this.SuspendLayout();


            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.btnPedidos, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnProductos, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnCerrarSesion, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(40, 40);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(40);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(400, 300);
            this.tableLayoutPanel.TabIndex = 0;

            // 
            // btnPedidos
            // 
            this.btnPedidos.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPedidos.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnPedidos.Location = new System.Drawing.Point(3, 3);
            this.btnPedidos.Name = "btnPedidos";
            this.btnPedidos.Size = new System.Drawing.Size(394, 94);
            this.btnPedidos.TabIndex = 0;
            this.btnPedidos.Text = "📦 PEDIDOS";
            this.btnPedidos.UseVisualStyleBackColor = false;
            this.btnPedidos.Click += new System.EventHandler(this.btnPedidos_Click);

            // 
            // btnProductos
            // 
            this.btnProductos.BackColor = System.Drawing.Color.LightGreen;
            this.btnProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProductos.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnProductos.Location = new System.Drawing.Point(3, 103);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(394, 94);
            this.btnProductos.TabIndex = 1;
            this.btnProductos.Text = "🖥️ PRODUCTOS";
            this.btnProductos.UseVisualStyleBackColor = false;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);

            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.LightCoral;
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.Location = new System.Drawing.Point(3, 203);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(394, 94);
            this.btnCerrarSesion.TabIndex = 2;
            this.btnCerrarSesion.Text = "🔒 CERRAR SESIÓN";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 380);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de Administración - ZonaPC";
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}