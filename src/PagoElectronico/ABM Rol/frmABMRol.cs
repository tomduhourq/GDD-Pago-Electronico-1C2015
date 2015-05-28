using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Models.BO;

namespace PagoElectronico.ABM_Rol
{
    public partial class frmABMRol : Form
    {
        public frmABMRol()
        {
            InitializeComponent();
        }

        private List<Rol> lstRoles { get; set; }
        public string operacion { get; set; }

        private void frmABMRol_Load(object sender, EventArgs e)
        {
            dtgRol.AutoGenerateColumns = false;
            dtgRol.MultiSelect = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            chkActivo.Checked = true;
            txtNombre.Text = "";
            actualizarGrilla();
        }

        public void actualizarGrilla()
        {
            // TODO: Evaluar las combinaciones y popular la grid
            dtgRol.DataSource = lstRoles;
        }

        private void cargarGrilla()
        {
            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn();
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Nombre Rol";
            colNombre.Width = 120;

            dtgRol.Columns.Add(colNombre);

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try { actualizarGrilla(); }
            catch { MessageBox.Show("No existe rol con esas caracteristicas", "Error!", MessageBoxButtons.OK); }
        }

    }
}
