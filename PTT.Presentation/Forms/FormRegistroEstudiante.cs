using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using ProyectoTicketTurno.Business.Models;
using ProyectoTicketTurno.Data.Context;
using ProyectoTicketTurno.Infrastructure.Utilities;

namespace PTT.Presentation.Forms
{
    public class FormRegistroEstudiante : Form
    {
        private readonly string _curpInicial;
        private TextBox txtCURP, txtNombre, txtApPat, txtApMat, txtTelefono;
        private DateTimePicker dtpNacimiento;
        private ComboBox cbSexo, cbEstado, cbMunicipio, cbNivel;
        private NumericUpDown nudGrado;
        private Button btnBuscar, btnGuardar;

        public FormRegistroEstudiante(string curpInicial = null)
        {
            _curpInicial = curpInicial;
            InicializarUI();
            Load += FormRegistroEstudiante_Load;
        }

        private void FormRegistroEstudiante_Load(object sender, EventArgs e)
        {
            CargarCatalogos();
            if (!string.IsNullOrWhiteSpace(_curpInicial))
            {
                txtCURP.Text = _curpInicial.Trim().ToUpperInvariant();
                BuscarPorCurp();
            }
        }

        private void InicializarUI()
        {
            Text = "Registro / Edición de Estudiante";
            Width = 700;
            Height = 520;
            StartPosition = FormStartPosition.CenterParent;

            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 12, Padding = new Padding(10) };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

            txtCURP = new TextBox();
            txtNombre = new TextBox();
            txtApPat = new TextBox();
            txtApMat = new TextBox();
            txtTelefono = new TextBox();
            dtpNacimiento = new DateTimePicker { Format = DateTimePickerFormat.Short };
            cbSexo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbEstado = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbMunicipio = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cbNivel = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            nudGrado = new NumericUpDown { Minimum = 1, Maximum = 6, Value = 1 };

            cbSexo.Items.AddRange(new object[] { "H", "M" });
            cbSexo.SelectedIndex = 0;

            btnBuscar = new Button { Text = "Buscar CURP" };
            btnGuardar = new Button { Text = "Guardar" };
            btnBuscar.Click += (s, e) => BuscarPorCurp();
            btnGuardar.Click += (s, e) => Guardar();

            AgregarFila(panel, "CURP:", txtCURP);
            AgregarFila(panel, "", btnBuscar);
            AgregarFila(panel, "Nombre:", txtNombre);
            AgregarFila(panel, "Apellido Paterno:", txtApPat);
            AgregarFila(panel, "Apellido Materno:", txtApMat);
            AgregarFila(panel, "Fecha Nacimiento:", dtpNacimiento);
            AgregarFila(panel, "Sexo:", cbSexo);
            AgregarFila(panel, "Estado Nacimiento:", cbEstado);
            AgregarFila(panel, "Municipio Estudio:", cbMunicipio);
            AgregarFila(panel, "Nivel Educativo:", cbNivel);
            AgregarFila(panel, "Grado:", nudGrado);
            AgregarFila(panel, "Teléfono:", txtTelefono);
            AgregarFila(panel, "", btnGuardar);

            Controls.Add(panel);
        }

        private void CargarCatalogos()
        {
            using (var ctx = new AplicacionDbContext())
            {
                cbEstado.DataSource = ctx.Estados.AsNoTracking().OrderBy(x => x.Nombre).ToList();
                cbEstado.DisplayMember = "Nombre";
                cbEstado.ValueMember = "Clave";

                cbMunicipio.DataSource = ctx.Municipios.AsNoTracking().OrderBy(x => x.Nombre).ToList();
                cbMunicipio.DisplayMember = "Nombre";
                cbMunicipio.ValueMember = "IdMunicipio";

                cbNivel.DataSource = ctx.NivelesEducativos.AsNoTracking().OrderBy(x => x.Nombre).ToList();
                cbNivel.DisplayMember = "Nombre";
                cbNivel.ValueMember = "IdNivelEducativo";
            }
        }

        private void BuscarPorCurp()
        {
            string curp = (txtCURP.Text ?? "").Trim().ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(curp)) return;

            using (var ctx = new AplicacionDbContext())
            {
                var est = ctx.Estudiantes.AsNoTracking().FirstOrDefault(x => x.CURP == curp);
                if (est == null)
                {
                    MessageBox.Show("No existe estudiante con esa CURP. Se registrará nuevo.", "Info");
                    return;
                }

                txtNombre.Text = est.Nombre;
                txtApPat.Text = est.ApellidoPaterno;
                txtApMat.Text = est.ApellidoMaterno;
                dtpNacimiento.Value = est.FechaNacimiento;
                cbSexo.SelectedItem = est.Sexo.ToString();
                cbEstado.SelectedValue = est.EstadoNacimiento;
                cbMunicipio.SelectedValue = est.MunicipioEstudio;
                cbNivel.SelectedValue = est.IdNivelEducativo;
                nudGrado.Value = est.Grado;
                txtTelefono.Text = est.TelefonoContacto;
            }
        }

        private void Guardar()
        {
            string curp = (txtCURP.Text ?? "").Trim().ToUpperInvariant();
            string nombre = (txtNombre.Text ?? "").Trim();
            string apPat = (txtApPat.Text ?? "").Trim();
            string apMat = (txtApMat.Text ?? "").Trim();
            string sexoTxt = cbSexo.SelectedItem?.ToString() ?? "H";
            char sexo = sexoTxt[0];
            string estado = cbEstado.SelectedValue?.ToString() ?? "NE";

            if (!FormatoCURPHelper.ValidarFormatoCURP(curp, nombre, apPat, apMat, dtpNacimiento.Value, sexo, estado))
            {
                MessageBox.Show("CURP inválida o no coherente con los datos capturados.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int edad = CalcularEdad(dtpNacimiento.Value.Date);

            using (var ctx = new AplicacionDbContext())
            {
                var existente = ctx.Estudiantes.FirstOrDefault(x => x.CURP == curp);

                if (existente == null)
                {
                    existente = new Estudiante { CURP = curp, FechaRegistro = DateTime.Now };
                    ctx.Estudiantes.Add(existente);
                }

                existente.Nombre = nombre;
                existente.ApellidoPaterno = apPat;
                existente.ApellidoMaterno = apMat;
                existente.FechaNacimiento = dtpNacimiento.Value.Date;
                existente.Sexo = sexo;
                existente.Edad = edad;
                existente.EstadoNacimiento = estado;
                existente.MunicipioEstudio = (int)cbMunicipio.SelectedValue;
                existente.IdNivelEducativo = (int)cbNivel.SelectedValue;
                existente.Grado = (byte)nudGrado.Value;
                existente.TelefonoContacto = (txtTelefono.Text ?? "").Trim();

                ctx.SaveChanges();
            }

            MessageBox.Show("Estudiante guardado correctamente.", "Éxito");
        }

        private static int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }

        private void AgregarFila(TableLayoutPanel panel, string etiqueta, Control control)
        {
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.Controls.Add(new Label { Text = etiqueta, AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
            panel.Controls.Add(control);
        }
    }
}