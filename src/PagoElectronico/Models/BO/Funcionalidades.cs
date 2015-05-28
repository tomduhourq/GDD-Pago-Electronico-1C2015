using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using PagoElectronico.Models.DataBase;

namespace PagoElectronico.Models.BO
{
    class Funcionalidades
    {
        public static List<Funcionalidad> ObtenerFuncionalidades() {
            List<Funcionalidad> l = new List<Funcionalidad>();

            SqlDataReader lector = DBAcess.GetDataReader("SELECT * from VIDA_ESTATICA.Rol", "T", new List<SqlParameter>());

            if (lector.HasRows)
            {
                while (lector.Read()) {
                    Funcionalidad f = new Funcionalidad();
                    f.id = (int)(decimal)lector["id"];
                    f.nombre = (string)lector["nombre"];
                    l.Add(f);
                }
            }
            return l;
        }
    }
}
