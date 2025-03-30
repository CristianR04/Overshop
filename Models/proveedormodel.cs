using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Overshop.Models
{
    public class proveedormodel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Idproveedor { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]

        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]

        public string Empresa { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]

        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo fecha de nacimiento es requerido")]

        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]

        public string TelefonoEmpresa { get; set; }

        [Required(ErrorMessage = "El campo confirmar contraseña es requerido")]

        public string Correoprov { get; set; }

        [Required(ErrorMessage = "El campo confirmar contraseña es requerido")]

        public string CorreoEmpresa { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]

        public tipoproveedor TipoProv { get; set; }


    }

    
}
