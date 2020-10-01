﻿using System;
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
               
            }
            //RegistroCliente();
            int rows = 0;
            do
            {
                Console.WriteLine("Introduzca el DNI");
                rows = EditarCliente(Console.ReadLine());

            } while (rows == 0);*/

           // CheckOut(Console.ReadLine());

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
        static int EditarCliente(string DNI)
        {
            conexion.Open();
            string querySelect = $"SELECT nombre, apellido from clientes where dni = '{DNI}'";
            SqlCommand comando = new SqlCommand(querySelect, conexion);
            SqlDataReader reader = comando.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine("El nombre es {0} y el apellido es {1}", reader[0].ToString(), reader[1].ToString());
                conexion.Close();
                conexion.Open();
                Console.WriteLine("Introduzca el nombre correcto");
                string nombre = Console.ReadLine(); ;
                Console.WriteLine("Introduzca el apellido correcto");
                string apellido = Console.ReadLine();
                string queryUpdate = $"Update Clientes set nombre = '{nombre}' , apellido = '{apellido}' where dni = '{DNI}' ";
                comando = new SqlCommand(queryUpdate, conexion);
                int rows = comando.ExecuteNonQuery();
                conexion.Close();
                return rows;
            }
            conexion.Close();
            return 0;

        }
        static void CheckIn()
        {

        }
        static void CheckOut(string DNI)
        {
            conexion.Open();
            string querySelect = $"SELECT * from reservas where dniCliente = '{DNI}' and fechacheckout is null";
            SqlCommand comando = new SqlCommand(querySelect, conexion);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                do
                {
                    Console.WriteLine(reader[2].ToString(), reader[3].ToString());

                } while (reader.Read());
                Console.WriteLine("Introduzca el numero de habitacion");
                int hab = Convert.ToInt32(Console.ReadLine().ToString());
                conexion.Close();
                conexion.Open();
                string queryUpdateReserva = $"UPDATE Reservas set fechacheckout ='{DateTime.Now}' where dniCliente = '{DNI}' and " +
                    $"codhabitacion = '{hab}'";//cierra la reserva
                comando = new SqlCommand(queryUpdateReserva, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                conexion.Open();
                string queryUpdateHabitacion = $"UPDATE Habitaciones set estado = '0' where codhabitacion = {hab}";//libera habitacion
                comando = new SqlCommand(queryUpdateHabitacion, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            else {
                conexion.Close();
                Console.WriteLine($"No hay reservas pendientes de cerrar para el cliente con dni {DNI}");
            }
        }
        static void VerHabit()
        {

        }
    }
}
