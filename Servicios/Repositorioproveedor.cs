using Dapper;
using Microsoft.Data.SqlClient;
using Overshop.Models;

namespace Overshop.Servicios
{
    public interface Irepositorioproveedor
    {
        Task<bool> proveedor(proveedormodel proveedor);
    }
    public class Repositorioproveedor : Irepositorioproveedor
    {
        private readonly string cnx;
        public Repositorioproveedor(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> proveedor(proveedormodel proveedor)
        {
            bool isInserted = false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                     @"INSERT INTO Proveedor ( Idproveedor, Nombres, Empresa, Direccion, Telefono, TelefonoEmpresa, Correoprov, CorreoEmpresa, TipoProv)
                        VALUES (@Idproveedor, @Nombres, @Empresa, @Direccion, @Telefono, @TelefonoEmpresa, @Correoprov, @CorreoEmpresa, @TipoProv)", proveedor) >
                             0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return isInserted;
        }
    }
}
