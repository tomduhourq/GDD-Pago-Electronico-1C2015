using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Models.BO;

namespace PagoElectronico.ABM_Cliente
{
    public partial class FormABMCliente : Form
    {
        public FormABMCliente()
        {
            InitializeComponent();
        }

        private List<TipoDocumento> lstTipos = new List<TipoDocumento>();

        private void frmABMCliente_Load(object sender, EventArgs e)
        {
            lstTipos = TipoDocumento.ObtenerTiposDocumento();

            if (lstTipos.Count > 0)
            {
                cmbTipoID.Visible = true;

                cmbTipoID.DataSource = lstTipos;
                cmbTipoID.DisplayMember = "descripcion";
                cmbTipoID.ValueMember = "id";
                Console.ReadLine();
            } else {

            }

            dtgCliente.AutoGenerateColumns = false;
            dtgCliente.MultiSelect = false;
        }
        
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNumID.Text = "";
            txtEmail.Text = "";
        }
        
        public bool isCaracterInvalido(Char c)
        {
            if (char.IsLetter(c))
            {
                return true;
            }
            return false;
        }

        private void txtNumID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isCaracterInvalido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
