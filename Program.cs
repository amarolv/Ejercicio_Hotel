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
            /*Check-in: Aquí pediremos el DNI del cliente que quiere hacer la reserva. Si el cliente no 
            existe en la tabla clientes aparecerá un mensaje que nos indique que el cliente no está 
            registrado y por lo tanto no puede hacer una reserva.*/
            //1.- Pedir DNI con console.readline()
            //2.- Revisar en BBDD si existe mediante consulta/select
            //3.- Si existe, asignar a tabla RESERVAS (CodReserva, DNICliente, CodHabitacion, FechaCheckIn, FechaCheckOut)

            string dni;
            Console.WriteLine("Ingresa tu DNI:");
            dni = Console.ReadLine();

            conexion.Open();
            string query = "Select DNI from Clientes";
            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())

                conexion.Close();
        }
        static void CheckOut(string DNI)
        {
            
        }
        static void VerHabit()
        {

        }
    }
}
