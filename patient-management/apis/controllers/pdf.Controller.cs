using System.Text;
using clinical_data_grid.apis.services;
using clinical_data_grid.database.models;
using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PdfController : ControllerBase
{

    [HttpPost("generate-prescription-pdf")]
    public IActionResult GeneratePrescriptionPdf([FromBody] PrescriptionContent prescriptionContent)
    {
        string htmlContent = HtmlGenerator.GeneratePrescriptionHtml(prescriptionContent);
        Console.WriteLine(prescriptionContent);
        // MemoryStream is a class in .NET that provides a stream of data stored in memory (RAM) rather than a file or another medium
        using var stream = new MemoryStream();
        // HtmlConverter.ConvertToPdf() method writes the generated PDF into a stream. Using a MemoryStream avoids the need to create a physical file on disk, improving performance and reducing IO overhead.
        HtmlConverter.ConvertToPdf(htmlContent, stream);
        return File(stream.ToArray(), "application/pdf", "GeneratedDocument.pdf");
    }
}
