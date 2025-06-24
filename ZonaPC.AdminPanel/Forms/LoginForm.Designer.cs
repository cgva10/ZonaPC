namespace ZonaPC.AdminPanel.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblEmail = new Label();
            lblPassword = new Label();
            lblMensaje = new Label();
            picAdvertencia = new PictureBox();
            btnMostrarPassword = new Button();
            var lblTitulo = new Label();
            var picLogo = new PictureBox();

            SuspendLayout();

            // LoginForm (form style)
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.icono;

            // lblTitulo
            lblTitulo.Text = "ZonaPC - Panel de Administración";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(30, 30, 30);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(320, 30);
            Controls.Add(lblTitulo);

            // picLogo
            picLogo.Image = Properties.Resources.icono.ToBitmap();
            picLogo.Size = new Size(64, 64);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.Location = new Point(250, 20);
            Controls.Add(picLogo);

            // txtEmail
            txtEmail.Location = new Point(350, 105);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(369, 27);
            txtEmail.TabIndex = 0;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.ForeColor = Color.Gray;
            txtEmail.Text = "Ingresá tu email";
            txtEmail.GotFocus += (s, e) =>
            {
                if (txtEmail.Text == "Ingresá tu email")
                {
                    txtEmail.Text = "";
                    txtEmail.ForeColor = Color.Black;
                }
            };
            txtEmail.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    txtEmail.Text = "Ingresá tu email";
                    txtEmail.ForeColor = Color.Gray;
                }
            };

            // txtPassword
            txtPassword.Location = new Point(350, 228);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(330, 27);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = false;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.ForeColor = Color.Gray;
            txtPassword.Text = "Contraseña";
            txtPassword.GotFocus += (s, e) =>
            {
                if (txtPassword.Text == "Contraseña")
                {
                    txtPassword.Clear();
                    txtPassword.ForeColor = Color.Black;
                }
                if (!string.IsNullOrWhiteSpace(txtPassword.Text) && txtPassword.Text != "Contraseña")
                {
                    txtPassword.UseSystemPasswordChar = true;
                }
            };
            txtPassword.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    txtPassword.UseSystemPasswordChar = false;
                    txtPassword.Text = "Contraseña";
                    txtPassword.ForeColor = Color.Gray;
                }
            };

            // btnMostrarPassword
            btnMostrarPassword.Text = "👁";
            btnMostrarPassword.Font = new Font("Segoe UI", 8F);
            btnMostrarPassword.Location = new Point(685, 228);
            btnMostrarPassword.Size = new Size(35, 27);
            btnMostrarPassword.FlatStyle = FlatStyle.Flat;
            btnMostrarPassword.TabIndex = 6;
            btnMostrarPassword.Click += (s, e) =>
            {
                if (txtPassword.Text != "Contraseña")
                {
                    txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
                }
            };

            // btnLogin
            btnLogin.ForeColor = Color.White;
            btnLogin.BackColor = Color.Crimson;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Location = new Point(397, 330);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(267, 38);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(270, 108);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "Email:";
            lblEmail.Font = new Font("Segoe UI", 10F);

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(230, 231);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(86, 20);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Contraseña:";
            lblPassword.Font = new Font("Segoe UI", 10F);

            // picAdvertencia
            picAdvertencia.Image = SystemIcons.Warning.ToBitmap();
            picAdvertencia.Location = new Point(400, 390);
            picAdvertencia.Size = new Size(24, 24);
            picAdvertencia.SizeMode = PictureBoxSizeMode.StretchImage;
            picAdvertencia.Visible = false;

            // lblMensaje
            lblMensaje.AutoSize = true;
            lblMensaje.Location = new Point(430, 392);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 5;
            lblMensaje.ForeColor = Color.Crimson;

            // LoginForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 526);
            Controls.Add(picAdvertencia);
            Controls.Add(lblMensaje);
            Controls.Add(lblPassword);
            Controls.Add(lblEmail);
            Controls.Add(btnLogin);
            Controls.Add(btnMostrarPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Name = "LoginForm";
            Text = "ZonaPC - Acceso Admin";
            this.AcceptButton = btnLogin;
            ResumeLayout(false);
            PerformLayout();
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        #endregion

        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblMensaje;
        private Button btnMostrarPassword;
        private PictureBox picAdvertencia;
    }
}
