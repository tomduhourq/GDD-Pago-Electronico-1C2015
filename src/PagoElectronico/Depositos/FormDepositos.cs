using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Models.Utils;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.DAO;

namespace PagoElectronico.Depositos
{
    public partial class FormDepositos : Form
    {
        private Cliente cliente { get; set; }
        private List<Cuenta> cuentas { get; set; }
        private List<Tarjeta> tarjetas { get; set; }
        private List<Moneda> monedas { get; set; }
        private DAOTarjeta daoTarjeta = new DAOTarjeta();
        private DAOCuenta daoCuenta = new DAOCuenta();
        private DAOMoneda daoMoneda = new DAOMoneda();
        private DAOEstadoCuenta daoEstado = new DAOEstadoCuenta();

        public FormDepositos(Cliente cli)
        {
            cliente = cli;
            cuentas = daoCuenta
                      .retrieveByClientId(cliente.id)
                      .FindAll(cuenta => 
                          cuenta.estado == daoEstado.habilitado().id);
            ValidarHabilitadas();
            InitializeComponent();
            DeshabilitarTarjetas();
        }

        private void DeshabilitarTarjetas()
        {
            lblTarjeta.Visible = false;
            cmbTarjeta.Visible = false;
        }

        private void ValidarHabilitadas()
        {
            if (cuentas.Count() == 0)
            {
                MessageBox.Show("No posee cuentas habilitadas, esta ventana se cerrará");
                this.Close();
            }
        }

        private void FormDepositos_Load(object sender, EventArgs e)
        {
            // Traer cuentas y para la que seleccione traer la/s tarjetas asociadas
            tarjetas = daoTarjeta.retrieveByClientId(cliente.id);
            monedas = daoMoneda.retrieveBase();

            cmbCuenta.DataSource = cuentas;
            cmbCuenta.DisplayMember = "numCuenta";
            cmbCuenta.ValueMember = "id";

            cmbMoneda.DataSource = monedas;
            cmbMoneda.DisplayMember = "descripcion";
            cmbMoneda.ValueMember = "id";
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
