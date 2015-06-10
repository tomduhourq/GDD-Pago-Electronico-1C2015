using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PagoElectronico.Models;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOCuenta: DAOBase<Cuenta>
    {
        public DAOCuenta()
            : base("VIDA_ESTATICA.Cuenta", "id")
        {
        }

        public Cuenta update(Cuenta _Cuenta)
        {
            DB.ExecuteNonQuery("UPDATE"); //FIXIT
            return DB.ExecuteReaderSingle<Cuenta>("SELECT * FROM Cuenta WHERE id = @1", _Cuenta.id);
        }

        public Cuenta create(Cuenta _Cuenta)
        {
            if (_Cuenta.id == null || !_Cuenta.id.HasValue)
            {
                int id = DB.ExecuteCastable<int>("INSERT INTO Cuenta () values (); SELECT SCOPE_IDENTITY();"); //FIXIT
                return DB.ExecuteReaderSingle<Cuenta>("SELECT * FROM Cuenta WHERE id = @1", id);
            }
            else
                return update(_Cuenta);
        }

        public void delete(int Cuenta_id)
        {
            DB.ExecuteNonQuery("DELETE FROM" + tabla + " WHERE id = @1", Cuenta_id);
        }

        public Cuenta retrieveBy_id(object _value)
        {
            return DB.ExecuteReaderSingle<Cuenta>("SELECT * FROM " + tabla + " WHERE id = @1", _value);
        }

        public List<Cuenta> retrieveBy_Cliente(object cliID)
        {
            return DB.ExecuteReader<Cuenta>("SELECT * FROM " + tabla + " WHERE cod_cli = @1", cliID);
        }
    }
}