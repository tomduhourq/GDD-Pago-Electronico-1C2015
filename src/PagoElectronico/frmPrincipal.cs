using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Models;

namespace PagoElectronico
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal(Usuario invoker)
        {
            InitializeComponent();
            user = invoker;
        }

        public Usuario user { get; set; }
        // TODO: private Rol rol { get; set; }
        // private List<Rol> lstRoles = new List<Rol>();

        // Acá cada uno pone los botones para cada rol, y se deriva a cada una
    }
}
