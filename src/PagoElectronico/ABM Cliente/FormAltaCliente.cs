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
        private DAOCliente daoCliente = new DAOCliente();
        private DAOPais daoPais = new DAOPais();
        private bool update;


        // ESTE ES EL CONSTRUCTOR PARA MODIFICAR CLIENTES
        public FormAltaCliente(Cliente cli)
        {
            cliente = cli;
            InitializeComponent();
        }

        private void cargarDatosClientes()
        {
                txtUsuario.Text = cliente.usuario;
                txtNombre.Text = cliente.nombre;
                txtApellido.Text = cliente.apellido;
                txtCalle.Text = cliente.dom_calle;
                txtDepto.Text = cliente.dom_dpto.ToString();
                txtPiso.Text = cliente.dom_piso.ToString();
                txtMail.Text = cliente.mail;
                txtNumero.Text = cliente.dom_nro.ToString();
                txtNumID.Text = cliente.documento.ToString();
                txtTipoID.Text = cliente.tipo_documento.ToString();
                dateNacimiento.Value = (DateTime)cliente.fecha_nac;
                string pa = (string)cliente.get_pais().Substring(1);            
                cbNacionalidad.SelectedIndex = cbNacionalidad.FindStringExact(pa); 
                checkActivo.Checked = (bool)cliente.activo;
        }


        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Utils.isCaracterInvalido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCrear_Click_1(object sender, EventArgs e)
        {
                cliente.nombre = txtNombre.Text;
                cliente.apellido = txtApellido.Text;
                cliente.mail = txtMail.Text;
                cliente.fecha_nac = dateNacimiento.Value.Date;
                cliente.dom_calle = txtCalle.Text;
                cliente.dom_nro = (int?)Convert.ToInt32(txtNumero.Text);
                cliente.dom_piso = Convert.ToInt32(txtPiso.Text);
                cliente.dom_dpto = txtDepto.Text;
                cliente.tipo_documento = Convert.ToInt32(txtTipoID.Text);
                cliente.documento = Convert.ToInt32(txtNumID.Text);
                cliente.nacionalidad = ((Pais)cbNacionalidad.SelectedItem).id;
                cliente.activo = (bool)checkActivo.Checked;
                if (update)
                {
                    if (daoCliente.update(cliente))
                    {
                        MessageBox.Show("Cliente actualizado correctamente");
                        this.Close();
                        return;
                    }
                    else
                    {
                        throw new Exception("Datos no se cargaron correctamente");
                    }
                }
                else
                {
                    if (daoCliente.create(cliente))
                    {
                        MessageBox.Show("Cliente creado correctamente");
                        this.Close();
                        return;
                    }

                }
             
        }
           
            
        

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargarCombos()
        {
            cbNacionalidad.Items.AddRange(daoPais.retrieveBase().ToArray());
        }

        private void FormAltaCliente_Load(object sender, EventArgs e)
        {                       
            cargarCombos();
            if (cliente.id != null)
            {
                txtUsuario.Enabled = false;
                update = true;
                cargarDatosClientes(); 
            }
        }

    }

    
}
