using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOTarjeta : DAOBase<Tarjeta>
    {
        public DAOTarjeta()
            : base("VIDA_ESTATICA.Tarjeta", "id") { }
        public List<Tarjeta> retrieveByClientId(object val) {
            return DB.ExecuteReader<Tarjeta>("SELECT * FROM "+ tabla +" WHERE id = @1", val);
        }
    }
}
