using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overshop.Servicios;
using Overshop.Models;
using static Overshop.Models.carroitem;
namespace Overshop.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IRepositoriocarrito _repositoriocarrito;
        private readonly IRepositorioHome _repositorioHome;
        public CarritoController(IRepositoriocarrito repositoriocarrito, IRepositorioHome repositorioHome)
        {
            _repositoriocarrito = repositoriocarrito;
            _repositorioHome = repositorioHome;
        }


        public IActionResult agregar(int Id, int cantidad)
        {
            var detalle = _repositorioHome.selectcarro(Id,cantidad);
            if (detalle != null)
            {
                _repositoriocarrito.AgregarItemCarro(detalle, cantidad);
            }
            var carritoItems = _repositoriocarrito.listarItemsCarro();
            return View("Carrito", carritoItems);
        }

        public IActionResult eliminar(int Id)
        {
            _repositoriocarrito.EliminarItemCarro(Id);
            var carritoItems = _repositoriocarrito.listarItemsCarro();
            return View("carrito", carritoItems);
        }

        public IActionResult actualizarItem(int Id, int cantidad)
        {
            if (cantidad < 1)
            {
                return BadRequest("La cantidad debe ser al menos 1.");
            }
            _repositoriocarrito.ActualizarItemCarro(Id, cantidad);
            var carritoItems = _repositoriocarrito.listarItemsCarro();
            return View("carrito", carritoItems);
        }


        public IActionResult Carrito()
        {

            return View(); 

        }

        public IActionResult menu()
        {
            return View();
        }
    }
}
