using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Ejercicio_Hotel
{
    class Program
    {
        //static string connectionStringAmaro = ConfigurationManager.ConnectionStrings["DDBB_HOTELAmaro"].ConnectionString;
        static string connectionStringAlex = ConfigurationManager.ConnectionStrings["DDBB_HOTELAlex"].ConnectionString;
        //static SqlConnection conexion = new SqlConnection(connectionStringAmaro);
        static SqlConnection conexion = new SqlConnection(connectionStringAlex);
        //static string cadena;
        //static SqlCommand comando;
        //static SqlDataReader registros;

        static void Main(string[] args)
        {
            conexion.Open();
            string query = "SELECT * FROM Habitaciones";
            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Console.WriteLine(registros["CodHabitacion"].ToString() + "\n" +
                    "\t" + registros["Estado"]);
            }
        }
    }
}
