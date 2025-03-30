using System.ComponentModel.DataAnnotations;

namespace Overshop.Models
{
    public class registromodel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string identificacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Tiposex sex { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string nacimiento { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string password { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string confircontraseña { get; set; }

    }
}
