using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Ejercicio_Hotel
{
    class Program
    {
        string Disponible = "Disponible";
        string Ocupado = "Ocupada";
        //static string connectionStringAmaro = ConfigurationManager.ConnectionStrings["DDBB_HOTEL_Amaro"].ConnectionString;
        static string connectionStringAlex = ConfigurationManager.ConnectionStrings["DDBB_HOTEL_Alejandro"].ConnectionString;
        //static SqlConnection conexion = new SqlConnection(connectionStringAmaro);
        static SqlConnection conexion = new SqlConnection(connectionStringAlex);
     

        static void Main(string[] args)
        {
            /*
            conexion.Open();
            string query = "SELECT * FROM Habitaciones";
            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Console.WriteLine(registros["CodHabitacion"].ToString() + "\n" +
                    "\t" + registros["Estado"]);
            }
            */


        }
    }
}
