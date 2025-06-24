using System;
using System.Windows.Forms;
using ZonaPC.AdminPanel.Forms;


namespace ZonaPC.AdminPanel
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;


            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}
