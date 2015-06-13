using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PagoElectronico.Models.BO
{
    public partial class Deposito: IBO <Deposito>
    {
        public long id {get; set;}
        public DateTime fecha {get; set;}
        public double? importe {get; set;}
        public int? tipo_moneda {get; set;}
        public long? tarjeta_id {get; set;}
        public long? cuenta_destino {get; set;}

        private DataRow dr;

           public Deposito(DataRow dr)
        {
            initialize(dr);
        }

        public Deposito() { }

        public Deposito setData(DataRow dr)
        {
            initialize(dr);

            return this;
        }

        public Deposito initialize(DataRow _dr)
        {
            dr = _dr;
            DataColumnCollection dcc = dr.Table.Columns;
            if (dcc.Contains("id"))
                id = Convert.ToInt64(dr["id"]);
            if (dcc.Contains("fecha"))
                fecha = Convert.ToDateTime(dr["fecha"]);
            if (dcc.Contains("importe"))
                importe = (dr["importe"] == DBNull.Value) ? null : (double?)Convert.ToDouble(dr["importe"]);
            if(dcc.Contains("tipo_moneda"))
                tipo_moneda = (dr["tipo_moneda"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["tipo_moneda"]);
            if (dcc.Contains("tarjeta_id"))
                tarjeta_id = (dr["tarjeta_id"] == DBNull.Value) ? null : (long?)Convert.ToInt64(dr["tarjeta_id"]);
            if (dcc.Contains("cuenta_destino"))
                cuenta_destino = (dr["cuenta_destino"] == DBNull.Value) ? null : (long?)Convert.ToInt64(dr["cuenta_destino"]);
            return this;
        }
    }
}
