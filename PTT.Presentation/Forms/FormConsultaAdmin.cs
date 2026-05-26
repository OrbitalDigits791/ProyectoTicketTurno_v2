using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using ProyectoTicketTurno.Data.Context;

namespace PTT.Presentation.Forms
{
    public class FormConsultaAdmin : Form
    {
        private ComboBox cbModo;
        private TextBox txtFiltro;
        private Button btnBuscar, btnEditarEstudiante, btnEditarSolicitud, btnEliminarSolicitud, btnCambiarEstatus;
        private ComboBox cbNuevoEstatus;
        private DataGridView dgv;

        public FormConsultaAdmin()
        {
            InicializarUI();
        }

        private void InicializarUI()
        {
            Text = "Consulta Administrador";
            Width = 1000;
            Height = 600;
            StartPosition = FormStartPosition.CenterParent;

            cbModo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 140 };
            cbModo.Items.AddRange(new object[] { "CURP", "Nombre" });
            cbModo.SelectedIndex = 0;

            txtFiltro = new TextBox { Width = 220 };
            btnBuscar = new Button { Text = "Buscar" };
            btnBuscar.Click += (s, e) => Buscar();

            btnEditarEstudiante = new Button { Text = "Editar Estudiante" };
            btnEditarSolicitud = new Button { Text = "Editar Solicitud" };
            btnEliminarSolicitud = new Button { Text = "Eliminar Solicitud" };
            btnCambiarEstatus = new Button { Text = "Cambiar Estatus" };

            cbNuevoEstatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 };
            cbNuevoEstatus.Items.AddRange(new object[] { "Pendiente", "Resuelto", "Cancelado" });
            cbNuevoEstatus.SelectedIndex = 0;

            btnEditarEstudiante.Click += (s, e) => EditarEstudiante();
            btnEditarSolicitud.Click += (s, e) => EditarSolicitud();
            btnEliminarSolicitud.Click += (s, e) => EliminarSolicitud();
            btnCambiarEstatus.Click += (s, e) => CambiarEstatus();

            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };
            top.Controls.Add(new Label { Text = "Buscar por:", AutoSize = true, Padding = new Padding(0, 10, 0, 0) });
            top.Controls.Add(cbModo);
            top.Controls.Add(txtFiltro);
            top.Controls.Add(btnBuscar);
            top.Controls.Add(btnEditarEstudiante);
            top.Controls.Add(btnEditarSolicitud);
            top.Controls.Add(btnEliminarSolicitud);
            top.Controls.Add(cbNuevoEstatus);
            top.Controls.Add(btnCambiarEstatus);

            dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            Controls.Add(dgv);
            Controls.Add(top);
        }

        private void Buscar()
        {
            string filtro = (txtFiltro.Text ?? "").Trim();

            using (var ctx = new AplicacionDbContext())
            {
                var q = ctx.SolicitudesTurno
                    .AsNoTracking()
                    .Join(ctx.Estudiantes.AsNoTracking(),
                        s => s.CURP,
                        e => e.CURP,
                        (s, e) => new
                        {
                            s.NumeroTurno,
                            s.CURP,
                            NombreCompleto = e.Nombre + " " + e.ApellidoPaterno + " " + e.ApellidoMaterno,
                            s.IdMunicipio,
                            s.IdAsunto,
                            s.Estatus,
                            s.FechaSolicitud
                        });

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    if (cbModo.SelectedItem.ToString() == "CURP")
                        q = q.Where(x => x.CURP.Contains(filtro));
                    else
                        q = q.Where(x => x.NombreCompleto.Contains(filtro));
                }

                dgv.DataSource = q.OrderByDescending(x => x.FechaSolicitud).ToList();
            }
        }

        private void EditarEstudiante()
        {
            var row = dgv.CurrentRow;
            if (row == null) return;
            string curp = Convert.ToString(row.Cells["CURP"].Value);

            using (var f = new FormRegistroEstudiante(curp))
            {
                f.ShowDialog(this);
            }
            Buscar();
        }

        private void EditarSolicitud()
        {
            var row = dgv.CurrentRow;
            if (row == null) return;
            string curp = Convert.ToString(row.Cells["CURP"].Value);
            int turno = Convert.ToInt32(row.Cells["NumeroTurno"].Value);

            using (var f = new FormSolicitudTurno(curp, turno))
            {
                f.ShowDialog(this);
            }
            Buscar();
        }

        private void EliminarSolicitud()
        {
            var row = dgv.CurrentRow;
            if (row == null) return;

            int turno = Convert.ToInt32(row.Cells["NumeroTurno"].Value);

            var r = MessageBox.Show("¿Eliminar la solicitud seleccionada?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            using (var ctx = new AplicacionDbContext())
            {
                var sol = ctx.SolicitudesTurno.FirstOrDefault(x => x.NumeroTurno == turno);
                if (sol != null)
                {
                    ctx.SolicitudesTurno.Remove(sol);
                    ctx.SaveChanges();
                }
            }

            Buscar();
        }

        private void CambiarEstatus()
        {
            var row = dgv.CurrentRow;
            if (row == null) return;

            int turno = Convert.ToInt32(row.Cells["NumeroTurno"].Value);
            string nuevo = cbNuevoEstatus.SelectedItem.ToString();

            using (var ctx = new AplicacionDbContext())
            {
                var sol = ctx.SolicitudesTurno.FirstOrDefault(x => x.NumeroTurno == turno);
                if (sol == null) return;

                sol.Estatus = nuevo;
                sol.FechaResolucion = nuevo == "Resuelto" ? (DateTime?)DateTime.Now : null;
                ctx.SaveChanges();
            }

            Buscar();
        }
    }
}