namespace Overshop.Models
{
    
    public class ingresarcompra
    {
        public DateTimeOffset fecha { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Idproveedor { get;set; }
        public string Empresa { get; set; }
        public decimal valor { get; set; }
        public decimal Valortotal { get; set; }
    }
}
