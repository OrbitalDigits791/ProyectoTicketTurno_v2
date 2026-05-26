using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using PTT.Presentation.Security;

namespace PTT.Presentation.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            Text = "Login - Proyecto Ticket de Turno";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string usuario = textBoxUsuario.Text?.Trim();
            string password = textBoxContraseña.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string adminUsuario = ConfigurationManager.AppSettings["AdminUsuario"] ?? "admin";
            string adminPassword = ConfigurationManager.AppSettings["AdminPassword"] ?? "Admin123*";

            if (!usuario.Equals(adminUsuario, StringComparison.OrdinalIgnoreCase) || password != adminPassword)
            {
                MessageBox.Show("Credenciales inválidas.", "Acceso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SesionAplicacion.IniciarSesion(usuario);

            FormPrincipal formPrincipal = new FormPrincipal();
            formPrincipal.Show();
            Hide();
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}