using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Models.BO;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormAltaCuenta : Form
    {
        public FormAltaCuenta(Cliente cli)
        {
            InitializeComponent();
            tbCliente.Text = String.Format("{0} {1}", cli.nombre,cli.apellido);
        }

        public FormAltaCuenta(Cliente cli, Cuenta cuenta):this(cli)
        {
            this.Text = "Modificar Cuenta";
            tbEstado.Text = cuenta.estado.ToString();
            tbNroCuenta.Text = cuenta.numCuenta.ToString();
            dtFechaApertura.Value = (DateTime)cuenta.fechaCreacion;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
