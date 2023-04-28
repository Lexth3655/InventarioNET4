using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Conexion;


namespace Modelo
{
    public class LoginUsuario
    {

        public static int Loguear(string usuario, string clave)
        {
            int idUsuario = 0;
            using (SqlConnection cn = new SqlConnection(DALConexion.sqlCn))
            {
                try
                {
                    //mandar a llamar al procedimiento almacenado 
                    SqlCommand cmd = new SqlCommand("usp_loginUsuario", cn);
                    cmd.Parameters.AddWithValue("Usuario", usuario);
                    cmd.Parameters.AddWithValue("clave", clave);
                    cmd.Parameters.AddWithValue("idUser", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    cmd.ExecuteNonQuery();

                    idUsuario = Convert.ToInt32(cmd.Parameters["IdUser"].Value);
                }
                catch (Exception e)
                {
                    idUsuario = 0;

                    throw;
                }

            }
            return idUsuario;
        }

        //Metodo para obtener los permiso
        public static List<Menu> ObtenerPermiso(int P_idUsuario)
        {

            List<Menu> listPermiso = new List<Menu>();//Lista de Permiso de laa DB
            using (SqlConnection cn = new SqlConnection(DALConexion.sqlCn))
            {
                try
                {
                    //mandar a llamar al procedimiento almacenado 
                    SqlCommand cmd = new SqlCommand("usp_ObtenerPermisos", cn);
                    cmd.Parameters.AddWithValue("idUser", P_idUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    XmlReader leerXML = cmd.ExecuteXmlReader();//Lee el xml generado por la Dbaa

                    while (leerXML.Read())
                    {

                        XDocument doc = XDocument.Load(leerXML);

                        if (doc.Element("Permisos") != null)
                        {
                            listPermiso = doc.Element("Permisos").Element("DetalleMenu") == null ? new List<Menu>() :

                                (from menu in doc.Element("Permisos").Element("DetalleMenu").Elements("Menu")
                                 select new Menu()
                                 {
                                     nombre = menu.Element("descripcionMenu").Value,
                                     icono = menu.Element("icono").Value,
                                     listaSubMenu = menu.Element("DetalleSubMenu") == null ? new List<SubMenu>() :
                                     (from SubMenu in menu.Element("DetalleSubMenu").Elements("SubMenu")
                                      select new SubMenu()
                                      {
                                          nombreSub = SubMenu.Element("nombre").Value,
                                          nombreFormulario = SubMenu.Element("nombreFormulario").Value
                                      }
                                     ).ToList()
                                 }).ToList();
                        }//fin del if
                    }//fin de while                 
                }
                catch (Exception e)
                {
                    listPermiso = new List<Menu>();

                    throw;
                }

            }
            return listPermiso;
        }
    }
}
