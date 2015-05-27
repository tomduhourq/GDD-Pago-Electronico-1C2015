using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PagoElectronico.Models;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO {
    public partial class DAOTipoCuenta: DAOBase<TipoCuenta> {
        public DAOTipoCuenta()
            : base("Tipo_Cuenta", "id") {
        }
        
        public TipoCuenta update(TipoCuenta _Tipo_Cuenta) {
			DB.ExecuteNonQuery("UPDATE"); //FIXIT
			return DB.ExecuteReaderSingle<TipoCuenta>("SELECT * FROM Tipo_Cuenta WHERE id = @1", _Tipo_Cuenta.id);
        }

        public TipoCuenta create(TipoCuenta _Tipo_Cuenta) {
            if (_Tipo_Cuenta.id == null || !_Tipo_Cuenta.id.HasValue) {
                int id = DB.ExecuteCastable<int>("INSERT INTO Tipo_Cuenta () values (); SELECT SCOPE_IDENTITY();"); //FIXIT
                return DB.ExecuteReaderSingle<TipoCuenta>("SELECT * FROM Tipo_Cuenta WHERE id = @1", id);
			} else
				return update(_Tipo_Cuenta);
        }
		
		public void delete(int Tipo_Cuenta_id) {
			DB.ExecuteNonQuery("DELETE FROM" + tabla + " WHERE tipo = @1", Tipo_Cuenta_id);
        }
		public TipoCuenta retrieveBy_id(object _value){
			
            return DB.ExecuteReaderSingle<TipoCuenta>("SELECT * FROM " + tabla + " WHERE tipo = @1", _value);
			
		}
    }
}
