using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using Overshop.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;
using static Overshop.Models.carroitem;

namespace Overshop.Servicios
{


    public interface IRepositoriocarrito
    {
        void AgregarItemCarro(Producto Id, int cantidad);
        void EliminarItemCarro(int Id);
        List<carroitem> listarItemsCarro();
        void ActualizarItemCarro(int Id, int cantidad);
       
    }

    public class Repositoriocarrito : IRepositoriocarrito
    {
        private readonly productoSeleccionado _productoSeleccionado;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string cnx;
        public Repositoriocarrito(productoSeleccionado __productoSeleccionado, IHttpContextAccessor __httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = __httpContextAccessor;
            _productoSeleccionado = __productoSeleccionado;
            cnx = configuration.GetConnectionString("DefaultConnection");
        }
        private productoSeleccionado obtenerItemsSesion()
        {
            var Session = _httpContextAccessor.HttpContext.Session;
            var cartJson = Session.GetString("Carrito");
            return string.IsNullOrEmpty(cartJson)
                ? new productoSeleccionado()
                : JsonSerializer.Deserialize<productoSeleccionado>(cartJson);
        }
        private void guardarItemsSesion(productoSeleccionado cart)
        {
            var Session = _httpContextAccessor.HttpContext.Session;
            Session.SetString("Carrito", JsonSerializer.Serialize(cart));
        }

        

        public void AgregarItemCarro(Producto Id, int cantidad)
        {
            var cart= obtenerItemsSesion();

            var existingItem = cart.Items.FirstOrDefault(i=> i.producto.Id == Id.Id);

            if (existingItem != null) 
            {
                existingItem.cantidad += cantidad;
            }
            else
            {
                cart.Items.Add(new carroitem { cantidad = cantidad, producto = Id });
            }
            guardarItemsSesion(cart);
        }
        public void EliminarItemCarro(int Id)
        {
            var cart = obtenerItemsSesion();
            var item = cart.Items.FirstOrDefault(i => i.producto.Id == Id);
            if (item != null)
            {
                cart.Items.Remove(item);
                guardarItemsSesion(cart);
            }
        }
        public decimal obtenertotal()
        {
            return _productoSeleccionado.TotalPrecio;
        }
        public void ActualizarItemCarro(int Id, int cantidad)
        {
            var cart = obtenerItemsSesion();
            var existeItem = cart.Items.FirstOrDefault(i => i.producto.Id == Id);

            if (existeItem != null)
            {
                existeItem.cantidad = cantidad;
            }
            guardarItemsSesion(cart);
        }

        public List<carroitem> listarItemsCarro()
        {
            return obtenerItemsSesion().Items;
        }
    }

}
