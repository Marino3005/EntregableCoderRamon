using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.Handlers
{
    public class MetodosProducto
    {
        public static List<Producto> Traer_Producto(int IdUsuario)
        {
            var listaProductos = new List<Producto>();

            SqlConnectionStringBuilder connectionBuilder = new();
            connectionBuilder.DataSource = "LAPTOP-ANAQNMU4";
            connectionBuilder.InitialCatalog = "SistemaGestion";
            connectionBuilder.IntegratedSecurity = true;
            var cs = connectionBuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {


                connection.Open();
                string commText = "SELECT * FROM Producto WHERE IdUsuario =@IdUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);
                var Parametero = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero.Value = IdUsuario;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    var Produc = new Producto();

                    Produc.Id = Convert.ToInt32(reader.GetValue(0));
                    Produc.Descripciones = Convert.ToString(reader.GetValue(1));
                    Produc.Costo = Convert.ToDouble(reader.GetValue(2));
                    Produc.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    Produc.Stock = Convert.ToInt32(reader.GetValue(4));
                    Produc.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(Produc);
                }
                reader.Close();
            }
            return listaProductos;
        }
    }
    
}
