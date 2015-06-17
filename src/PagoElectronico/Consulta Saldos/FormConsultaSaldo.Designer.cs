namespace PagoElectronico.Consulta_Saldos
{
    partial class FormConsultaSaldo
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgRetiros = new System.Windows.Forms.DataGridView();
            this.dgDepositos = new System.Windows.Forms.DataGridView();
            this.dgTransferencias = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCuenta = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgRetiros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDepositos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransferencias)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ultimas 10 Transferencias de Fondos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ultimos 5 Depositos";
            // 
            // dgRetiros
            // 
            this.dgRetiros.AllowUserToAddRows = false;
            this.dgRetiros.AllowUserToDeleteRows = false;
            this.dgRetiros.AllowUserToOrderColumns = true;
            this.dgRetiros.AllowUserToResizeColumns = false;
            this.dgRetiros.AllowUserToResizeRows = false;
            this.dgRetiros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRetiros.Location = new System.Drawing.Point(398, 62);
            this.dgRetiros.MultiSelect = false;
            this.dgRetiros.Name = "dgRetiros";
            this.dgRetiros.ReadOnly = true;
            this.dgRetiros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRetiros.Size = new System.Drawing.Size(386, 118);
            this.dgRetiros.TabIndex = 3;
            // 
            // dgDepositos
            // 
            this.dgDepositos.AllowUserToAddRows = false;
            this.dgDepositos.AllowUserToDeleteRows = false;
            this.dgDepositos.AllowUserToOrderColumns = true;
            this.dgDepositos.AllowUserToResizeColumns = false;
            this.dgDepositos.AllowUserToResizeRows = false;
            this.dgDepositos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDepositos.Location = new System.Drawing.Point(15, 62);
            this.dgDepositos.MultiSelect = false;
            this.dgDepositos.Name = "dgDepositos";
            this.dgDepositos.ReadOnly = true;
            this.dgDepositos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDepositos.Size = new System.Drawing.Size(368, 118);
            this.dgDepositos.TabIndex = 4;
            // 
            // dgTransferencias
            // 
            this.dgTransferencias.AllowUserToAddRows = false;
            this.dgTransferencias.AllowUserToDeleteRows = false;
            this.dgTransferencias.AllowUserToOrderColumns = true;
            this.dgTransferencias.AllowUserToResizeColumns = false;
            this.dgTransferencias.AllowUserToResizeRows = false;
            this.dgTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTransferencias.Location = new System.Drawing.Point(15, 221);
            this.dgTransferencias.MultiSelect = false;
            this.dgTransferencias.Name = "dgTransferencias";
            this.dgTransferencias.ReadOnly = true;
            this.dgTransferencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTransferencias.Size = new System.Drawing.Size(769, 160);
            this.dgTransferencias.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(395, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ultimos 5 Retiros";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cuenta:";
            // 
            // tbCuenta
            // 
            this.tbCuenta.Enabled = false;
            this.tbCuenta.Location = new System.Drawing.Point(62, 9);
            this.tbCuenta.Name = "tbCuenta";
            this.tbCuenta.Size = new System.Drawing.Size(261, 20);
            this.tbCuenta.TabIndex = 7;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(301, 388);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(160, 23);
            this.btnCerrar.TabIndex = 8;
            this.btnCerrar.Text = "Volver a las cuentas";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FormConsultaSaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 421);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.tbCuenta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgTransferencias);
            this.Controls.Add(this.dgDepositos);
            this.Controls.Add(this.dgRetiros);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormConsultaSaldo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consula de Saldo de Cuenta";
            ((System.ComponentModel.ISupportInitialize)(this.dgRetiros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDepositos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransferencias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgRetiros;
        private System.Windows.Forms.DataGridView dgDepositos;
        private System.Windows.Forms.DataGridView dgTransferencias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCuenta;
        private System.Windows.Forms.Button btnCerrar;
    }
}