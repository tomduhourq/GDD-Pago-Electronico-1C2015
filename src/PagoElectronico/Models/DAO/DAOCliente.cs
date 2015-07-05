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
                ListaParametros.Add(new SqlParameter("@nombre", _Cliente.nombre));
                ListaParametros.Add(new SqlParameter("@apellido", _Cliente.apellido));
                ListaParametros.Add(new SqlParameter("@documento", _Cliente.documento));
                ListaParametros.Add(new SqlParameter("@dom_calle", _Cliente.dom_calle));
                ListaParametros.Add(new SqlParameter("@dom_nro", _Cliente.dom_nro));
                ListaParametros.Add(new SqlParameter("@dom_piso", _Cliente.dom_piso));
                ListaParametros.Add(new SqlParameter("@dom_dpto", _Cliente.dom_dpto[0]));
                ListaParametros.Add(new SqlParameter("@fecha_nac", _Cliente.fecha_nac));
                ListaParametros.Add(new SqlParameter("@mail", _Cliente.mail));
                ListaParametros.Add(new SqlParameter("@nacionalidad", _Cliente.nacionalidad));
                ListaParametros.Add(new SqlParameter("@tipo_documento", _Cliente.tipo_documento));
                if (_Cliente.usuario != null)
                {
                    ListaParametros.Add(new SqlParameter("@usuario", _Cliente.usuario));
                }
                else
                {
                    ListaParametros.Add(new SqlParameter("@usuario", ""));
                }

                return DBAcess.WriteInBase("INSERT INTO VIDA_ESTATICA.Cliente VALUES (@nombre,@apellido,@documento," +
                    "@dom_calle,@dom_nro,@dom_piso,@dom_dpto,@fecha_nac," +
                    "@mail,@nacionalidad,@tipo_documento,@usuario,1)", "T", ListaParametros);
            }
            catch { return false; }

        }

        public bool update(Cliente cliente)
        {
            try
            {                
                List<SqlParameter> ListaParametros = new List<SqlParameter>();
                ListaParametros.Add(new SqlParameter("@id", (int)cliente.id));
                ListaParametros.Add(new SqlParameter("@nombre", cliente.nombre));
                ListaParametros.Add(new SqlParameter("@apellido", cliente.apellido));                
                ListaParametros.Add(new SqlParameter("@documento", cliente.documento));
                ListaParametros.Add(new SqlParameter("@dom_calle", cliente.dom_calle));                
                ListaParametros.Add(new SqlParameter("@dom_nro", cliente.dom_nro));
                ListaParametros.Add(new SqlParameter("@dom_piso", cliente.dom_piso));
                ListaParametros.Add(new SqlParameter("@dom_dpto", cliente.dom_dpto));
                ListaParametros.Add(new SqlParameter("@fecha_nac", cliente.fecha_nac));
                ListaParametros.Add(new SqlParameter("@mail", cliente.mail));
                ListaParametros.Add(new SqlParameter("@nacionalidad", cliente.nacionalidad));
                ListaParametros.Add(new SqlParameter("@tipo_documento", cliente.tipo_documento));
              
                
                return DBAcess.WriteInBase("update VIDA_ESTATICA.Cliente set nombre =@nombre, apellido=@apellido, documento=@documento," +
                    "dom_calle=@dom_calle,dom_nro=@dom_nro,dom_piso=@dom_piso,dom_dpto=@dom_dpto,fecha_nac=@fecha_nac," +
                    "mail=@mail, nacionalidad=@nacionalidad where id=@id", "T", ListaParametros);
            }
            catch { return false; }
        }

        public void delete(int Cliente_id)
        {
            string update = String.Format("UPDATE " + tabla + " SET activo = 0 WHERE id = {0}", Cliente_id);
            DB.ExecuteNonQuery(update);
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
                baseQuery += String.Format(" nombre LIKE '{0}%' AND", nombre);
            }
            if (!String.IsNullOrEmpty(apellido))
            {
                baseQuery += String.Format(" apellido LIKE '{0}%'", apellido);
            }

            baseQuery = baseQuery.Substring(0, baseQuery.Length - 3);
            
            return DB.ExecuteReader<Cliente>(baseQuery);
        }


        public Cliente retrieveBy_user(string userId)
        {
            return DB.ExecuteReaderSingle<Cliente>("SELECT * FROM " + tabla + " WHERE usuario = @1", userId);
        }

        public List<Cliente> search(string first_name, string last_name, string identification_type, string identification_number, string email) { 
            List<Cliente> lc = new List<Cliente>();

            if (String.IsNullOrEmpty(first_name) && String.IsNullOrEmpty(last_name) && String.IsNullOrEmpty(identification_number)
                && String.IsNullOrEmpty(identification_number) && String.IsNullOrEmpty(email)) {
                    String query = String.Format("SELECT * FROM VIDA_ESTATICA.CLiente");
                    return DB.ExecuteReader<Cliente>(query);
            }


            String base_query = String.Format("SELECT * FROM VIDA_ESTATICA.Cliente WHERE");
            if (!String.IsNullOrEmpty(first_name))
            {
                base_query += String.Format(" nombre like '{0}%' AND", first_name);
            }
            if (!String.IsNullOrEmpty(last_name))
            {
                base_query += String.Format(" apellido like '{0}%' AND", last_name);
            }
            if (!String.IsNullOrEmpty(identification_type))
            {
                base_query += String.Format(" tipo_documento = ( select top 1 id from VIDA_ESTATICA.Tipo_Documento where descripcion = '{0}') AND", identification_type);
            }
            if (!String.IsNullOrEmpty(identification_number))
            {
                base_query += String.Format(" documento = '{0}' AND", identification_number);
            }
            if (!String.IsNullOrEmpty(email))
            {
                base_query += String.Format(" mail LIKE '{0}%' AND", email);
            }

            base_query = base_query.Substring(0, base_query.Length - 3);

            return DB.ExecuteReader<Cliente>(base_query);
        }

        public List<Cliente> retrieveAll()
        {
            List<Cliente> l = new List<Cliente>();
            SqlDataReader lector = DBAcess.GetDataReader("SELECT * from VIDA_ESTATICA.Cliente", "T", new List<SqlParameter>());

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    Cliente unCliente = new Cliente();
                    unCliente.nombre = (string)lector["nombre"];
                    unCliente.id = (int)(decimal)lector["id"];
                    unCliente.apellido = (string)lector["apellido"];
                    unCliente.documento = (int)(decimal)lector["documento"];
                    unCliente.dom_calle = (string)lector["dom_calle"];
                    unCliente.dom_dpto = (string)lector["dom_dpto"];
                    unCliente.dom_nro = (int)(decimal)lector["dom_nro"];
                    unCliente.dom_piso = (int)(decimal)lector["dom_piso"];
                    unCliente.mail = (string)lector["mail"];
                    unCliente.nacionalidad = (int)(decimal)lector["nacionalidad"];
                    unCliente.fecha_nac = (DateTime?)lector["fecha_nac"];
                    unCliente.activo = (bool?)lector["activo"];
                    try
                    {
                        unCliente.usuario = (string)lector["usuario"];
                    }
                    catch
                    {
                        unCliente.usuario = "";
                    }
                    unCliente.tipo_documento = (int)(decimal)lector["tipo_documento"];
                    l.Add(unCliente);
                }
            }
            return l;
        }
    }
}