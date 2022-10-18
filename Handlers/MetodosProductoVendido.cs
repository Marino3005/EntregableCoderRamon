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
    public class MetodosProductoVendido
    {
        public static List<ProductoVendido> Traer_ProductoVendido(int IdUsuario)
        {
            var listaProductosVendidos = new List<ProductoVendido>();

            SqlConnectionStringBuilder connectionBuilder = new();
            connectionBuilder.DataSource = "LAPTOP-ANAQNMU4";
            connectionBuilder.InitialCatalog = "SistemaGestion";
            connectionBuilder.IntegratedSecurity = true;
            var cs = connectionBuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {


                connection.Open();
                string commText = "SELECT  pv.Id, pv.Stock, pv.IdProducto, pv.IdVenta FROM Producto as p INNER JOIN ProductoVendido as pv ON p.Id = pv.IdProducto WHERE p.IdUsuario = @IdUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);
                var Parametero = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero.Value = IdUsuario;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    var Produc = new ProductoVendido();

                    Produc.id = Convert.ToInt32(reader.GetValue(0));
                    Produc.Stock = Convert.ToInt32(reader.GetValue(1));
                    Produc.IdProducto = Convert.ToInt32(reader.GetValue(2));
                    Produc.IdVenta = Convert.ToInt32(reader.GetValue(3));

                    listaProductosVendidos.Add(Produc);
                }
                reader.Close();
            }
            return listaProductosVendidos;
        }
    }
}
