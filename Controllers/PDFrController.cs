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
    public class PDFrController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositorioPDF repositorioPdf;


        public PDFrController(IConfiguration configuration, IRepositorioPDF repositorioPdf)
        {
            _configuration = configuration;
            this.repositorioPdf = repositorioPdf;
        }
        public IActionResult PDFr()
        {
            return View();
        }
        public IActionResult ListadoregistroPdf()
        {

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Título del documento
            document.Add(new Paragraph("Listado de Registro")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(6, true); // 4 columnas
            table.AddHeaderCell("Identificacion");
            table.AddHeaderCell("Nombre");
            table.AddHeaderCell("Apellido");
            table.AddHeaderCell("Fecha de Nacimiento");
            table.AddHeaderCell("Sexo");
            table.AddHeaderCell("Correo");
            



            // Llenar la tabla con datos
            registromodel registrar = new registromodel();
            var personas = repositorioPdf.registro(registrar);
            foreach (var persona in personas)
            {
                table.AddCell(persona.identificacion);
                table.AddCell(persona.nombre);
                table.AddCell(persona.apellido);
                table.AddCell(persona.nacimiento);
                table.AddCell(persona.sex.ToString());
                table.AddCell(persona.correo);




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
