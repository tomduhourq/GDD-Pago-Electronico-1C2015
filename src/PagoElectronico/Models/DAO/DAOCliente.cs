using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PagoElectronico.Models;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOCliente: DAOBase<Cliente>
    {
        public DAOCliente()
            : base("VIDA_ESTATICA.Cliente", "id")
        {
        }

        public Cliente update(Cliente _Cliente)
        {
            DB.ExecuteNonQuery("UPDATE"); //FIXIT
            return DB.ExecuteReaderSingle<Cliente>("SELECT * FROM Cliente WHERE id = @1", _Cliente.id);
        }

        public Cliente create(Cliente _Cliente)
        {
            if (_Cliente.id == null || !_Cliente.id.HasValue)
            {
                int id = DB.ExecuteCastable<int>("INSERT INTO Cliente () values (); SELECT SCOPE_IDENTITY();"); //FIXIT
                return DB.ExecuteReaderSingle<Cliente>("SELECT * FROM Cliente WHERE id = @1", id);
            }
            else
                return update(_Cliente);
        }

        public void delete(int Cliente_id)
        {
            DB.ExecuteNonQuery("DELETE FROM" + tabla + " WHERE id = @1", Cliente_id);
        }

        public Cliente retrieveBy_id(object _value)
        {
            return DB.ExecuteReaderSingle<Cliente>("SELECT * FROM " + tabla + " WHERE id = @1", _value);
        }
    }
}