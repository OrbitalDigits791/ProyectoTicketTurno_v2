using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PTT.Presentation.Security;

namespace PTT.Presentation.Forms
{
    namespace PTT.Presentation.Forms
    {
        public partial class FormPrincipal : Form
        {
            public FormPrincipal()
            {
                InitializeComponent();
            }

            private void InitializeComponent()
            {
                throw new NotImplementedException();
            }

            private void FormPrincipal_Load(object sender, EventArgs e)
            {
                Text = "Sistema de Turnos - Coahuila";
                StartPosition = FormStartPosition.CenterScreen;
                WindowState = FormWindowState.Maximized;

                if (!SesionAplicacion.SesionActiva)
                {
                    MessageBox.Show("No hay sesión activa. Regrese al login.", "Sesión inválida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }
            }

            private void buttonRegistrarEstudiante_Click(object sender, EventArgs e)
            {
                using (var f = new FormRegistroEstudiante())
                {
                f.ShowDialog(this);
                }
            }


            private void buttonSolicitarTurno_Click(object sender, EventArgs e)
            {
                 using (var f = new FormSolicitudTurno())
                 {
                    f.ShowDialog(this);
                 }
            }

            private void buttonConsultarTurno_Click(object sender, EventArgs e)
            {
                using (var f = new FormConsultaAdmin())
                {
                    f.ShowDialog(this);
                }
            }

            private void buttonDashboard_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Abrir dashboard de estatus por municipio/total.", "Módulo");
            }

            private void buttonSalir_Click(object sender, EventArgs e)
            {
                CerrarSesionYSalir();
            }

            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    var r = MessageBox.Show(
                        "La sesión se cerrará y se bloqueará el acceso a la aplicación.\n¿Desea continuar?",
                        "Cerrar aplicación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (r == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }

                    SesionAplicacion.CerrarSesion();
                }

                base.OnFormClosing(e);
            }

            private void CerrarSesionYSalir()
            {
                var r = MessageBox.Show(
                    "La sesión se cerrará y se bloqueará el acceso a la aplicación.\n¿Desea salir?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (r != DialogResult.Yes) return;

                SesionAplicacion.CerrarSesion();
                Application.Exit();
            }
        }
    }
}