using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Ejercicio_Hotel
{
    class Program
    {
        string Disponible = "Disponible";
        string Ocupado = "Ocupada";
        static string connectionStringAmaro = ConfigurationManager.ConnectionStrings["DDBB_HOTEL_Amaro"].ConnectionString;
        //static string connectionStringAlex = ConfigurationManager.ConnectionStrings["DDBB_HOTEL_Alejandro"].ConnectionString;
        static SqlConnection conexion = new SqlConnection(connectionStringAmaro);
        //static SqlConnection conexion = new SqlConnection(connectionStringAlex);


        static void Main(string[] args)
        {
            Menu();
            /*
            RegistroCliente();
            int rows = 0;
            do
            {
                Console.WriteLine("Introduzca el DNI");
                rows = EditarCliente(Console.ReadLine());

            } while (rows == 0);

            CheckOut(Console.ReadLine());

            CheckIn();
            */
        }
        static void Menu()
        {
            int answer;
            string[] mainMenu = { "1. Registrar Cliente", "2. Editar Cliente", "3. Check-in", "4. Check-out", "5. Salir" };
            Console.WriteLine("Elige una opción:");
            Console.WriteLine(mainMenu[0] + "\n" +
                mainMenu[1] + "\n" +
                mainMenu[2] + "\n" +
                mainMenu[3] + "\n" +
                mainMenu[4] + "\n");
            answer = Convert.ToInt32(Console.ReadLine());

            switch (answer)
            {
                case 1:
                    Console.WriteLine("Has elegido registrar un cliente.");
                    RegistroCliente();
                    break;
                case 2:
                    string dni;
                    Console.WriteLine("Has elegido editar un cliente. \n " +
                        "Ingresa DNI de cliente.");
                    dni = Console.ReadLine();
                    EditarCliente(dni);
                    break;
                case 3:
                    Console.WriteLine("Has elegido hacer un check in.");
                    CheckIn();
                    break;
                case 4:
                    Console.WriteLine("Has elegido hacer un check out. \n " +
                        "Ingresa DNI de cliente.");
                    dni = Console.ReadLine();
                    CheckOut(dni);
                    break;
                default:
                    Console.WriteLine("Hasta luego MariCarmen.");
                    break;
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
                string dni;
                Console.WriteLine("Ingresa tu DNI:");
                dni = Console.ReadLine();
                conexion.Open();
                string query = "select * from Clientes where DNI = '" + dni + "'";
                string query2 = "select CodHabitacion from Habitaciones where Estado = 0";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                if (registros.Read())
                {
                    Console.WriteLine("Bienvenida(o)" + " " + registros["Nombre"] + " " + registros["Apellido"]);
                    conexion.Close();
                    conexion.Open();
                    comando = new SqlCommand(query2, conexion);
                    registros = comando.ExecuteReader();
                    if (registros.Read())
                    {
                        Console.WriteLine("Elige una habitación");
                        do
                        {
                            Console.WriteLine(registros["CodHabitacion"].ToString());
                        } while (registros.Read());
                        conexion.Close();
                        conexion.Open();
                        string hab = Console.ReadLine();
                        string queryInsertReserva = $"Insert into Reservas (DNICliente, CodHabitacion, FechaCheckIn) values ('{dni}', {hab}, '{DateTime.Now}')";
                        string queryUpdateHab = $"UPDATE Habitaciones set Estado = 1 where CodHabitacion = '{hab}'";
                        comando = new SqlCommand(queryInsertReserva, conexion);
                        comando.ExecuteNonQuery();
                        conexion.Close();
                        conexion.Open();
                        comando = new SqlCommand(queryUpdateHab, conexion);
                        comando.ExecuteNonQuery();
                        conexion.Close();
                    }
                    else
                    {
                        Console.WriteLine("pos te j0des jeje");
                    }
                }
                else
                {
                    Console.WriteLine("No estás registrado");
                }
                conexion.Close();
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
                else
                {
                    conexion.Close();
                    Console.WriteLine($"No hay reservas pendientes de cerrar para el cliente con dni {DNI}");
                }


            }

            static void VerHab()
            {

                string queryHabVacias = "SELECT * from Habitaciones where estado = 0";
                string queryHabCliente = "SELECT Clientes.Nombre,Clientes.Apellido,[Habitaciones].CodHabitacion " +
                    "FROM[DDBB_HOTEL].[dbo].[Reservas], Habitaciones, Clientes " +
                    "where Reservas.CodHabitacion = Habitaciones.CodHabitacion " +
                    "and FechaCheckOut is null " +
                    "and Clientes.DNI = DNICliente " +
                    "and Habitaciones.Estado = 1";
                string queryAll = "SELECT * FROM Habitaciones h FULL OUTER JOIN Reservas r ON r.CodHabitacion=h.CodHabitacion and r.FechaCheckOut is null " +
                    "FULL OUTER JOIN Clientes ON Clientes.DNI=r.DNICliente " +
                    "where r.FechaCheckOut is null";
                conexion.Open();
                SqlCommand comando = new SqlCommand(queryAll, conexion);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader[0].ToString() != "" ? reader[0].ToString() : "----", reader[1].ToString() != "" ? reader[1].ToString() : "----",
                        reader[2].ToString(), reader[3].ToString());
                }
                conexion.Close();
                Console.WriteLine("\n\n********************************************************************\n");

                conexion.Open();
                comando = new SqlCommand(queryHabCliente, conexion);
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}", reader[0].ToString() != "" ? reader[0].ToString() : "----", reader[1].ToString() != "" ? reader[1].ToString() : "----",
                        reader[2].ToString());
                }
                conexion.Close();

                Console.WriteLine("\n\n********************************************************************\n");
                conexion.Open();
                comando = new SqlCommand(queryHabVacias, conexion);
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}", reader[0].ToString(), reader[1].ToString());
                }
                conexion.Close();
            }
        }
    }
}
