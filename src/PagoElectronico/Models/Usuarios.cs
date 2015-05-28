using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;
using System.Data.SqlClient;
using PagoElectronico.Models.DataBase;

namespace PagoElectronico.Models
{
    class Usuarios
    {
        public static List<Rol> ObtenerRoles(Usuario user)
        {
            List<Rol> l = new List<Rol>();

            List<SqlParameter> lParameters = new List<SqlParameter>();
            lParameters.Add(new SqlParameter("@nombre", user.Name));
            SqlDataReader lector = DBAcess.GetDataReader("SELECT * from VIDA_ESTATICA.roles_usuario(@nombre)", "T", lParameters);

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    Rol unRol = new Rol();
                    unRol.nombre = (string)lector["nombre"];
                    unRol.id = (int)lector["rol"];
                    unRol.activo = true;
                    l.Add(unRol);
                }
            }
            return l;
        }
    }
}
