using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PagoElectronico.Models;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO {
    public partial class DAOPais: DAOBase<Pais> {
        public DAOPais()
            : base("Pais", "id") {
        }
        
        public Pais update(Pais _Pais) {
			DB.ExecuteNonQuery("UPDATE"); //FIXIT
			return DB.ExecuteReaderSingle<Pais>("SELECT * FROM Pais WHERE id = @1", _Pais.id);
        }

        public Pais create(Pais _Pais) {
            if (_Pais.id == null || !_Pais.id.HasValue) {
                int id = DB.ExecuteCastable<int>("INSERT INTO Pais () values (); SELECT SCOPE_IDENTITY();"); //FIXIT
                return DB.ExecuteReaderSingle<Pais>("SELECT * FROM Pais WHERE id = @1", id);
			} else
				return update(_Pais);
        }
		
		public void delete(int Pais_id) {
			DB.ExecuteNonQuery("DELETE FROM" + tabla + " WHERE id = @1", Pais_id);
        }

		public Pais retrieveBy_id(object _value){
            return DB.ExecuteReaderSingle<Pais>("SELECT * FROM " + tabla + " WHERE id = @1", _value);
		}
    }
}
