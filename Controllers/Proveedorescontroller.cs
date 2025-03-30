using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overshop.Models;

using Overshop.Servicios;
using System.Diagnostics.Contracts;

namespace Overshop.Controllers
{
    public class Proveedorescontroller : Controller
    {
        private readonly Irepositorioproveedor repoproveedor;
        public Proveedorescontroller(Irepositorioproveedor repoproveedor)
        {
            this.repoproveedor = repoproveedor;
        } 

        public IActionResult Proveedores()
        {
            return View();
        }

        public IActionResult Proveedor(proveedormodel proveedor)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Proveedores/Proveedores.cshtml");

            repoproveedor.proveedor(proveedor);
            TempData["SuccessMessage"] = "El registro de Proveedor fue exitoso.";
            return View("~/Views/Proveedores/Proveedores.cshtml");

        }
    }
}
