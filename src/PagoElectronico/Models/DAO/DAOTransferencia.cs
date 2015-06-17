﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOTransferencia : DAOBase<Transferencia>
    {
         public DAOTransferencia()
            : base("VIDA_ESTATICA.Transferencia", "id") {
        }

         private static string columns = "fecha,importe,costo,cuenta_origen,cuenta_destino,tipo_moneda";

         internal Transferencia create(Transferencia transferencia)
         {
             string values = CreateValues(transferencia);
             int id = DB.ExecuteCastable<int>("INSERT INTO " + tabla + " (" + columns + ") values (" + values + "); SELECT SCOPE_IDENTITY();"); //FIXIT
             return DB.ExecuteReaderSingle<Transferencia>("SELECT * FROM " + tabla + " WHERE id = @1", id);
         }

         private string CreateValues(Transferencia tran)
         {
             string[] imp = tran.importe.ToString().Split(',');
             string sqlDouble = "";
             if (imp.Length == 2) sqlDouble = imp[0] + "." + imp[1];
             else sqlDouble = imp[0];
             return tran.fecha.ToString("dd/MM/yyyy") + "," +
                    sqlDouble + "," +
                    tran.costo.ToString() + "," +
                    tran.cuenta_origen.ToString() + "," +
                    tran.cuenta_destino.ToString() + "," +
                    tran.tipo_moneda.ToString();
         }
    }
}
