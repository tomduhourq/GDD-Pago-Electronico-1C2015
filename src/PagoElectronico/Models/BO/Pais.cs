using System;
using System.Text;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.Generic;

using PagoElectronico.Models.DAO;

namespace PagoElectronico.Models.BO {
    public partial class Pais : IBO<Pais>{
		
        public Pais() {}
        
        public Pais(DataRow dr) {
            initialize(dr);
        }
		
        public int? id {get; set;}
        public string descripcion {get; set;}
		
        public Pais initialize(DataRow _dr) {
			
			dr = _dr;
            DataColumnCollection dcc = dr.Table.Columns;
	
            if (dcc.Contains("id"))
                id = (dr["id"] == DBNull.Value) ? null : (int?) Convert.ToInt32(dr["id"]);
            if (dcc.Contains("descripcion"))
                descripcion = (dr["descripcion"] == DBNull.Value) ? null : dr["descripcion"].ToString();
			
            return this;
        }
		
        private DataRow dr;
		
		public Pais setData(DataRow dr) {
            initialize(dr);
			
			return this;
        }
		
        public void setById(object _id) {
            initialize(new DAOPais().retrieveBy_id(_id).dr);
        }
		
		public override bool Equals(object obj) {
            if (obj == null)
                return false;

            Pais aux = obj as Pais;
            if ((object)aux == null)
                return false;

            return aux.id == id;
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.descripcion.Trim();
        }
    }
}
