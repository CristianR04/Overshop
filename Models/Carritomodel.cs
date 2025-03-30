using Microsoft.AspNetCore.Http.Features;

namespace Overshop.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public decimal Valor { get; set; }
        public string descripcion { get; set; }
        public string urlimagen { get; set; }
    }

    public class carroitem
    {
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        
    }

    public class productoSeleccionado
    {
        public List<carroitem> Items { get; set; } = new List<carroitem>();
        public decimal TotalPrecio => Items.Sum(item => item.producto.Valor * item.cantidad);
    }
}
