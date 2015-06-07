﻿using System;
using System.Text;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.Generic;

using PagoElectronico.Models.DAO;

namespace PagoElectronico.Models.BO
{
    public partial class Cliente : IBO<Cliente>
    {

        public Cliente() { }

        public Cliente(DataRow dr)
        {
            initialize(dr);
        }

        public int? id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int? documento { get; set; }
        public int? tipo_documento { get; set; }
        public string dom_calle { get; set; }
        public int? dom_piso { get; set; }
        public char? dom_dpto { get; set; }
        public int? dom_pais { get; set; }
        public string mail { get; set; }
        public int? nacionalidad { get; set; }
        public int? cuenta { get; set; }
        public int? banco { get; set; }

        public Cliente initialize(DataRow _dr)
        {

            dr = _dr;
            DataColumnCollection dcc = dr.Table.Columns;

            if (dcc.Contains("id"))
                id = (dr["id"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["id"]);
            if (dcc.Contains("nombre"))
                nombre = (dr["nombre"] == DBNull.Value) ? null : dr["nombre"].ToString();
            if (dcc.Contains("apellido"))
                apellido = (dr["apellido"] == DBNull.Value) ? null : dr["apellido"].ToString();
            if (dcc.Contains("documento"))
                documento = (dr["documento"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["documento"]);
            if (dcc.Contains("tipo_documento"))
                tipo_documento = (dr["tipo_documento"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["tipo_documento"]);
            if (dcc.Contains("dom_calle"))
                dom_calle = (dr["dom_calle"] == DBNull.Value) ? null : dr["dom_calle"].ToString();
            if (dcc.Contains("dom_piso"))
                dom_piso = (dr["dom_piso"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["dom_piso"]);
            if (dcc.Contains("dom_calle"))
                dom_calle = (dr["dom_calle"] == DBNull.Value) ? null : dr["dom_calle"].ToString();
            if (dcc.Contains("dom_pais"))
                dom_pais = (dr["dom_pais"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["dom_pais"]);
            if (dcc.Contains("mail"))
                mail = (dr["mail"] == DBNull.Value) ? null : dr["mail"].ToString();
            if (dcc.Contains("nacionalidad"))
                nacionalidad = (dr["nacionalidad"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["nacionalidad"]);
            if (dcc.Contains("cuenta"))
                cuenta = (dr["cuenta"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["cuenta"]);
            if (dcc.Contains("banco"))
                banco = (dr["banco"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["banco"]);


            return this;
        }

        private DataRow dr;

        public Cliente setData(DataRow dr)
        {
            initialize(dr);

            return this;
        }

        public void setById(object _id)
        {
            initialize(new DAOCliente().retrieveBy_id(_id).dr);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Cliente aux = obj as Cliente;
            if ((object)aux == null)
                return false;

            return aux.id == id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}