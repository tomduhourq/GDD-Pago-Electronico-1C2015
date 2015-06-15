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
        private DAOPais dao_pais = new DAOPais();

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
            //cliente.nacionalidad = dao_pais.retrieveBy_name(txtNacionalidad.Text).id;

            if (dao.create(cliente))
            {
                MessageBox.Show("Cliente creado correctamente");
            }
            else
            {
                MessageBox.Show("");
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

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            try
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
                cliente.nacionalidad = dao_pais.retrieveBy_name(txtNacionalidad.Text);

                if (cliente.nacionalidad == -1)
                {
                    Pais pais = new Pais();
                    pais.descripcion = txtNacionalidad.Text;
                    Pais pais_creado = dao_pais.create(pais);
                    cliente.nacionalidad = pais_creado.id;
                }    
                if (dao.create(cliente))
                {
                    MessageBox.Show("Cliente creado correctamente");
                    this.Close();
                    return;
                }
                else
                {
                    throw new Exception("Datos no se cargaron correctamente");
                }
            }
            catch
            {
                MessageBox.Show("Los datos no se han ingresado correctamente");
                return;
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dateNacimiento_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNacionalidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPiso_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCalle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDepto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTipoID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

    
}
