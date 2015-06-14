using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PagoElectronico.Models;
using PagoElectronico.Models.BO;
using PagoElectronico.Models.Utils;
using System.Data.SqlClient;
using PagoElectronico.Models.DataBase;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOCliente: DAOBase<Cliente>
    {
        public DAOCliente()
            : base("VIDA_ESTATICA.Cliente", "id")
        {
        }

        public bool create(Cliente _Cliente)
        {
            try
            {
                List<SqlParameter> ListaParametros = new List<SqlParameter>();
                ListaParametros.Add(new SqlParameter("@nombreCliente", _Cliente.nombre));
                ListaParametros.Add(new SqlParameter("@apellidoCliente", _Cliente.apellido));
                ListaParametros.Add(new SqlParameter("@documentoCliente", _Cliente.documento));
                ListaParametros.Add(new SqlParameter("@domCalleCliente", _Cliente.dom_calle));
                ListaParametros.Add(new SqlParameter("@domDptoCliente", _Cliente.dom_dpto));
                ListaParametros.Add(new SqlParameter("@domNroCliente", _Cliente.dom_nro));
                ListaParametros.Add(new SqlParameter("@domPisoCliente", _Cliente.dom_piso));
                ListaParametros.Add(new SqlParameter("@fecNacCliente", _Cliente.fecha_nac));
                ListaParametros.Add(new SqlParameter("@mailCliente", _Cliente.mail));
                ListaParametros.Add(new SqlParameter("@nacionalidadCliente", _Cliente.nacionalidad));
                ListaParametros.Add(new SqlParameter("@tipoDocCliente", _Cliente.tipo_documento));
                ListaParametros.Add(new SqlParameter("@usuarioCliente", _Cliente.usuario));
                SqlParameter paramRet = new SqlParameter("@ret", System.Data.SqlDbType.Decimal);
                paramRet.Direction = System.Data.ParameterDirection.Output;
                ListaParametros.Add(paramRet);

                // insert cliente
                int ret = (int)DBAcess.ExecStoredProcedure("VIDA_ESTATICA.agregarCliente", ListaParametros);

                return true;
            }
            catch { return false; } 

        }

        public bool update(Cliente cliente)
        {
            try
            {
                List<SqlParameter> lp = new List<SqlParameter>();
                lp.Add(new SqlParameter("@cliente", (int)cliente.id));
                bool del = DBAcess.WriteInBase("DELETE FROM VIDA_ESTATICA.Cliente WHERE id =@cliente", "T", lp);
                
                List<SqlParameter> ListaParametros = new List<SqlParameter>();
                ListaParametros.Add(new SqlParameter("@id", (int)cliente.id));
                ListaParametros.Add(new SqlParameter("@nombre", cliente.nombre));
                return DBAcess.WriteInBase("update VIDA_ESTATICA.Cliente set nombre =@nombre where id=@id", "T", ListaParametros);
            }
            catch { return false; }
        }

        public void delete(int Cliente_id)
        {
            DB.ExecuteNonQuery("DELETE FROM" + tabla + " WHERE id = @1", Cliente_id);
        }

        public Cliente retrieveBy_id(object _value)
        {
            return DB.ExecuteReaderSingle<Cliente>("SELECT * FROM " + tabla + " WHERE id = @1", _value);
        }

        public List<Cliente> retrieveByInfo(string id, string numeroDoc, string nombre, string apellido)
        {

            if (String.IsNullOrEmpty(id) && String.IsNullOrEmpty(nombre) &&
                String.IsNullOrEmpty(numeroDoc) && String.IsNullOrEmpty(apellido))
            {
                return retrieveBase();
            }

            string baseQuery = String.Format("SELECT * FROM {0} WHERE", tabla);

            if (!String.IsNullOrEmpty(id))
            {
                baseQuery += String.Format(" id = {0} AND", id);
            }
            if (!String.IsNullOrEmpty(numeroDoc))
            {
                baseQuery += String.Format(" documento = {0} AND", numeroDoc);
            }
            if (!String.IsNullOrEmpty(nombre))
            {
                baseQuery += String.Format(" nombre LIKE '%{0}%' AND", nombre);
            }
            if (!String.IsNullOrEmpty(apellido))
            {
                baseQuery += String.Format(" apellido LIKE '%{0}%' AND", apellido);
            }
            
            baseQuery = baseQuery.Substring(0, baseQuery.Length - 3);
            
            return DB.ExecuteReader<Cliente>(baseQuery);
        }


        public Cliente retrieveBy_user(string userId)
        {
            return DB.ExecuteReaderSingle<Cliente>("SELECT * FROM " + tabla + " WHERE usuario = @1", userId);
        }
    }
}