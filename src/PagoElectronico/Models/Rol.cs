using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using PagoElectronico.Models.DB;

namespace PagoElectronico.Models
{
    public class Rol
    {
        public string Name { get; set; }
        public Funcionalidad Funcionalidad { get; set; }
        public bool Activo { get; set; }

        public Rol() { }

        public Rol(string rolName)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@rolName", rolName));

            SqlDataReader lector = DBAcess.GetDataReader("SELECT * FROM VIDA_ESTATICA.Rol WHERE name=@rolName", "T", paramList);
            if (lector.HasRows)
            {
                lector.Read();
                Name = rolName;
                Funcionalidad = (Funcionalidad)lector["funcionalidad"]; //TODO: 
                Activo = (bool)lector["activo"];
            }
        }

    }
}