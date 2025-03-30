using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overshop.Servicios;
using Overshop.Models;
using iText.Commons.Actions.Contexts;


namespace Overshop.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IRepositorioUsuario repousuario;


        public UsuarioController(IRepositorioUsuario Repousuario)
        {

            this.repousuario = Repousuario;
        }



        public IActionResult registro(registromodel usuario)
        {

            if (!ModelState.IsValid)
            {

                return View("~/views/Iniciarsesion/registro.cshtml", usuario);

            }
            CryptoClass cryptoclase = new CryptoClass();
            usuario.password = cryptoclase.Encrypt(usuario.password);
            TempData["SuccessMessage"] = "El registro fue exitoso.";
            repousuario.registromodel(usuario);
            return View("~/Views/Iniciarsesion/registro.cshtml");





        }

        public async Task<IActionResult> inicio(login login)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", login);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel();
                try
                {
                    CryptoClass cryptoclase = new CryptoClass();
                    login.password=cryptoclase.Encrypt(login.password);
                    bool rsp = await repousuario.verificarusuario(login);
                    if (rsp)
                    {
                        return View("~/views/Home/menusuario.cshtml");
                    }
                    else
                    {
                        return View("~/views/home/Index.cshtml");
                    }
                }
                catch (Exception ex)
                {

                    error.RequestId = ex.HResult.ToString();
                    error.message = ex.Message;
                }
                return View("Error", error);

            }
        }

    }
}
