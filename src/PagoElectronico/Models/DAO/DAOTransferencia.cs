using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOTransferencia : DAOBase<Transferencia>
    {
        public DAOTransferencia(): base("VIDA_ESTATICA.Transferencia", "id") 
        {
        }

        public Transferencia retrieveBy_id(object _value)
        {
            return DB.ExecuteReaderSingle<Transferencia>("SELECT * FROM " + tabla + " WHERE id = @1", _value);
        }

        public List<Transferencia> ultimasTransferenciasDeCuenta(Cuenta c)
        {
            string query = String.Format("SELECT TOP 10 * FROM {0} WHERE cuenta_destino = {1} ORDER BY id DESC", tabla, c.id);
            return DB.ExecuteReader<Transferencia>(query);
        }
    }
}
