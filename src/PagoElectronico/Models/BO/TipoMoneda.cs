using System;
using System.Text;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.Generic;

using PagoElectronico.Models.DAO;

namespace PagoElectronico.Models.BO {
    public partial class TipoMoneda : IBO<TipoMoneda>{
		
        public TipoMoneda() {}

        public TipoMoneda(DataRow dr) {
            initialize(dr);
        }
		
        public int? id {get; set;}
        public string descripcion {get; set;}
		
        public TipoMoneda initialize(DataRow _dr) {
			
			dr = _dr;
            DataColumnCollection dcc = dr.Table.Columns;
	
            if (dcc.Contains("id"))
                id = (dr["id"] == DBNull.Value) ? null : (int?) Convert.ToInt32(dr["id"]);
            if (dcc.Contains("descripcion"))
                descripcion = (dr["descripcion"] == DBNull.Value) ? null : dr["descripcion"].ToString();
			
            return this;
        }
		
        private DataRow dr;
		
		public TipoMoneda setData(DataRow dr) {
            initialize(dr);
			
			return this;
        }
		
        public void setById(object _id) {
            initialize(new DAOMoneda().retrieveBy_id(_id).dr);
        }
		
		public override bool Equals(object obj) {
            if (obj == null)
                return false;

            TipoMoneda aux = obj as TipoMoneda;
            if ((object)aux == null)
                return false;

            return aux.id == id;
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
