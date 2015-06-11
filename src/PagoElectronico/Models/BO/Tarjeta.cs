using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PagoElectronico.Models.BO
{
    public partial class Tarjeta: IBO<Tarjeta>
    {
        public Tarjeta() { }

        public Tarjeta(DataRow dr) { initialize(dr); }

        public int? id {get; set;}
        public long? numero {get; set; }
        public DateTime? fecha_emision {get; set; }
        public DateTime? fecha_vencimiento {get; set; }
        public int? cod_seguridad {get; set; }
        public int? emisor {get; set; }
        public int? cuenta {get; set; }
        public int? cod_banco {get; set;}

        private DataRow dr;

        public Tarjeta initialize(DataRow _dr)
        {
            dr = _dr;
            DataColumnCollection dcc = dr.Table.Columns;

            if (dcc.Contains("id"))
                id = (dr["id"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["id"]);
            if (dcc.Contains("numero"))
                numero = (dr["numero"] == DBNull.Value) ? null : (long?)Convert.ToInt64(dr["numero"]);
            if(dcc.Contains("fecha_emision"))
                fecha_emision = (dr["fecha_emision"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(dr["fecha_emision"]);
            if (dcc.Contains("fecha_vencimiento"))
                fecha_vencimiento = (dr["fecha_vencimiento"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(dr["fecha_vencimiento"]);
            if (dcc.Contains("cod_seguridad"))
                cod_seguridad = (dr["cod_seguridad"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["cod_seguridad"]);
            if (dcc.Contains("emisor"))
                emisor = (dr["emisor"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["emisor"]);
            if (dcc.Contains("cuenta"))
                cuenta = (dr["cuenta"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["cuenta"]);
            if (dcc.Contains("banco"))
               cod_banco = (dr["banco"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["banco"]);
            return this;
        }

        public Tarjeta setData(DataRow dr)
        {
            initialize(dr);

            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Tarjeta aux = obj as Tarjeta;
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
