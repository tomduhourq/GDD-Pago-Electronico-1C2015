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
using PagoElectronico.Models.Utils;

namespace PagoElectronico.ABM_Cliente
{
    public partial class FormAltaCliente : Form
    {
        private Cliente cliente { get; set; }
        private DAOCliente dao = new DAOCliente();
        
        public FormAltaCliente(Cliente aCliente)
        {
            cliente = aCliente;
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void btnCrear_Click(object sender, EventArgs e)
        {
            cliente.nombre = txtNombre.Text;
            cliente.apellido = txtApellido.Text;
            cliente.mail = txtMail.Text;
            cliente.fecha_nac = dateNacimiento.Value.Date;
            cliente.dom_calle = txtCalle.Text;
            cliente.dom_nro = Convert.ToInt32(txtNumero.Text);
            cliente.dom_piso = Convert.ToInt32(txtPiso.Text);
            cliente.dom_dpto = Convert.ToChar(txtDepto.Text);
            cliente.tipo_documento = Convert.ToInt32(txtTipoID.Text);
            cliente.documento = Convert.ToInt32(txtNumID.Text);
            //cliente.nacionalidad; //TODO: Ejecutar query que me traiga la id del pais con la descripcion del txt


            // Alta o modificación
            if (cliente.id == null)
            {
                if (dao.create(cliente)) MessageBox.Show("Cliente creado correctamente");
            }
            else
            {
                if (dao.update(cliente)) MessageBox.Show("Cliente modificado correctamente");
            }
        }

        private void FormAltaCliente_Load(object sender, EventArgs e)
        {

        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Utils.isCaracterInvalido(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

    
}
