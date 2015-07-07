using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;
using System.Data.SqlClient;
using PagoElectronico.Models.DataBase;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOTarjeta : DAOBase<Tarjeta>
    {
        public DAOTarjeta()
            : base("VIDA_ESTATICA.Tarjeta", "id") { }

        public List<Tarjeta> retrieveByClientId(object val) {
            return DB.ExecuteReader<Tarjeta>("SELECT * FROM "+ tabla +" WHERE cod_cli = @1", val);
        }
        
        public void delete(long Tarjeta_num)
        {
            DB.ExecuteNonQuery("DELETE FROM " + tabla + " WHERE numero = @1", Tarjeta_num);
        }

        public bool update(Tarjeta tarjeta)
        {
            try
            {
                List<SqlParameter> ListaParametros = new List<SqlParameter>();
                ListaParametros.Add(new SqlParameter("@numero", (long)tarjeta.numero));
                ListaParametros.Add(new SqlParameter("@fecha_emision", tarjeta.fecha_emision));
                ListaParametros.Add(new SqlParameter("@fecha_vencimiento", tarjeta.fecha_vencimiento));
                ListaParametros.Add(new SqlParameter("@cod_seguridad", tarjeta.cod_seguridad));


                return DBAcess.WriteInBase("update VIDA_ESTATICA.Tarjeta set fecha_emision=@fecha_emision, fecha_vencimiento=@fecha_vencimiento," +
                    "cod_seguridad=@cod_seguridad WHERE numero=@numero", "T", ListaParametros);
            }
            catch { return false; }
        }
    }
}
