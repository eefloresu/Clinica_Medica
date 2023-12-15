using MySql.Data.MySqlClient;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sistema_ClinicaMedica.Models
{
    public class ProjectContext
    {
        private static MySqlConnection conexion;
        private static Users usuario;
        private static String sitio = ""; //Para hacer pruebas locales dejar vacia
        //private static String sitio = "Sistema_ClinicaMedica"; //Antes de publicar 

        public ProjectContext()
        {
            usuario = new Users();
            usuario.user = "root";
            usuario.pass = "GRRM@8398/*";
        }

        public ProjectContext(String user, String pass)
        {
            usuario = new Users();
            usuario.user = user;
            usuario.pass = pass;
            conexion = null;
            ObtenerConexion();
        }
        //Validar conexiones
        public Operation validaConexion()
        {
            Operation operacion = new Operation();
            if (conexion != null)
            {
                try
                {
                    if (conexion.State != System.Data.ConnectionState.Open)
                    {
                        conexion.Open();
                        string sql = "SHOW GRANTS FOR CURRENT_USER()";

                        // Crear un objeto MySqlCommand
                        MySqlCommand command = new MySqlCommand(sql, conexion);

                        // Ejecutar la consulta y obtener un objeto MySqlDataReader
                        MySqlDataReader reader = command.ExecuteReader();

                        // Recorrer los resultados de la consulta
                        while (reader.Read())
                        {
                            String permisos = reader.GetString(0);
                            if (permisos.Contains("cuentasxcobrar") || usuario.user == "root")
                            {
                                operacion.esValida = permisos.ToUpper().Contains("SELECT");

                                if (!operacion.esValida)
                                {
                                    operacion.Mensaje = "Usuario no tiene permisos para realizar operaciones";
                                }
                                else
                                {
                                    operacion.Mensaje = "";
                                    break;
                                }

                            }
                            else
                                continue;

                            // ...
                        }
                        conexion.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1044)
                    {
                        operacion.esValida = false;
                        operacion.Mensaje = "Usuario no tiene permisos para realizar operaciones en el sistema";
                    }
                }
            }
            return operacion;
        }

        public static MySqlConnection ObtenerConexion()
        {
            try
            {
                if (conexion == null)
                {
                    // Parámetros de conexión
                    string server = "localhost";
                    string database = "sistema_cuentasXcobrar";
                    string uid = usuario.user;
                    string password = usuario.pass;

                    // Cadena de conexión
                    string connectionString = $"Server={server};Database={database};Uid={uid};Pwd={password};";

                    // Crear la conexión
                    conexion = new MySqlConnection(connectionString);
                }
            }
            catch (MySqlException ex)
            {

            }


            return conexion;
        }


        public bool EscribirLogs(String linea, String operacion = "")
        {
            try
            {
                String mensajeFinal = String.Format("{1}:{2}:{0}",
                    linea,
                    DateTime.Now.ToString("HH:mm:ss"),
                    operacion
                    );
                String ruta = "C:\\Logs\\Sistema_ClinicaMedica\\";
                string fileName = ruta + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                // Crea el archivo y escribe el texto en él
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine(mensajeFinal);

                }
            }
            catch
            {

            }


            return false;
        }

/*
 CRUD
 */

        public String getSitio()
        {
            return sitio;
        }
    }
}
