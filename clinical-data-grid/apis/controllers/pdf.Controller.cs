using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class PdfController : ControllerBase
{
    [HttpPost("generate")]
    public IActionResult GeneratePdf([FromBody] string htmlContent)
    {
        using var stream = new MemoryStream();
        HtmlConverter.ConvertToPdf(htmlContent, stream);
        return File(stream.ToArray(), "application/pdf", "GeneratedDocument.pdf");
    }
}
