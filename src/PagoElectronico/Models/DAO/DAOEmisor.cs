using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOEmisor : DAOBase<Emisor>
    {
        public DAOEmisor()
            : base("VIDA_ESTATICA.Emisor", "id")
        {
        }

        public Emisor retrieveBy_id(object _value)
        {
            return DB.ExecuteReaderSingle<Emisor>("SELECT * FROM " + tabla + " WHERE id = @1", _value);
        }
    }
}
