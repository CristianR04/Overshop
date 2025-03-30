using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overshop.Models;
using Overshop.Servicios;

namespace Overshop.Controllers
{
    public class ComprasController : Controller
    {
        private readonly IRepositorioCompras repocompras;
        public ComprasController(IRepositorioCompras repositoriocompras)
        {
            this.repocompras = repositoriocompras;
        }
        public IActionResult Compras(ingresarcompra ingresarcompras)
        {
            ingresarcompra ingresarcompra = new ingresarcompra();
            if (!ModelState.IsValid)
                return View(ingresarcompras);

            repocompras.Compras(ingresarcompras);
            TempData["SuccessMessage"] = "El registro de compras fue exitoso.";
            return View(ingresarcompras);
        }

        [HttpGet]
        public JsonResult detalleproveedor(int id)
        {
            ingresarcompra detalle = repocompras.detalleproveedor(id);
            return Json(detalle.Empresa);
        }

    }
}
