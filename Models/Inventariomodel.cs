using System.ComponentModel.DataAnnotations;

namespace Overshop.Models
{
    public class Inventariomodel 
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        public string valor { get; set; }
        public Estadoproduc estado { get; set; }
        [Required(ErrorMessage = "Por favor, seleccione una imagen")]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }
        public string urlimagen { get; set; }
        public string modelo { get; set; }
        public Accesorios accesorios { get; set; }
    }
           
}
