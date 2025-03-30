using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overshop.Models;
using Overshop.Servicios;
using System.Diagnostics.Contracts;

namespace Overshop.Controllers
{
    public class IniciarsesionController : Controller
    {
        private readonly IRepositorioUsuario repocontacto;
        public IniciarsesionController(IRepositorioUsuario repocontacto)
        {
            this.repocontacto = repocontacto;
        }

        public IActionResult registro()
        {

            return View();
        }

        public IActionResult Contacto()
        {
            return View();

        }
        public IActionResult enviarcontacto(Contactomodel contacto)
        {
            if (!ModelState.IsValid)
                return View("~/views/Iniciarsesion/Contacto.cshtml");

            repocontacto.Contacto(contacto);
            TempData["SuccessMessage"] = "El registro fue exitoso."; 
            return View("~/views/Iniciarsesion/Contacto.cshtml");
        }

    }
}
