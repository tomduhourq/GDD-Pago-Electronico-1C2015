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

namespace PagoElectronico.Retiros
{
    public partial class IngresarDNI : Form
    {
        private Cliente cliente {get; set;}
        private Cheque cheque { get; set; }
        private DAOCheque daoCheque = new DAOCheque();

        public IngresarDNI(Cliente cli, Cheque cheq)
        {
            cliente = cli;
            cheque = cheq;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (!Utils.isNumeric(keypress))
            {
                MessageBox.Show(" Solo puede ingresar un número o ,!");
                textBox1.Text = "";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt64(textBox1.Text) == cliente.documento){
                Cheque inserted = daoCheque.create(cheque);
                if(inserted.id_egreso == cheque.id_egreso)
                    MessageBox.Show("Su cheque tiene número de egreso: " + cheque.id_egreso);
            }
            else {
                MessageBox.Show("El DNI no coincide con el del cliente logueado. Esta ventana se cerrará.");
                this.Close();
            }
        }
    }
}
