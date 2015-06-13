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

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormAltaCuenta : Form
    {
        List<Pais> paises;
        List<EstadoCuenta> estados;
        List<Moneda> monedas;
        List<TipoCuenta> cuentas;
        List<Banco> bancos;

        public FormAltaCuenta(Cliente cli)
        {
            InitializeComponent();
            tbCliente.Text = String.Format("{0} {1}", cli.nombre,cli.apellido);
            tbNroCuenta.Text = new DAOCuenta().proximoNumeroDeCuenta().ToString();
            cargarCombos();
        }

        public FormAltaCuenta(Cliente cli, Cuenta cuenta):this(cli)
        {
            this.Text = "Modificar Cuenta";
            cbEstado.Enabled = false;
            volcarDatosCliente(cuenta);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void cargarCombos(){

            estados = new DAOEstadoCuenta().retrieveBase();
            cuentas = new DAOTipoCuenta().retrieveBase();
            paises = new DAOPais().retrieveBase();
            monedas = new DAOMoneda().retrieveBase();
            bancos = new DAOBanco().retrieveBase();

            cbPais.Items.AddRange(paises.ToArray());
            cbTipoCuenta.Items.AddRange(cuentas.ToArray());
            cbMoneda.Items.AddRange(monedas.ToArray());
            cbEstado.Items.AddRange(estados.ToArray());
            cbBanco.Items.AddRange(bancos.ToArray());
        }

        private void volcarDatosCliente(Cuenta cuenta){
            
            tbNroCuenta.Text = cuenta.numCuenta.ToString();
            dtFechaApertura.Value = (DateTime)cuenta.fechaCreacion;
            cbBanco.SelectedItem = bancos.Find(b => b.cod == cuenta.codBanco);
            cbEstado.SelectedItem = estados.Find(e => e.id == cuenta.estado);
            cbPais.SelectedItem = paises.Find(p => p.id == cuenta.pais);
            cbMoneda.SelectedItem = monedas.Find(m => m.id == cuenta.tipoMoneda);
            cbTipoCuenta.SelectedItem = cuentas.Find(c => c.id == cuenta.tipoCuenta);
        }
    }
}
