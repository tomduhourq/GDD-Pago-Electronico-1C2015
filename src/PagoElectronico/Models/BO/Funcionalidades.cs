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
        public static List<Funcionalidad> ObtenerFuncionalidades(int id)
        {
            List<Funcionalidad> l = new List<Funcionalidad>();

            SqlDataReader lector = DBAcess.GetDataReader(
                "SELECT f.id, f.nombre from VIDA_ESTATICA.Funcionalidad f INNER JOIN VIDA_ESTATICA.Funcionalidad_Rol fr ON f.id = fr.funcionalidad AND fr.rol ="+id, "T", new List<SqlParameter>());
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    Funcionalidad f = new Funcionalidad();
                    f.id = (int)(decimal)lector["id"];
                    f.nombre = (string)lector["nombre"];
                    l.Add(f);
                }
            }
            return l;
        }


        public static List<Funcionalidad> ObtenerFuncionalidades() {
            List<Funcionalidad> l = new List<Funcionalidad>();

            SqlDataReader lector = DBAcess.GetDataReader("SELECT * from VIDA_ESTATICA.Funcionalidad", "T", new List<SqlParameter>());

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

        public static List<Funcionalidad> DependeDe(int id)
        {
            List<Funcionalidad> l = new List<Funcionalidad>();
            SqlDataReader lector = DBAcess.GetDataReader("SELECT * from VIDA_ESTATICA.Funcionalidad_Rol WHERE rol = "+id, "T", new List<SqlParameter>());

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    Funcionalidad f = new Funcionalidad();
                    l.Add(f);
                }
            }
            return l;
        }

        public static bool AgregarFuncionalidadEnRol(int idRol, Funcionalidad unaFunc)
        {
            List<SqlParameter> ListaParametros = new List<SqlParameter>();
            ListaParametros.Add(new SqlParameter("@idRol", idRol));
            ListaParametros.Add(new SqlParameter("@idFunc", (int)unaFunc.id));

            return DBAcess.WriteInBase("INSERT INTO VIDA_ESTATICA.Funcionalidad_Rol (rol, funcionalidad) VALUES (@idRol, @idFunc)", "T", ListaParametros);
        }
    }
}
