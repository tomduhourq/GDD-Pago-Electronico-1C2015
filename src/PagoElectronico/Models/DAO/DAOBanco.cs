using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PagoElectronico.Models;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOBanco : DAOBase<Banco>
    {
        public DAOBanco()
            : base("Banco", "cod")
        {
        }

        public Banco update(Banco _Banco)
        {
            DB.ExecuteNonQuery("UPDATE"); //FIXIT
            return DB.ExecuteReaderSingle<Banco>("SELECT * FROM Banco WHERE cod = @1", _Banco.cod);
        }

        public Banco create(Banco _Banco)
        {
            if (_Banco.cod == null || !_Banco.cod.HasValue)
            {
                int cod = DB.ExecuteCastable<int>("INSERT INTO Banco () values (); SELECT SCOPE_IDENTITY();"); //FIXIT
                return DB.ExecuteReaderSingle<Banco>("SELECT * FROM Banco WHERE cod = @1", cod);
            }
            else
                return update(_Banco);
        }

        public void delete(int Banco_cod)
        {
            DB.ExecuteNonQuery("DELETE FROM" + tabla + " WHERE cod = @1", Banco_cod);
        }

        public Banco retrieveBy_id(object _value)
        {
            return DB.ExecuteReaderSingle<Banco>("SELECT * FROM " + tabla + " WHERE cod = @1", _value);
        }
    }
}
