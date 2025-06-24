using System;
using System.Windows.Forms;
using ZonaPC.AdminPanel.Forms;

namespace ZonaPC.WinForms
{
    public partial class MainForm : Form
    {
        private readonly string _token;

        public MainForm(string token)
        {
            InitializeComponent();
            _token = token;
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            var pedidosForm = new PedidosForm(_token);
            pedidosForm.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            var productosForm = new ProductosForm(_token);
            productosForm.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}