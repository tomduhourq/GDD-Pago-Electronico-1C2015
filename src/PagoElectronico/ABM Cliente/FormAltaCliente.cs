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

        public FormAltaCliente()
        {
            cliente = new Cliente();
            
            InitializeComponent();
            cargarCombos();
        }

        // ESTE ES EL CONSTRUCTOR PARA MODIFICAR CLIENTES
        public FormAltaCliente(Cliente cli):this()
        {
            cliente = cli;

            // Bloqueamos todos los campos que NO se pueden editar
            txtUsuario.Enabled = false;

            // TODO cargar todos los datos del cliente para editar
            cargarDatosClientes();

            InitializeComponent();
        }

        private void cargarDatosClientes()
        {
            if (cliente.id != null)
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
                cbNacionalidad.SelectedItem = cliente.nacionalidad;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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
            try
            {
                cliente.nombre = txtNombre.Text;
                cliente.apellido = txtApellido.Text;
                cliente.mail = txtMail.Text;
                cliente.fecha_nac = dateNacimiento.Value.Date;
                cliente.dom_calle = txtCalle.Text;
                cliente.dom_nro = Convert.ToInt32(txtNumero.Text);
                cliente.dom_piso = Convert.ToInt32(txtPiso.Text);
                cliente.dom_dpto = txtDepto.Text;
                cliente.tipo_documento = Convert.ToInt32(txtTipoID.Text);
                cliente.documento = Convert.ToInt32(txtNumID.Text);
                cliente.nacionalidad = ((Pais)cbNacionalidad.SelectedItem).id;

                if (daoCliente.create(cliente))
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

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargarCombos()
        {
            cbNacionalidad.Items.AddRange(daoPais.retrieveBase().ToArray());
        }
    }

    
}
