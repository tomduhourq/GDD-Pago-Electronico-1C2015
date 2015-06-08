using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.DAO;
using PagoElectronico.Shared;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormABMCuenta : Form
    {
        private Cliente cli;

        public FormABMCuenta()
        {
            InitializeComponent();
            activarABM(false);
            
        }

        public FormABMCuenta(Cliente _cli):this()
        {
            cli = _cli;
            tbCliente.Text = String.Format("{0} {1}", cli.nombre, cli.apellido);
            activarABM(true);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormAltaCuenta frmAlta = new FormAltaCuenta(cli);
            frmAlta.ShowDialog();
        }

        private void btnBuscarCli_Click(object sender, EventArgs e)
        {
            BuscadorCliente buscadorCliente = new BuscadorCliente();
            buscadorCliente.ShowDialog();

            if (buscadorCliente.getCliente() != null)
            {
                cli = buscadorCliente.getCliente();
                tbCliente.Text = String.Format("{0} {1}", cli.nombre, cli.apellido);
                activarABM(true);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            FormAltaCuenta frmAlta = new FormAltaCuenta(cli, new Cuenta()); // SUPERHARDCODED
            frmAlta.ShowDialog();
        }

        private void btnEliminarCuenta_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta seguro que quiere eliminar la cuenta Nro: " + 1 + "?",  //REHARDCODED
                "Eliminar Cuenta",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FormABMCuenta_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void activarABM(bool value)
        {
            btnCrear.Enabled = value;
            btnModificar.Enabled = value;
            btnEliminarCuenta.Enabled = value;
        }

    }
}
