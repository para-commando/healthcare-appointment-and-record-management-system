using System.Text;
using clinical_data_grid.apis.services;
using clinical_data_grid.database.models;
using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class PdfController : ControllerBase
{

    [HttpPost("generate")]
    public IActionResult GeneratePrescriptionPdf([FromBody] PrescriptionContent prescriptionContent)
    {
        string htmlContent = HtmlGenerator.GeneratePrescriptionHtml(prescriptionContent);

        Console.WriteLine(prescriptionContent);
        using var stream = new MemoryStream();
        HtmlConverter.ConvertToPdf(htmlContent, stream);
        return File(stream.ToArray(), "application/pdf", "GeneratedDocument.pdf");
    }
}
