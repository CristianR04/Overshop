using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overshop.Models;
using Overshop.Servicios;

namespace Overshop.Controllers
{
    public class PDFiController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositorioPDF repositorioPdf;


        public PDFiController(IConfiguration configuration, IRepositorioPDF repositorioPdf)
        {
            _configuration = configuration;
            this.repositorioPdf = repositorioPdf;
        }

        public IActionResult PDFi()
        {

            return View();

        }
        public IActionResult ListadoinventarioPdf()
        {

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Título del documento
            document.Add(new Paragraph("Listado de Inventario")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(7, true); // 4 columnas
            table.AddHeaderCell("Id Producto");
            table.AddHeaderCell("Nombre");
            table.AddHeaderCell("Marca");
            table.AddHeaderCell("Modelo");
            table.AddHeaderCell("Estado");
            table.AddHeaderCell("Accesorios");
            table.AddHeaderCell("Valor");




            // Llenar la tabla con datos
            Inventariomodel inventario = new Inventariomodel();
            var personas = repositorioPdf.Inventario(inventario);
            foreach (var persona in personas)
            {
                table.AddCell(persona.Id);
                table.AddCell(persona.Nombre);
                table.AddCell(persona.marca);
                table.AddCell(persona.modelo);
                table.AddCell(persona.estado.ToString());
                table.AddCell(persona.accesorios.ToString());
                table.AddCell(persona.valor);



            }

            // Agregar la tabla al documento
            document.Add(table);
            document.Close();

            // Retornar el archivo como respuesta
            byte[] pdfBytes = stream.ToArray();
            // Aplicar opción abrir pestaña navegador
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoPersonas.pdf");
            return File(pdfBytes, "application/pdf");
        }
    }
}
