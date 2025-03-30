using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Overshop.Models;
using System.Data;
using System.Reflection;

namespace Overshop.Servicios
{
    public interface IRepositorioHome
    {
        Producto selectcarro(int Id, int cantidad);
        Task<bool> Administrador(Inventariomodel model);
        Inventariomodel detalleProducto(int id);
        IEnumerable<Inventariomodel> ListarProductos();
    }

    public class RepositorioHome : IRepositorioHome
    {
        private readonly string cnx;
        public RepositorioHome(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> Administrador(Inventariomodel model)
        {
            bool isInserted = false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                     @"INSERT INTO Inventario (Id, Nombre, descripcion, valor, estado, urlimagen, marca, modelo, accesorios)
                    VALUES (@Id, @Nombre, @descripcion, @valor, @estado, @urlimagen, @marca, @modelo, @accesorios)", model) >
                             0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return isInserted;
        }

        public Inventariomodel detalleProducto(int Id)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT Id, Nombre, descripcion, valor, estado, urlimagen, marca, modelo, accesorios FROM inventario WHERE Id= @Id";

                Inventariomodel model = db.QuerySingleOrDefault<Inventariomodel>(sqlQuery, new { Id });

                return model;
            }
        }

        

        public Producto selectcarro(int Id, int cantidad)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string query = "SELECT * FROM Inventario WHERE Id = @Id";
                Producto product = db.QuerySingleOrDefault<Producto>(query, new { Id });
                return product;
            }
        }
        public IEnumerable<Inventariomodel> ListarProductos()
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT * FROM Inventario";
                var productos = db.Query<Inventariomodel>(sqlQuery).ToList();
                return productos;
            }

        }
    }
}
