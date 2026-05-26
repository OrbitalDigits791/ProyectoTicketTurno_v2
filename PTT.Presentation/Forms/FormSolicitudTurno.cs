using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using ProyectoTicketTurno.Business.Models;
using ProyectoTicketTurno.Data.Context;

namespace PTT.Presentation.Forms
{
    public class FormSolicitudTurno : Form
    {
        private readonly string _curpInicial;
        private readonly int? _turnoInicial;

        private TextBox txtCURP, txtNumeroTurno, txtPersona, txtParentesco, txtObservaciones;
        private ComboBox cbMunicipio, cbAsunto, cbEstatus;
        private Button btnCargar, btnGuardar;

        public FormSolicitudTurno(string curpInicial = null, int? turnoInicial = null)
        {
            _curpInicial = curpInicial;
            _turnoInicial = turnoInicial;
            InicializarUI();
            Load += FormSolicitudTurno_Load;
        }

        private void FormSolicitudTurno_Load(object sender, EventArgs e)
        {
            CargarCatalogos();

            if (!string.IsNullOrWhiteSpace(_curpInicial))
                txtCURP.Text = _curpInicial.Trim().ToUpperInvariant();

            if (_turnoInicial.HasValue)
            {
                txtNumeroTurno.Text = _turnoInicial.Value.ToString();
                CargarSolicitud();
            }
        }

        private void InicializarUI()
        {
            Text = "Solicitud / Edición de Turno";
            Width = 700;
            Height = 500;
            StartPosition = FormStartPosition.CenterParent;

            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 11, Padding = new Padding(10) };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

            txtCURP = new TextBox();
            txtNumeroTurno = new TextBox();
            txtPersona = new TextBox();
            txtParentesco = new TextBox();
            txtObservaciones = new TextBox { Multiline = true, Height = 80 };

            cbMunicipio = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbAsunto = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbEstatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbEstatus.Items.AddRange(new object[] { "Pendiente", "Resuelto", "Cancelado" });
            cbEstatus.SelectedItem = "Pendiente";

            btnCargar = new Button { Text = "Cargar por Turno" };
            btnGuardar = new Button { Text = "Guardar" };
            btnCargar.Click += (s, e) => CargarSolicitud();
            btnGuardar.Click += (s, e) => Guardar();

            AgregarFila(panel, "CURP:", txtCURP);
            AgregarFila(panel, "Número de Turno (opcional):", txtNumeroTurno);
            AgregarFila(panel, "", btnCargar);
            AgregarFila(panel, "Municipio:", cbMunicipio);
            AgregarFila(panel, "Asunto:", cbAsunto);
            AgregarFila(panel, "Persona Tramitera:", txtPersona);
            AgregarFila(panel, "Parentesco:", txtParentesco);
            AgregarFila(panel, "Estatus:", cbEstatus);
            AgregarFila(panel, "Observaciones:", txtObservaciones);
            AgregarFila(panel, "", btnGuardar);

            Controls.Add(panel);
        }

        private void CargarCatalogos()
        {
            using (var ctx = new AplicacionDbContext())
            {
                cbMunicipio.DataSource = ctx.Municipios.AsNoTracking().OrderBy(x => x.Nombre).ToList();
                cbMunicipio.DisplayMember = "Nombre";
                cbMunicipio.ValueMember = "IdMunicipio";

                cbAsunto.DataSource = ctx.Asuntos.AsNoTracking().Where(x => x.Activo).OrderBy(x => x.Descripcion).ToList();
                cbAsunto.DisplayMember = "Descripcion";
                cbAsunto.ValueMember = "IdAsunto";
            }
        }

        private void CargarSolicitud()
        {
            int turno;
            if (!int.TryParse(txtNumeroTurno.Text, out turno))
            {
                MessageBox.Show("Capture un número de turno válido.", "Validación");
                return;
            }

            using (var ctx = new AplicacionDbContext())
            {
                var sol = ctx.SolicitudesTurno.AsNoTracking().FirstOrDefault(x => x.NumeroTurno == turno);
                if (sol == null)
                {
                    MessageBox.Show("No existe solicitud con ese turno.", "Info");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtCURP.Text) &&
                    !string.Equals(txtCURP.Text.Trim().ToUpperInvariant(), sol.CURP, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("El turno existe, pero no corresponde a la CURP capturada.", "Validación");
                    return;
                }

                txtCURP.Text = sol.CURP;
                cbMunicipio.SelectedValue = sol.IdMunicipio;
                cbAsunto.SelectedValue = sol.IdAsunto;
                txtPersona.Text = sol.PersonaTramitera;
                txtParentesco.Text = sol.Parentesco;
                cbEstatus.SelectedItem = sol.Estatus;
                txtObservaciones.Text = sol.Observaciones;
            }
        }

        private void Guardar()
        {
            string curp = (txtCURP.Text ?? "").Trim().ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(curp))
            {
                MessageBox.Show("La CURP es obligatoria.", "Validación");
                return;
            }

            using (var ctx = new AplicacionDbContext())
            {
                var est = ctx.Estudiantes.FirstOrDefault(x => x.CURP == curp);
                if (est == null)
                {
                    MessageBox.Show("No existe estudiante con esa CURP. Regístralo primero.", "Validación");
                    return;
                }

                SolicitudTurno sol = null;
                int turno;
                if (int.TryParse(txtNumeroTurno.Text, out turno))
                    sol = ctx.SolicitudesTurno.FirstOrDefault(x => x.NumeroTurno == turno && x.CURP == curp);

                bool esNueva = false;
                if (sol == null)
                {
                    sol = new SolicitudTurno
                    {
                        CURP = curp,
                        FechaSolicitud = DateTime.Now
                    };
                    ctx.SolicitudesTurno.Add(sol);
                    esNueva = true;
                }

                sol.IdMunicipio = (int)cbMunicipio.SelectedValue;
                sol.IdAsunto = (int)cbAsunto.SelectedValue;
                sol.PersonaTramitera = (txtPersona.Text ?? "").Trim();
                sol.Parentesco = (txtParentesco.Text ?? "").Trim();
                sol.Estatus = cbEstatus.SelectedItem?.ToString() ?? "Pendiente";
                sol.Observaciones = (txtObservaciones.Text ?? "").Trim();

                if (sol.Estatus == "Resuelto" && !sol.FechaResolucion.HasValue)
                    sol.FechaResolucion = DateTime.Now;

                if (sol.Estatus != "Resuelto")
                    sol.FechaResolucion = null;

                ctx.SaveChanges();

                MessageBox.Show(esNueva
                    ? $"Solicitud creada. Turno asignado: {sol.NumeroTurno}"
                    : "Solicitud actualizada correctamente.", "Éxito");
                txtNumeroTurno.Text = sol.NumeroTurno.ToString();
            }
        }

        private void AgregarFila(TableLayoutPanel panel, string etiqueta, Control control)
        {
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.Controls.Add(new Label { Text = etiqueta, AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
            panel.Controls.Add(control);
        }
    }
}