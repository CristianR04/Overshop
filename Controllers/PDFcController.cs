using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overshop.Models;
using Overshop.Servicios;

namespace Overshop.Controllers
{
    public class PDFcController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositorioPDF repositorioPdf;


        public PDFcController(IConfiguration configuration, IRepositorioPDF repositorioPdf)
        {
            _configuration = configuration;
            this.repositorioPdf = repositorioPdf;
        }

        public IActionResult PDFc()
        {
            return View();
        }

        public IActionResult ListadoContactosPdf()
        {

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Título del documento
            document.Add(new Paragraph("Listado de Contactanos")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(3, true); // 4 columnas
            table.AddHeaderCell("nombre");
            table.AddHeaderCell("correo");
            table.AddHeaderCell("mensaje");



            // Llenar la tabla con datos
            Contactomodel pdfcontactanos = new Contactomodel();
            var personas = repositorioPdf.contactar(pdfcontactanos);
            foreach (var persona in personas)
            {
                table.AddCell(persona.nombre);
                table.AddCell(persona.correo);
                table.AddCell(persona.mensaje);


            }

            document.Add(table);
            document.Close();

            byte[] pdfBytes = stream.ToArray();
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoPersonas.pdf");
            return File(pdfBytes, "application/pdf");
        }

    }
}
