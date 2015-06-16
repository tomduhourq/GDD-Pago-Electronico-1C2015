using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAORetiro : DAOBase<Retiro>
    {
          public DAORetiro()
            : base("VIDA_ESTATICA.Retiro", "id") {
        }


          private static string columns = "fecha,importe,cuenta_destino,moneda";

          internal Retiro create(Retiro retiro)
          {
              string values = CreateValues(retiro);
              int id = DB.ExecuteCastable<int>("INSERT INTO " + tabla + " (" + columns + ") values (" + values + "); SELECT SCOPE_IDENTITY();"); //FIXIT
              return DB.ExecuteReaderSingle<Retiro>("SELECT * FROM " + tabla + " WHERE id = @1", id);
          }

        public string CreateValues(Retiro retiro) {
            // Adapt to sql server
             string[] imp = retiro.importe.ToString().Split(',');
             string sqlDouble = "";
             if(imp.Length == 2) sqlDouble = imp[0] + "." + imp[1];
             else sqlDouble = imp[0];
             return 
                    retiro.fecha.ToString("dd/MM/yyyy") + "," +
                    sqlDouble + "," +
                    retiro.cuenta_destino.ToString() + "," +
                    retiro.moneda.ToString();
        }
    }
}
