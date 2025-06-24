using ClosedXML.Excel;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using ZonaPC.AdminPanel.Models;
using ZonaPC.AdminPanel.Services;
using ZonaPC.WinForms;

namespace ZonaPC.AdminPanel.Forms
{
    public partial class LoginForm : Form
    {
        private int intentosFallidos = 0;
        private DateTime? loginBloqueadoHasta = null;

        public LoginForm()
        {
            InitializeComponent();

            this.AcceptButton = btnLogin;

            this.Opacity = 0;
            var fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 30;

            fadeTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.05;
                else
                    fadeTimer.Stop();
            };

            fadeTimer.Start();



        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (loginBloqueadoHasta != null && DateTime.Now < loginBloqueadoHasta)
            {
                picAdvertencia.Visible = true;
                lblMensaje.Text = $"Login bloqueado. Esperá {Math.Ceiling((loginBloqueadoHasta.Value - DateTime.Now).TotalSeconds)} segundos.";
                lblMensaje.ForeColor = Color.Crimson;
                return;
            }

            if (intentosFallidos >= 3)
            {
                loginBloqueadoHasta = DateTime.Now.AddSeconds(10);
                intentosFallidos = 0;
                lblMensaje.Text = "Demasiados intentos. Esperá 10 segundos.";
            }


            if (string.IsNullOrWhiteSpace(txtEmail.Text) || txtEmail.Text == "Ingresá tu email" ||
                string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text == "Contraseña")
            {
                picAdvertencia.Visible = true;
                lblMensaje.Text = "Por favor, completá todos los campos.";
                lblMensaje.ForeColor = Color.Crimson;
                return;
            }

            picAdvertencia.Visible = false;
            lblMensaje.Text = "Iniciando sesión...";
            lblMensaje.ForeColor = Color.Gray;
            btnLogin.Enabled = false;
            btnLogin.Text = "Verificando...";

            var loginData = new
            {
                email = txtEmail.Text,
                contrasena = txtPassword.Text
            };

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7144/api/")
            };

            try
            {
                var response = await client.PostAsJsonAsync("Usuarios/login", loginData);

                if (!response.IsSuccessStatusCode)
                {
                    intentosFallidos++;
                    picAdvertencia.Visible = true;
                    lblMensaje.Text = "Credenciales incorrectas.";
                    lblMensaje.ForeColor = Color.Crimson;
                    return;
                }

                var json = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (json == null)
                {
                    picAdvertencia.Visible = true;
                    lblMensaje.Text = "Error inesperado. Token no recibido.";
                    lblMensaje.ForeColor = Color.Crimson;
                    return;
                }

                if (json.Rol?.ToLower() != "admin")
                {
                    picAdvertencia.Visible = true;
                    lblMensaje.Text = $"Acceso denegado.";
                    lblMensaje.Left = (this.ClientSize.Width - lblMensaje.Width) / 2;

                    lblMensaje.ForeColor = Color.Crimson;
                    return;
                }

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", json.Token);

                ApiHelper.InitializeClient(json.Token);

                this.Hide();
                var mainForm = new MainForm(json.Token);
                mainForm.Show();
            }
            catch (Exception ex)
            {
                picAdvertencia.Visible = true;
                lblMensaje.Text = "Error al conectar con la API.";
                lblMensaje.ForeColor = Color.Crimson;
                MessageBox.Show(ex.ToString(), "Excepción");
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Iniciar Sesión";
            }
        }

    }
}
