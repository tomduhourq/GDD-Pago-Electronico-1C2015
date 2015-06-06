using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PagoElectronico.Models;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;

namespace PagoElectronico.Models.DAO {
    public partial class DAORol: DAOBase<Rol> {
        public DAORol()
            : base("VIDA_ESTATICA.Rol", "id") {
        }
        
        public Rol update(Rol _Rol) {
			DB.ExecuteNonQuery("UPDATE"); //FIXIT
			return DB.ExecuteReaderSingle<Rol>("SELECT * FROM Rol WHERE id = @1", _Rol.id);
        }

        public Rol create(Rol _Rol) {
            if (_Rol.id == null || !_Rol.id.HasValue) {
                int id = DB.ExecuteCastable<int>("INSERT INTO Rol () values (); SELECT SCOPE_IDENTITY();"); //FIXIT
                return DB.ExecuteReaderSingle<Rol>("SELECT * FROM Rol WHERE id = @1", id);
			} else
				return update(_Rol);
        }
		
		public void delete(int Rol_id) {
			DB.ExecuteNonQuery("DELETE FROM" + tabla + " WHERE id = @1", Rol_id);
        }

		public Rol retrieveBy_id(object _value){
            return DB.ExecuteReaderSingle<Rol>("SELECT * FROM " + tabla + " WHERE id = @1", _value);
		}
    }
}
