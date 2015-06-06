using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Models;
using PagoElectronico.Login;
using PagoElectronico.Models.BO;
using PagoElectronico.ABM_Rol;
using PagoElectronico.ABM_Cliente;

namespace PagoElectronico
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal(Usuario invoker)
        {
            InitializeComponent();
            user = invoker;
        }

        public Usuario user { get; set; }
        private Rol rol { get; set; }
        private List<Rol> lstRoles = new List<Rol>();

        private void frmPrincipal_Load_1(object sender, EventArgs e)
        {
            lstRoles = Usuarios.ObtenerRoles(user);
            if (lstRoles.Count > 0)
            {
                // Enable selección de rol
                lblSeleccionRol.Visible = true;
                cmbRol.Visible = true;

                // Populo el combo
                cmbRol.DataSource = lstRoles;
                cmbRol.DisplayMember = "nombre";
                cmbRol.ValueMember = "id";
            } else {
                MessageBox.Show("El usuario no tiene ningun rol asignado!", "Error!", MessageBoxButtons.OK);
                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }
        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si seleccionó Admin
            if (cmbRol.SelectedIndex == 0){
                groupCliente.Visible = true;
                groupAdmin.Visible = true;
            } else { groupAdmin.Visible = false; groupCliente.Visible = true; }
        }

        // Inicializar la ventana
        private void btnABMRol_Click(object sender, EventArgs e)
        {
            frmABMRol rol = new frmABMRol();
            rol.Show();
        }

        private void btnABMCliente_Click(object sender, EventArgs e)
        {
            frmABMCliente cliente = new frmABMCliente();
            cliente.Show();
        }

   

    }
}
