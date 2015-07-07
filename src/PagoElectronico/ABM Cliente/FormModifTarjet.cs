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

namespace PagoElectronico.ABM_Cliente
{
    public partial class FormModifTarjet : Form
    {

        private Tarjeta tarjeta { get; set; }
        private DAOTarjeta daoTarjeta = new DAOTarjeta();
        private bool update { get; set; }

        public FormModifTarjet(Tarjeta tar)
        {
            tarjeta = tar;
            InitializeComponent();
            
        }

        private void FormModifTarjeta_Load(object sender, EventArgs e)
        {
            
            if (tarjeta.numero != null)
            {               
                update = true;
                txtNumero.Enabled = false;
                txtNumero.Text = tarjeta.Visualize;
                dateEmision.Value = (DateTime)tarjeta.fecha_emision;
                dateVencimiento.Value = (DateTime)tarjeta.fecha_vencimiento;
                txtCodigo.Text = tarjeta.cod_seguridad.ToString();
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            tarjeta.fecha_emision = dateEmision.Value.Date;
            tarjeta.fecha_vencimiento = dateVencimiento.Value.Date;
            tarjeta.cod_seguridad = (int?)Convert.ToInt32(txtCodigo.Text);
            if (update)
            {
                if (daoTarjeta.update(tarjeta))
                {
                    MessageBox.Show("Tarjeta actualizada correctamente");
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
               
            }

        }
    }
}
