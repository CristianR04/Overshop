using Dapper;
using Microsoft.Data.SqlClient;
using Overshop.Models;
using System.Data;

namespace Overshop.Servicios
{
    public interface IRepositorioPDF
    {
        List<proveedormodel> HacerPDF(proveedormodel hacer);
        List<Contactomodel> contactar(Contactomodel pdfcontactanos);
        List<Inventariomodel> Inventario(Inventariomodel generar);
        List<registromodel> registro(registromodel registrar);
    }
    public class RepositorioPDF : IRepositorioPDF
    {
        private readonly string cnx;
        private readonly IConfiguration configuration;
        public RepositorioPDF(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }
        public List<proveedormodel> HacerPDF(proveedormodel Hacer)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT * FROM Proveedor";
                var productos = db.Query<proveedormodel>(sqlQuery).ToList();
                return productos;
            }
        }

        public List<Inventariomodel> Inventario(Inventariomodel generar)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(cnx);
            var query = "SELECT * FROM inventario";
            using var generarpdf = new SqlConnection(connectionString);
            var pdf = connection.Query<Inventariomodel>(query).ToList();

            return pdf;
        }

        public List<registromodel> registro(registromodel registrar)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(cnx);
            var query = "SELECT * FROM Modelo";
            using var generar = new SqlConnection(connectionString);
            var pdf = connection.Query<registromodel>(query).ToList();

            return pdf;

        }

        public List<Contactomodel> contactar(Contactomodel pdfcontactanos)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(cnx);
            var query = "SELECT * FROM Contacto";
            using var generar = new SqlConnection(connectionString);
            var pdf = connection.Query<Contactomodel>(query).ToList();

            return pdf;

        }
    }
}
