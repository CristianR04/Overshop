using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity.Data;
using Overshop.Models;
using System.Data;


namespace Overshop.Servicios
{

    public interface IRepositorioUsuario
    {

        Task<bool> registromodel(registromodel usuario);
        Task<bool> Contacto(Contactomodel contacto);
        Task<bool> verificarusuario(login info);
        
    }

    public class RepositorioUsuario : IRepositorioUsuario
    {

        private readonly string cnx;
        public RepositorioUsuario(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> registromodel(registromodel usuario)
        {
            bool isInserted = false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                     @"INSERT INTO Modelo (identificacion, nombre, sex, correo, nacimiento, apellido, password)
                    VALUES (@identificacion, @nombre, @sex, @correo, @nacimiento, @apellido, @password)", usuario) >
                             0;
            }
            catch (Exception ex)
            {
            }
            //encabezado.idCronograma = id
            return isInserted;
        }

        public async Task<bool> Contacto(Contactomodel contacto)
        {
            bool isInserted = false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                     @"INSERT INTO Contacto (nombre, correo, mensaje)
                    VALUES (@nombre, @correo, @mensaje)", contacto) >
                             0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return isInserted;
        }

        public async Task<bool> verificarusuario(login info)

        {
            using var connection = new SqlConnection(cnx);
            string query = @"SELECT COUNT(1) FROM Modelo WHERE correo = @correo AND password = @password";
            bool usuarioexiste = await connection.ExecuteScalarAsync<int>(query, new { info.correo, info.password }) > 0;
            return usuarioexiste;
        }

        
    }
}
