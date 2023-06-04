using Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Roles
    {
        private DALConexion conexion =  new DALConexion();
        SqlDataReader dataReader;
        DataTable tableRol = new DataTable();
        SqlCommand comand = new SqlCommand();

        //Metodo para insertar datos a la table Rol
        public void CrearRol(string rol, bool estado)
        {
            comand.Connection = conexion.OpenConnection();
            comand.CommandText = "sp_Insertar_Rol";
            comand.CommandType = CommandType.StoredProcedure;
            comand.Parameters.AddWithValue("@rol", rol);
            comand.Parameters.AddWithValue("@estado", estado);
            comand.ExecuteNonQuery();
            comand.Parameters.Clear();
        }
        //Metodo para Actulizar registro 
        public void EditarRol(string rol, bool estado, int id)
        {
            comand.Connection = conexion.OpenConnection();
            comand.CommandText = "sp_Actualizar_Rol";
            comand.CommandType = CommandType.StoredProcedure;
            comand.Parameters.AddWithValue("@nombre", rol);
            comand.Parameters.AddWithValue("@estado", estado);
            comand.Parameters.AddWithValue("@id", id);
            comand.ExecuteNonQuery();
            comand.Parameters.Clear();

        }

        //Metodo para mostrar los datos
        public DataTable MostrarAllRoles()
        {
            comand.Connection =  conexion.OpenConnection ();
            comand.CommandText = "sp_Mostrar_Roles";
            comand.CommandType= CommandType.StoredProcedure;
            dataReader = comand.ExecuteReader();
            tableRol.Load(dataReader);
            conexion.CloseConnection();
            
            return tableRol;
        }
    }
}
