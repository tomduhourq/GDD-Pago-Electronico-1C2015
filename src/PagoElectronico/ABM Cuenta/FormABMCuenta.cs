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
using PagoElectronico.Shared;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormABMCuenta : Form
    {
        private Cliente cli;
        private List<Cuenta> cuentas;
        private DAOCuenta daoCuenta;

        public FormABMCuenta()
        {
            InitializeComponent();
            activarABM(false);
            daoCuenta = new DAOCuenta();
            
        }

        public FormABMCuenta(Cliente _cli):this()
        {
            cli = _cli;
            tbCliente.Text = String.Format("{0} {1}", cli.nombre, cli.apellido);
            btnBuscarCli.Enabled = false;
            cargarGrilla(cli);
            activarABM(true);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormAltaCuenta frmAlta = new FormAltaCuenta(cli);
            frmAlta.ShowDialog();
        }

        private void btnBuscarCli_Click(object sender, EventArgs e)
        {
            BuscadorCliente buscadorCliente = new BuscadorCliente();
            buscadorCliente.ShowDialog();

            if (buscadorCliente.getCliente() != null)
            {
                cli = buscadorCliente.getCliente();
                tbCliente.Text = String.Format("{0} {1}", cli.nombre, cli.apellido);
                cargarGrilla(cli);
                activarABM(true);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (grillaCuentas.SelectedCells[0] == null)
            {
                MessageBox.Show("Ninguna Cuenta Seleccionada");
                return;
            }

            Cuenta seleccionada = obtenerCuentaSeleccionada();
            FormAltaCuenta frmAlta = new FormAltaCuenta(cli, seleccionada); 
            frmAlta.ShowDialog();
        }

        private void btnEliminarCuenta_Click(object sender, EventArgs e)
        {
            Cuenta seleccionada = obtenerCuentaSeleccionada();
            if (MessageBox.Show("Esta seguro que quiere eliminar la cuenta Nro: " + seleccionada.numCuenta + "?",  //REHARDCODED
                "Eliminar Cuenta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                new DAOCuenta().delete(1);
                MessageBox.Show("La Cuenta " + seleccionada.numCuenta + " Fue Eliminada");
            }

            cargarGrilla(cli);
        }

        private void FormABMCuenta_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void activarABM(bool value)
        {
            btnCrear.Enabled = value;
            btnModificar.Enabled = value;
            btnEliminarCuenta.Enabled = value;
        }

        private void cargarGrilla(Cliente cli)
        {
            cuentas = daoCuenta.retrieveBy_Cliente(cli.id);

            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Numero");
            table.Columns.Add("Tipo");
            table.Columns.Add("Estado");
            table.Columns.Add("Banco");
            table.Columns.Add("Moneda");

            DataRow newRow;

            foreach (Cuenta cuenta in cuentas)
            {
                newRow = table.NewRow();
                newRow["ID"] = cuenta.id;
                newRow["Numero"] = cuenta.numCuenta;
                newRow["Tipo"] = cuenta.tipoCuenta;
                newRow["Estado"] = cuenta.estado;

                if (cuenta.codBanco != null)
                {
                    newRow["Banco"] = cuenta.codBanco;
                }

                newRow["Moneda"] = cuenta.tipoMoneda;

                table.Rows.Add(newRow);
            }

            grillaCuentas.DataSource = table;
            grillaCuentas.Columns[0].Visible = false;
            grillaCuentas.Columns["Numero"].Width = 150;

        }

        private Cuenta obtenerCuentaSeleccionada()
        {
            DataGridViewRow row = grillaCuentas.Rows[grillaCuentas.SelectedCells[0].RowIndex];
            long cuentaID = Convert.ToInt64(row.Cells["ID"].Value);
            return daoCuenta.retrieveBy_id(cuentaID);
        }
    }
}
