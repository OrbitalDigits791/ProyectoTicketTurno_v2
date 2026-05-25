using System;
using System.Windows.Forms;
using Serilog;

namespace ProyectoTicketTurno.Presentation
{
    public partial class FormLogin : Form
    {
        private readonly ILogger _logger;
        private const string USUARIO_ADMIN = "admin";
        private const string CONTRASEÑA_ADMIN = "admin123";

        // Constructor con ILogger como parámetro
        public FormLogin(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Aplicación de Gestión de Turnos - Login";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(400, 250);
        }

        private void InitializeComponent()
        {
            // Panel principal
            var panelPrincipal = new Panel();
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.BackColor = System.Drawing.Color.White;

            // Label usuario
            var labelUsuario = new Label();
            labelUsuario.Text = "Usuario:";
            labelUsuario.Location = new System.Drawing.Point(50, 40);
            labelUsuario.Size = new System.Drawing.Size(100, 25);
            labelUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);

            // TextBox usuario
            var textBoxUsuario = new TextBox();
            textBoxUsuario.Name = "textBoxUsuario";
            textBoxUsuario.Location = new System.Drawing.Point(150, 40);
            textBoxUsuario.Size = new System.Drawing.Size(200, 25);
            textBoxUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Label contraseña
            var labelContraseña = new Label();
            labelContraseña.Text = "Contraseña:";
            labelContraseña.Location = new System.Drawing.Point(50, 80);
            labelContraseña.Size = new System.Drawing.Size(100, 25);
            labelContraseña.Font = new System.Drawing.Font("Segoe UI", 10F);

            // TextBox contraseña
            var textBoxContraseña = new TextBox();
            textBoxContraseña.Name = "textBoxContraseña";
            textBoxContraseña.Location = new System.Drawing.Point(150, 80);
            textBoxContraseña.Size = new System.Drawing.Size(200, 25);
            textBoxContraseña.UseSystemPasswordChar = true;
            textBoxContraseña.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Botón login
            var btnLogin = new Button();
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.Location = new System.Drawing.Point(150, 130);
            btnLogin.Size = new System.Drawing.Size(200, 40);
            btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnLogin.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            btnLogin.ForeColor = System.Drawing.Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Click += (s, e) => BotonLogin_Click(textBoxUsuario, textBoxContraseña);

            // Botón salir
            var btnSalir = new Button();
            btnSalir.Text = "Salir";
            btnSalir.Location = new System.Drawing.Point(150, 180);
            btnSalir.Size = new System.Drawing.Size(200, 40);
            btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnSalir.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            btnSalir.ForeColor = System.Drawing.Color.White;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Click += ButtonSalir_Click;

            panelPrincipal.Controls.Add(labelUsuario);
            panelPrincipal.Controls.Add(textBoxUsuario);
            panelPrincipal.Controls.Add(labelContraseña);
            panelPrincipal.Controls.Add(textBoxContraseña);
            panelPrincipal.Controls.Add(btnLogin);
            panelPrincipal.Controls.Add(btnSalir);

            this.Controls.Add(panelPrincipal);
        }

        private void BotonLogin_Click(TextBox usuario, TextBox contraseña)
        {
            if (usuario.Text == USUARIO_ADMIN && contraseña.Text == CONTRASEÑA_ADMIN)
            {
                _logger.Information("Login exitoso para usuario admin");
                FormPrincipal formPrincipal = new FormPrincipal();
                formPrincipal.Show();
                this.Hide();
            }
            else
            {
                _logger.Warning("Intento fallido de login");
                MessageBox.Show("Usuario o contraseña incorrectos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}