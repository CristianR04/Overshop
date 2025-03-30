using System.ComponentModel.DataAnnotations;

namespace Overshop.Models
{
    public class login
    {
        [Required (ErrorMessage = "El campo {0} es requerido")]
        public string correo { get; set; }

        [Required(ErrorMessage = "El campo contraseña es requerido")]
        public string password { get; set; }
    }
}
