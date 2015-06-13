using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAODeposito : DAOBase<Deposito>
    {
        public DAODeposito()
            : base("VIDA_ESTATICA.Deposito", "id")
        {
        }

        private static string columns = "fecha,importe,tipo_moneda,tarjeta_id,cuenta_destino";

        public Deposito create(Deposito depo)
        {
            string values = CreateValues(depo);
            int id = DB.ExecuteCastable<int>("INSERT INTO "+ tabla +" ("+columns+") values ("+values+"); SELECT SCOPE_IDENTITY();"); //FIXIT
            return DB.ExecuteReaderSingle<Deposito>("SELECT * FROM "+ tabla +" WHERE id = @1", id);
        }

        private string CreateValues(Deposito depo)
        {
            string[] imp = depo.importe.ToString().Split(',');
            return depo.fecha.ToString("dd/MM/yyyy") + "," +
                   imp[0]+"."+imp[1] + "," +
                   depo.tipo_moneda.ToString() + "," +
                   depo.tarjeta_id.ToString() + "," +
                   depo.cuenta_destino.ToString();
        }
    }
}
