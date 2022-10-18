using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.Modelos;

namespace Curso.Handlers
{
    public class MetodosUsuario
    {
        public static Usuario Traer_Usuario(string Nombre)
        {
            Usuario usuario = new Usuario();

            SqlConnectionStringBuilder connectionBuilder = new();
            connectionBuilder.DataSource = "LAPTOP-ANAQNMU4";
            connectionBuilder.InitialCatalog = "SistemaGestion";
            connectionBuilder.IntegratedSecurity = true;
            var cs = connectionBuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                string commText = "SELECT * FROM Usuario WHERE Nombre =@NomUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("NomUsu", SqlDbType.VarChar);
                Parametero.Value = Nombre;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = Convert.ToString(reader.GetValue(1));
                    usuario.Apellido = Convert.ToString(reader.GetValue(2));
                    usuario.NombreUsuario = Convert.ToString(reader.GetValue(3));
                    usuario.Contraseña = Convert.ToString(reader.GetValue(4));
                    usuario.Mail = Convert.ToString(reader.GetValue(5));
                }
                reader.Close();
                connection.Close();
                return usuario;

            }
        }

        public static Usuario inicio_Sesion(string nombreUsuario, string Contrasena)
        {
            Usuario usuario = new Usuario();

            SqlConnectionStringBuilder connectionBuilder = new();
            connectionBuilder.DataSource = "LAPTOP-ANAQNMU4";
            connectionBuilder.InitialCatalog = "SistemaGestion";
            connectionBuilder.IntegratedSecurity = true;
            var cs = connectionBuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                string commText = "SELECT Contraseña FROM Usuario WHERE NombreUsuario =@NomUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("NomUsu", SqlDbType.VarChar);
                Parametero.Value = nombreUsuario;
                Comm.Parameters.Add(Parametero);
                var holder = Convert.ToString(Comm.ExecuteScalar());

                if (holder == Contrasena)
                {
                    string commText1 = "SELECT * FROM Usuario WHERE NombreUsuario =@NomUsu";
                    SqlCommand Comm1 = new SqlCommand(commText1, connection);

                    var Parametero1 = new SqlParameter("NomUsu", SqlDbType.VarChar);
                    Parametero1.Value = nombreUsuario;
                    Comm1.Parameters.Add(Parametero1);
                    var reader = Comm1.ExecuteReader();

                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader.GetValue(0));
                        usuario.Nombre = Convert.ToString(reader.GetValue(1));
                        usuario.Apellido = Convert.ToString(reader.GetValue(2));
                        usuario.NombreUsuario = Convert.ToString(reader.GetValue(3));
                        usuario.Contraseña = Convert.ToString(reader.GetValue(4));
                        usuario.Mail = Convert.ToString(reader.GetValue(5));
                    }
                    reader.Close();
                    connection.Close();
                    return usuario;
                }
                else return usuario;

            }
        }

        public static void contrasena(string nombreUsuario)
        {

            SqlConnectionStringBuilder connectionBuilder = new();
            connectionBuilder.DataSource = "LAPTOP-ANAQNMU4";
            connectionBuilder.InitialCatalog = "SistemaGestion";
            connectionBuilder.IntegratedSecurity = true;
            var cs = connectionBuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                string commText = "SELECT Contraseña FROM Usuario WHERE Nombre =@NomUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("NomUsu", SqlDbType.VarChar);
                Parametero.Value = nombreUsuario;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteScalar();
                Console.WriteLine(reader.ToString());
            }
        }
    }
}
