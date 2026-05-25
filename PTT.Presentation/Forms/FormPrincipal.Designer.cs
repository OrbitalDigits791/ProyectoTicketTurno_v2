
namespace PTT.Presentation.Forms
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.buttonRegistrarEstudiante = new System.Windows.Forms.Button();
            this.buttonSolicitarTurno = new System.Windows.Forms.Button();
            this.buttonConsultarTurno = new System.Windows.Forms.Button();
            this.buttonDashboard = new System.Windows.Forms.Button();
            this.buttonSalir = new System.Windows.Forms.Button();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // labelTitulo
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitulo.Location = new System.Drawing.Point(50, 30);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(300, 26);
            this.labelTitulo.TabIndex = 0;
            this.labelTitulo.Text = "Sistema de Gestión de Turnos";

            // buttonRegistrarEstudiante
            this.buttonRegistrarEstudiante.Location = new System.Drawing.Point(50, 80);
            this.buttonRegistrarEstudiante.Name = "buttonRegistrarEstudiante";
            this.buttonRegistrarEstudiante.Size = new System.Drawing.Size(200, 50);
            this.buttonRegistrarEstudiante.TabIndex = 1;
            this.buttonRegistrarEstudiante.Text = "Registrar Estudiante";
            this.buttonRegistrarEstudiante.UseVisualStyleBackColor = true;
            this.buttonRegistrarEstudiante.Click += new System.EventHandler(this.buttonRegistrarEstudiante_Click);

            // buttonSolicitarTurno
            this.buttonSolicitarTurno.Location = new System.Drawing.Point(50, 150);
            this.buttonSolicitarTurno.Name = "buttonSolicitarTurno";
            this.buttonSolicitarTurno.Size = new System.Drawing.Size(200, 50);
            this.buttonSolicitarTurno.TabIndex = 2;
            this.buttonSolicitarTurno.Text = "Solicitar Turno";
            this.buttonSolicitarTurno.UseVisualStyleBackColor = true;
            this.buttonSolicitarTurno.Click += new System.EventHandler(this.buttonSolicitarTurno_Click);

            // buttonConsultarTurno
            this.buttonConsultarTurno.Location = new System.Drawing.Point(50, 220);
            this.buttonConsultarTurno.Name = "buttonConsultarTurno";
            this.buttonConsultarTurno.Size = new System.Drawing.Size(200, 50);
            this.buttonConsultarTurno.TabIndex = 3;
            this.buttonConsultarTurno.Text = "Consultar Turno";
            this.buttonConsultarTurno.UseVisualStyleBackColor = true;
            this.buttonConsultarTurno.Click += new System.EventHandler(this.buttonConsultarTurno_Click);

            // buttonDashboard
            this.buttonDashboard.Location = new System.Drawing.Point(50, 290);
            this.buttonDashboard.Name = "buttonDashboard";
            this.buttonDashboard.Size = new System.Drawing.Size(200, 50);
            this.buttonDashboard.TabIndex = 4;
            this.buttonDashboard.Text = "Dashboard";
            this.buttonDashboard.UseVisualStyleBackColor = true;
            this.buttonDashboard.Click += new System.EventHandler(this.buttonDashboard_Click);

            // buttonSalir
            this.buttonSalir.Location = new System.Drawing.Point(50, 360);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(200, 50);
            this.buttonSalir.TabIndex = 5;
            this.buttonSalir.Text = "Salir";
            this.buttonSalir.UseVisualStyleBackColor = true;
            this.buttonSalir.Click += new System.EventHandler(this.buttonSalir_Click);

            // FormPrincipal
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.buttonSalir);
            this.Controls.Add(this.buttonDashboard);
            this.Controls.Add(this.buttonConsultarTurno);
            this.Controls.Add(this.buttonSolicitarTurno);
            this.Controls.Add(this.buttonRegistrarEstudiante);
            this.Controls.Add(this.labelTitulo);
            this.Name = "FormPrincipal";
            this.Text = "FormPrincipal";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button buttonRegistrarEstudiante;
        private System.Windows.Forms.Button buttonSolicitarTurno;
        private System.Windows.Forms.Button buttonConsultarTurno;
        private System.Windows.Forms.Button buttonDashboard;
        private System.Windows.Forms.Button buttonSalir;
        private System.Windows.Forms.Label labelTitulo;
    }
}