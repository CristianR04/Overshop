using Overshop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Overshop.Servicios;

namespace Overshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorioHome repohome;
        public HomeController(IRepositorioHome repositorioHome)
        {
            this.repohome = repositorioHome;
        }
        public IActionResult menu()
        {

            IEnumerable<Inventariomodel> productos = repohome.ListarProductos();
            return View(productos);
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Carrito()
        {

            return View();
        }
        public IActionResult Contacto()
        {

            return View();

        }
        public IActionResult Indexproductos()
        {
            return View("Administrador");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult menusuario()
        {
            IEnumerable<Inventariomodel> productos = repohome.ListarProductos();
            return View(productos);
        }

        public async Task<IActionResult> Administrador(Inventariomodel model)
        {
            try
            {
                if (model != null && model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var extension = Path.GetExtension(model.ImageFile.FileName);
                    var nuevoNombre = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/inventario", nuevoNombre);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    model.urlimagen = "./inventario/" + nuevoNombre;
                    
                    repohome.Administrador(model);

                }
            }
            catch
            {

            }
            TempData["SuccessMessage"] = "El registro de Inventario fue exitoso.";
            return View();
        }

        [HttpGet]
        public JsonResult detalleProducto(int id)
        {
            Inventariomodel detalle = repohome.detalleProducto(id);
            return Json(detalle);
        }



        public IActionResult contraseña()
        {
            return View();
        }

        public IActionResult ValidarLogin(IFormCollection formulario)
        {

            return View();


        }




    }

}
