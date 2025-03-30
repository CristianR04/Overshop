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
using static Overshop.Servicios.RepositorioPDF;


namespace Overshop.Controllers
{
    public class PDFController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IRepositorioPDF repositorioPDF;
        public PDFController(IConfiguration configuration, IRepositorioPDF repositorioPDF)
        {
            this.configuration = configuration;
            this.repositorioPDF = repositorioPDF;
        }

        public IActionResult PDF()
        {

            return View(); 

        }

        public IActionResult ListadoPersonasPdf()
        {
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            //  manejador de eventos para el pie de página

            // Título del documento
            document.Add(new Paragraph("Listado de Proveedor")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(8, true); // 4 columnas
            table.AddHeaderCell("ID");
            table.AddHeaderCell("Nombre");
            table.AddHeaderCell("Empresa");
            table.AddHeaderCell("Direccion");
            table.AddHeaderCell("Telefono");
            table.AddHeaderCell("Telefono De La Empresa");
            table.AddHeaderCell("Correo Del Proveedor");
            table.AddHeaderCell("Correo De La Empresa");

            // Llenar la tabla con datos
            proveedormodel proveedormodel = new proveedormodel();
            var PDF = repositorioPDF.HacerPDF(proveedormodel);
            foreach (var persona in PDF)
            {
                table.AddCell(persona.Idproveedor);
                table.AddCell(persona.Nombres);
                table.AddCell(persona.Empresa);
                table.AddCell(persona.Direccion);
                table.AddCell(persona.Telefono);
                table.AddCell(persona.TelefonoEmpresa);
                table.AddCell(persona.Correoprov);
                table.AddCell(persona.CorreoEmpresa);

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
