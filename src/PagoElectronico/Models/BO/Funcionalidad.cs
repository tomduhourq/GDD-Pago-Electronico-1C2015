using System;
using System.Text;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.Generic;

using PagoElectronico.Models.DAO;

namespace PagoElectronico.Models.BO
{
    public partial class Funcionalidad : IBO<Funcionalidad>
    {

        public Funcionalidad() { }

        public Funcionalidad(DataRow dr)
        {
            initialize(dr);
        }

        public int? id { get; set; }
        public string nombre { get; set; }

        public Funcionalidad initialize(DataRow _dr)
        {

            dr = _dr;
            DataColumnCollection dcc = dr.Table.Columns;

            if (dcc.Contains("id"))
                id = (dr["id"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["id"]);
            if (dcc.Contains("nombre"))
                nombre = (dr["nombre"] == DBNull.Value) ? null : dr["nombre"].ToString();
            
            return this;
        }

        private DataRow dr;

        public Funcionalidad setData(DataRow dr)
        {
            initialize(dr);

            return this;
        }

        public void setById(object _id)
        {
            initialize(new DAOFuncionalidad().retrieveBy_id(_id).dr);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Funcionalidad aux = obj as Funcionalidad;
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