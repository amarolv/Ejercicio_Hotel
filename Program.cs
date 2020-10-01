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
            string query = "SELECT asd FROM qwer";
            string query2 = "SELECT qwer FROM asd";
            SqlCommand comando = new SqlCommand(query2, conexion);
            comando.ExecuteNonQuery();
            {
               
            }*/
            Menu();


        }
        static void Menu()
        {
            
        }
        static void RegistroCliente()
        {
            string name;
            string lastName;
            string dni;
            Console.WriteLine("Ingresa tu nombre:");
            name = Console.ReadLine();
            Console.WriteLine("Ingresa tu apellido:");
            lastName = Console.ReadLine();
            Console.WriteLine("Ingresa tu DNI:");
            dni = Console.ReadLine();

            conexion.Open();
            string query1 = "insert into Clientes (Nombre,Apellido,DNI) values ('" + name + "','" + lastName + "','" + dni + "')";
            SqlCommand comando = new SqlCommand(query1, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        static void EditarCliente(string DNI)
        {
            
        }
        static void CheckIn()
        {
           
        }
        static void CheckOut(string DNI)
        {

        }
        static void VerHabit()
        {

        }
    }
}
