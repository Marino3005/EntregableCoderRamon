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
    public class MetodosVentas
    {
        public static List<Venta> Traer_Ventas(int IdUsuario)
        {
            var listaVentas = new List<Venta>();

            SqlConnectionStringBuilder connectionBuilder = new();
            connectionBuilder.DataSource = "LAPTOP-ANAQNMU4";
            connectionBuilder.InitialCatalog = "SistemaGestion";
            connectionBuilder.IntegratedSecurity = true;
            var cs = connectionBuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {


                connection.Open();
                string commText = "SELECT * FROM Venta WHERE IdUsuario =@IdUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);
                var Parametero = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero.Value = IdUsuario;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    var Venta = new Venta();

                    Venta.id = Convert.ToInt32(reader.GetValue(0));
                    Venta.Comentarios = Convert.ToString(reader.GetValue(1));
                    Venta.IdUsuario = Convert.ToInt32(reader.GetValue(2));
                   

                    listaVentas.Add(Venta);
                }
                reader.Close();
            }
            return listaVentas;
        }
    }
}
