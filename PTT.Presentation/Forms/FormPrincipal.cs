using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTT.Presentation.Forms
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "Sistema de Turnos - Coahuila";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonRegistrarEstudiante_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Registro de Estudiantes (Por implementar)", "Info");
        }

        private void buttonSolicitarTurno_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Solicitud de Turno (Por implementar)", "Info");
        }

        private void buttonConsultarTurno_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Consulta de Turnos (Por implementar)", "Info");
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dashboard (Por implementar)", "Info");
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}