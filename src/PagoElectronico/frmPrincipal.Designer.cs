namespace PagoElectronico
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSeleccionRol = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.groupAdmin = new System.Windows.Forms.GroupBox();
            this.btnABMRol = new System.Windows.Forms.Button();
            this.groupCliente = new System.Windows.Forms.GroupBox();
            this.groupAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSeleccionRol
            // 
            this.lblSeleccionRol.AutoSize = true;
            this.lblSeleccionRol.Location = new System.Drawing.Point(13, 13);
            this.lblSeleccionRol.Name = "lblSeleccionRol";
            this.lblSeleccionRol.Size = new System.Drawing.Size(23, 13);
            this.lblSeleccionRol.TabIndex = 0;
            this.lblSeleccionRol.Text = "Rol";
            this.lblSeleccionRol.Visible = false;
            // 
            // cmbRol
            // 
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(61, 10);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(177, 21);
            this.cmbRol.TabIndex = 1;
            this.cmbRol.Visible = false;
            this.cmbRol.SelectedIndexChanged += new System.EventHandler(this.cmbRol_SelectedIndexChanged);
            // 
            // groupAdmin
            // 
            this.groupAdmin.Controls.Add(this.btnABMRol);
            this.groupAdmin.Location = new System.Drawing.Point(16, 37);
            this.groupAdmin.Name = "groupAdmin";
            this.groupAdmin.Size = new System.Drawing.Size(470, 95);
            this.groupAdmin.TabIndex = 3;
            this.groupAdmin.TabStop = false;
            this.groupAdmin.Text = "Funciones Administrador";
            this.groupAdmin.Visible = false;
            // 
            // btnABMRol
            // 
            this.btnABMRol.Location = new System.Drawing.Point(7, 20);
            this.btnABMRol.Name = "btnABMRol";
            this.btnABMRol.Size = new System.Drawing.Size(75, 23);
            this.btnABMRol.TabIndex = 0;
            this.btnABMRol.Text = "ABM Rol";
            this.btnABMRol.UseVisualStyleBackColor = true;
            this.btnABMRol.Click += new System.EventHandler(this.btnABMRol_Click);
            // 
            // groupCliente
            // 
            this.groupCliente.Location = new System.Drawing.Point(16, 138);
            this.groupCliente.Name = "groupCliente";
            this.groupCliente.Size = new System.Drawing.Size(470, 100);
            this.groupCliente.TabIndex = 4;
            this.groupCliente.TabStop = false;
            this.groupCliente.Text = "Funciones Cliente";
            this.groupCliente.Visible = false;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 273);
            this.Controls.Add(this.groupCliente);
            this.Controls.Add(this.groupAdmin);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.lblSeleccionRol);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selección de Funcionalidad";
            this.Load += new System.EventHandler(this.frmPrincipal_Load_1);
            this.groupAdmin.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeleccionRol;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.GroupBox groupAdmin;
        private System.Windows.Forms.GroupBox groupCliente;
        private System.Windows.Forms.Button btnABMRol;
    }
}