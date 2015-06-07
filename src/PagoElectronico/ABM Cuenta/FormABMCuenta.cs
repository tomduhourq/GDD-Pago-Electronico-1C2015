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

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormABMCuenta : Form
    {
        private Cliente cli;

        public FormABMCuenta()
        {
            InitializeComponent();
        }

        public FormABMCuenta(Cliente _cli):this()
        {
            cli = _cli;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormAltaCuenta frmAlta = new FormAltaCuenta(cli);
        }

        private void btnBuscarCli_Click(object sender, EventArgs e)
        {
            cli = new DAOCliente().retrieveBy_id(1); //superHARDCODED

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            FormAltaCuenta frmAlta = new FormAltaCuenta(cli, new Cuenta()); // SUPERHARDCODED
        }

        private void btnEliminarCuenta_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta seguro que quiere eliminar la cuenta nro:" + 1,  //REHARDCODED
                "Eliminar Cuenta",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
