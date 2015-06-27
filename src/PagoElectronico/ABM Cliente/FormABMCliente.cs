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
    public partial class FormABMCliente : Form
    {
        public FormABMCliente()
        {
            InitializeComponent();
            lstClientes = new List<Cliente>();
            dao = new DAOCliente();
            lstTipos = new List<TipoDocumento>();
            cargarGrilla();
        }

        private DAOCliente dao;
        private List<Cliente> lstClientes { get; set; }
        private List<TipoDocumento> lstTipos;

        private void frmABMCliente_Load(object sender, EventArgs e)
        {
            lstTipos = TipoDocumento.ObtenerTiposDocumento();

            if (lstTipos.Count > 0)
            {
                cmbTipoID.Visible = true;
                cmbTipoID.DataSource = lstTipos;
                cmbTipoID.DisplayMember = "descripcion";
                cmbTipoID.ValueMember = "id";
                cmbTipoID.SelectedIndex = -1;

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

        private void txtNumID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Utils.isCaracterInvalido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            FormAltaCliente ac = new FormAltaCliente();
            ac.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            FormModifCliente mc = new FormModifCliente();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lstClientes = dao.search(txtNombre.Text, txtApellido.Text, cmbTipoID.SelectedText, txtNumID.Text, txtEmail.Text);
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Documento");
            table.Columns.Add("Nombre");
            table.Columns.Add("Apellido");
            table.Columns.Add("Mail");

            DataRow newRow;

            foreach (Cliente cli in lstClientes)
            {
                newRow = table.NewRow();
                newRow["ID"] = cli.id;
                newRow["Documento"] = cli.documento;
                newRow["Nombre"] = cli.nombre;
                newRow["Apellido"] = cli.apellido;
                newRow["Mail"] = cli.mail;

                table.Rows.Add(newRow);
            }

            dtgCliente.DataSource = table;
            dtgCliente.Columns["ID"].Width = 40;
        }


        private void cmbTipoID_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
