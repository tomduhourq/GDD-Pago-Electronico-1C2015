﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Models.DataBase;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Facturacion
{
    public partial class FormFacturacion : Form
    {

         private decimal idItem;
        public DataTable dt;
        public DataTable dtDatos;
        private string usuario;
        private string salida;
        private decimal id_item_factura;
        private decimal num_cuenta;

        private void FormFacturacion_Load(object sender, EventArgs e)
        {

        }

        public FormFacturacion(decimal id_item,string user)
        {
            InitializeComponent();
            usuario = user;
            idItem = id_item;

            dgvFactura.AllowUserToAddRows = false;
            dgvFactura.AllowUserToDeleteRows = false;
            

            //CARGO EL DATAGRIDVIEW CON LOS DATOS A FACTURAR
            SqlConnection con = DBAcess.GetConnection();
            if (getRolUser() == "Administrador")
            {
                if (id_item == 0)
                {
                    string query = "SELECT i.id_item_factura, i.num_cuenta, i.monto, it.descripcion, i.fecha FROM VIDA_ESTATICA.Item_Factura i"
                                + " JOIN VIDA_ESTATICA.ITEMS it ON it.id_item = i.id_item"
                                + " WHERE i.id_factura is NULL AND i.facturado = 0  AND fecha IS NOT NULL ORDER BY i.num_cuenta";
                    dtDatos = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dtDatos);
                    dt = dtDatos;
                }
                else
                {

                    string query = "SELECT i.id_item_factura, i.num_cuenta, i.monto, it.descripcion, i.fecha FROM VIDA_ESTATICA.Item_Factura i"
                                + " JOIN VIDA_ESTATICA.Items it ON it.id_item = i.id_item"
                                + " WHERE i.id_item = " + id_item + " AND  i.id_factura is NULL AND i.facturado = 0  AND fecha IS NOT NULL ORDER BY i.num_cuenta";

                    dtDatos = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dtDatos);
                    dt = dtDatos;
                }
            }
            else
            {
                if (id_item == 0)
                {
                    string query = "SELECT i.id_item_factura, i.num_cuenta, i.monto, it.descripcion, i.fecha FROM VIDA_ESTATICA.Item_Factura i"
                                + " JOIN VIDA_ESTATICA.Items it ON it.id_item = i.id_item"
                                + " JOIN VIDA_ESTATICA.Cuenta c ON c.num_cuenta = i.num_cuenta "
                                + " JOIN VIDA_ESTATICA.Cliente cl ON cl.id_cliente = c.id_cliente"
                                + " WHERE cl.username = '" + usuario + "' AND  i.id_factura is NULL AND i.facturado = 0  AND fecha IS NOT NULL ORDER BY i.num_cuenta";
                    dtDatos = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dtDatos);
                    dt = dtDatos;
                }
                else
                {

                    string query = "SELECT i.id_item_factura, i.num_cuenta, i.monto, it.descripcion, i.fecha FROM VIDA_ESTATICA.Item_Factura i"
                                + " JOIN VIDA_ESTATICA.Items it ON it.id_item = i.id_item"
                                +" JOIN VIDA_ESTATICA.Cuenta c ON c.num_cuenta = i.num_cuenta "
                                +" JOIN VIDA_ESTATICA.Cliente cl ON cl.id_cliente = c.id_cliente"
                                + " WHERE i.id_item = " + id_item + " AND cl.username = '"+usuario+"' AND  i.id_factura is NULL AND i.facturado = 0  AND fecha IS NOT NULL ORDER BY i.num_cuenta";

                    dtDatos = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dtDatos);
                    dt = dtDatos;
                }
                
             }
                dgvFactura.DataSource = dtDatos;
                con.Close();

                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                dgvFactura.Columns.Add(chk);
                chk.HeaderText = "Facturar";
                chk.Name = "chk";
            
        }

        private int getIdCliente()
        {
            SqlConnection con = DBAcess.GetConnection();
            if (getRolUser() == "Administrador")
            {
                string query = "SELECT id_cliente FROM VIDA_ESTATICA.Cuenta WHERE num_cuenta = '" + num_cuenta + "'";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader lector = command.ExecuteReader();
                lector.Read();
                int id_cliente = lector.GetInt32(0);
                con.Close();
                return id_cliente;

            }
            else
            {
                string query = "SELECT id_cliente FROM VIDA_ESTATICA.Cliente WHERE usuario = '" + usuario + "'";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader lector = command.ExecuteReader();
                lector.Read();
                int id_cliente = lector.GetInt32(0);
                con.Close();
                return id_cliente;

            }

        }

        private string getRolUser()
        {
            SqlConnection con = DBAcess.GetConnection();
            //OBTENGO ID DE CLIENTE
             
            string query = "SELECT R.nombre FROM VIDA_ESTATICA.Rol_Usuario U JOIN VIDA_ESTATICA.Rol R ON R.id_rol=U.rol WHERE U.usuario = '" + usuario + "'";
            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader lector = command.ExecuteReader();
            lector.Read();
            string rol = lector.GetString(0);
            con.Close();
            return rol;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {

        }
        private string facturar(decimal id_item)
        {
            
            SqlConnection con = DBAcess.GetConnection();
            string salida;

            try
            {
                string query = "VIDA_ESTATICA.PRC_facturar_item_factura";
                SqlCommand command = new SqlCommand(query, con);
                command.CommandType = CommandType.StoredProcedure;
                id_item_factura = id_item;
                command.Parameters.Add(new SqlParameter("@id_item_factura",id_item_factura));
                decimal id_factura = getIdFactura();
                command.Parameters.Add(new SqlParameter("@id_factura", id_factura));
                command.ExecuteNonQuery();
                
                salida = "Se facturo correctamente";
                
            }
            catch (Exception ex)
            {
                salida = "No se pudo facturar" + ex.ToString();
            }
            con.Close();
            return salida;
        }
        private decimal getIdFactura()
        {
            SqlConnection con = DBAcess.GetConnection();
            string query = "VIDA_ESTATICA.PRC_obtener_factura";
            SqlCommand command = new SqlCommand(query, con);
            command.CommandType = CommandType.StoredProcedure;
            DateTime fechaConfiguracion = Utils.fechaSistema;
            command.Parameters.Add(new SqlParameter("@fecha", fechaConfiguracion));
            command.Parameters.Add(new SqlParameter("@id_cliente", getIdCliente()));
            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@id_factura";
            outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
            outPutParameter.Direction = System.Data.ParameterDirection.Output;
            command.Parameters.Add(outPutParameter);

            command.ExecuteNonQuery();

            return Convert.ToDecimal(outPutParameter.Value); 
        }

        private void dgvFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFacturar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
