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
        }

        public FormAltaCuenta(Cliente cli, Cuenta cuenta):this(cli)
        {
            this.Text = "Modificar Cuenta";
        }
    }
}
