using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    public partial class frmABMCliente : Form
    {
        public frmABMCliente()
        {
            InitializeComponent();
        }

        private void frmABMCliente_Load(object sender, EventArgs e)
        {
            dtgCliente.AutoGenerateColumns = false;
            dtgCliente.MultiSelect = false;
        }
        
        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

    }
}
