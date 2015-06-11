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
using PagoElectronico.ABM_Cuenta;
using PagoElectronico.Models.DAO;
using PagoElectronico.Depositos;

namespace PagoElectronico
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal(Usuario invoker)
        {
            InitializeComponent();
            user = invoker;
        }

        public Usuario user { get; set; }
        private Rol rol { get; set; }
        private List<Rol> lstRoles = new List<Rol>();

        private void frmPrincipal_Load_1(object sender, EventArgs e)
        {
            lstRoles = Usuario.ObtenerRoles(user);
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
                FormLogin login = new FormLogin();
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
            FormABMRol rol = new FormABMRol();
            rol.Show();
        }

        private void btnABMCliente_Click(object sender, EventArgs e)
        {
            FormABMCliente cliente = new FormABMCliente();
            cliente.Show();
        }

        private void btnABMCuentas_Click(object sender, EventArgs e)
        {
            FormABMCuenta frmCuentas = new FormABMCuenta();
            frmCuentas.Show();
        }

        private void btnDepositos_Click(object sender, EventArgs e)
        {
            DAOCliente dao = new DAOCliente();
            FormDepositos frmDepo = new FormDepositos(dao.retrieveBy_user(user.Name));
            frmDepo.Show();
        }

   

    }
}
