using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Listados
{
    public partial class FormListados : Form
    {
        public FormListados()
        {
            InitializeComponent();
        }

        private void FormListados_Load(object sender, EventArgs e)
        {
            for (int i = 2015; i > 1995; i--)
            {
                cbAnio.Items.Add(i);
            }
            for (int i = 1; i < 5; i++)
            {
                cbTrimestre.Items.Add(i);
            }

            cbListado.Items.Add("Clientes con cuentas inhabilitadas por no pago");
            cbListado.Items.Add("Cliente con mayor cantidad de comisiones facturadas en sus cuentas");
            cbListado.Items.Add("Clientes con mayor cantidad de transacciones entre sus cuentas");
            cbListado.Items.Add("Países con mayor cantidad de movimientos");
            cbListado.Items.Add("Total facturado para los distintos tipos de cuentas");
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (cbListado.SelectedIndex < 0 || cbAnio.SelectedIndex < 0 || cbTrimestre.SelectedIndex < 0) {
                MessageBox.Show("Seleccione todos los campos para realizar la busqueda");
                return;
            }

            int ano = (int)cbAnio.SelectedItem;
            int trimestre = (int)cbTrimestre.SelectedItem;
            switch (cbListado.SelectedIndex)
            {
                case 0:
                    clientesConCuentasSinPagar(ano, trimestre);
                    break;
                case 1:
                    clientesConMasComisionesEntreCuentas(ano, trimestre);
                    break;
                case 2:
                    clientesConMasTransaccionesEntreSusCuentas(ano, trimestre);
                    break;
                case 3:
                    PaisesConMasMovimientos(ano, trimestre);
                    break;
                case 4:
                    TotalFacturadoTiposDeCuentas(ano, trimestre);
                    break;
                default:
                    MessageBox.Show("Listado no reconocido");
                    break;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cbAnio.SelectedIndex = -1;
            cbListado.SelectedIndex = -1;
            cbTrimestre.SelectedIndex = -1;
            dgResult.DataSource = null;
        }

        public void clientesConCuentasSinPagar(int ano, int trimestre) 
        { 
        }

        public void clientesConMasComisionesEntreCuentas(int ano, int trimestre)
        { 
        }

        public void clientesConMasTransaccionesEntreSusCuentas(int ano, int trimestre)
        { 
        }

        public void PaisesConMasMovimientos(int ano, int trimestre)
        { 
        }

        public void TotalFacturadoTiposDeCuentas(int ano, int trimestre)
        { 
        }
    }
}
