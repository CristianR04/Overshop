using Dapper;
using Microsoft.Data.SqlClient;
using Overshop.Models;
using System.Data;

namespace Overshop.Servicios
{
    public interface IRepositorioCompras
    {
        ingresarcompra detalleproveedor(int  Idproveedor);
        Task<bool> Compras(ingresarcompra ingresarcompras);
    }

    public class RepositorioCompras : IRepositorioCompras
    {
        private readonly string cnx;
        public RepositorioCompras(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> Compras(ingresarcompra ingresarcompras)
        {
            bool isInserted = false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                     @"INSERT INTO Compra ( fecha, Nombre, Empresa, Idproveedor, Id, Cantidad, valor, Valortotal )
                        VALUES (@fecha, @Nombre, @Empresa, @Idproveedor, @Id, @Cantidad, @valor, @Valortotal )", ingresarcompras) >
                             0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return isInserted;
        }

        

        public ingresarcompra detalleproveedor (int Idproveedor)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT Idproveedor, Empresa FROM Proveedor WHERE Idproveedor= @Idproveedor";

                ingresarcompra model = db.QuerySingleOrDefault<ingresarcompra>(sqlQuery, new { Idproveedor });

                return model;
            }
        }
    }
}
