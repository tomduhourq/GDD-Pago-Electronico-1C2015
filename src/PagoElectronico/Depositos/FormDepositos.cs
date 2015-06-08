using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Depositos
{
    public partial class FormDepositos : Form
    {
        public FormDepositos()
        {
            InitializeComponent();
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (!Utils.isNumeric(keypress))
            {
                MessageBox.Show(" Solo puede ingresar un número o ,!");
                txtImporte.Text = "";
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
