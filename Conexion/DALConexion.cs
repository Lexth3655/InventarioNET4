using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class DALConexion
    {
        //Cadena de Conexion hacia el servidor y base de datos
        public static string sqlCn = "Server=LEXTH3655;Initial Catalog=Inventario; Integrated Security=true";


        //Cadena de conexion para el acceso a la DB Inventario
        private SqlConnection conexion = new SqlConnection("Server=LEXTH3655;Initial Catalog=Inventario; Integrated Security=true");

        //Se abre la conexion a la base de datos
        public SqlConnection OpenConnection()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
            return conexion;
        }

        //Se Cierra la conexion con la base de datos
        public SqlConnection CloseConnection()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Close();
            }
            return conexion;
        }
    }
}
