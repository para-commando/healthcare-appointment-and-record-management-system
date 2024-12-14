using System.Text;
using clinical_data_grid.database.models;
namespace clinical_data_grid.apis.services;

public class HtmlGenerator
{
  public static string GeneratePrescriptionHtml(PrescriptionContent prescription)
  {
    var sb = new StringBuilder();

    // Start the HTML document
    sb.AppendLine("<!DOCTYPE html>");
    sb.AppendLine("<html lang='en'>");
    sb.AppendLine("<head>");
    sb.AppendLine("<meta charset='UTF-8'>");
    sb.AppendLine("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
    sb.AppendLine("<title>Prescription</title>");
    sb.AppendLine("<style>");
    sb.AppendLine(@"body { font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #e6f5f7; }
                    .prescription-container { width: 500px; margin: 20px auto; padding: 20px; background: linear-gradient(to bottom, #66c5f9, #e6f5f7); border-radius: 10px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); color: #000; position: relative; }
                    .header { text-align: center; padding: 10px; display: flex; justify-content: space-between; align-items: center; }
                    .logo { border-radius: 12px; width: 60px; height: 60px; margin-right: 7px;  }
                    .header h1 { margin: 0; font-size: 24px; }
                    .header p { margin: 5px 0; font-size: 14px; }
                    .content { font-size: 12px; margin: 20px 0; color: #000; }
                    .footer { text-align: center; font-size: 12px; color: #fff; background-color: #0288d1; padding: 1px; border-radius: 10px; }
                    .rx { font-size: 20px; font-weight: bold; color: #f6fcff; text-align: center; }
                    .line { margin: 10px 0; border-top: 3px solid #fff0f0; }
                    .hospital-details { display:flex; justify-content: center; align-items: center; padding-right:190px }
                    .doctor-details { margin-left: 12px; }");
    sb.AppendLine("</style>");
    sb.AppendLine("</head>");
    sb.AppendLine("<body>");
    sb.AppendLine("<div class='prescription-container'>");

    // Header Section
    sb.AppendLine("<div class='header'>");
    sb.AppendLine("<div class='hospital-details'>");
    sb.AppendLine("<img src='https://img.freepik.com/free-vector/hospital-logo-design-vector-medical-cross_53876-136743.jpg?t=st=1734101789~exp=1734105389~hmac=78c2ad45d8a7b0f1e20fb7960f002735943a92d712b984ea877d996e4a7773a0&w=740' alt='Logo' class='logo'>");
    sb.AppendLine("<p class='hospital-name'>New hospital,<br>we care for you</p>");

    sb.AppendLine("</div>");

    sb.AppendLine("<div class='doctor-details'>");
    sb.AppendLine($"<h1>Dr. {prescription.DoctorName}</h1>");
    sb.AppendLine($"<p>Specialization: {prescription.DoctorSpecialization}</p>");
    sb.AppendLine("</div>");
    sb.AppendLine("</div>");
    sb.AppendLine("<div class='line'></div>");

    // Content Section
    sb.AppendLine("<div class='content'>");
    sb.AppendLine($"<p>S. No: ___________ &nbsp;&nbsp;&nbsp;&nbsp; Date: {prescription.Date} &nbsp;&nbsp;&nbsp;&nbsp; Patient's Name: {prescription.PatientName}</p>");
    sb.AppendLine($"<p>Contact No.: {prescription.ContactNumber} &nbsp;&nbsp;&nbsp;&nbsp; Age: {prescription.Age} &nbsp;&nbsp; Gender: {prescription.Gender} </p>");
    sb.AppendLine($"<p> Diagnosis: {prescription.Diagnosis}</p>");
    sb.AppendLine("<div class='rx'>Rx:</div>");

    // Medicine Section
    sb.AppendLine("<div class='line'></div>");
    if (prescription.Medicines != null)
    {
      foreach (Medicines medicine in prescription.Medicines)
      {
        // code block to be executed
        sb.AppendLine($"<p>Medicine: {medicine.MedicineName} &nbsp;&nbsp; Duration: {medicine.Duration.Numb} {medicine.Duration.DurationFreq} &nbsp;&nbsp; Quantity: {medicine.Quantity} </p>");
        sb.AppendLine($"<p> To cure: {medicine.ToCure} </p>");
        sb.AppendLine($"<p>Frequency: {medicine.Frequency} &nbsp;&nbsp; Time of day: ");
        sb.AppendLine($"<input type='checkbox' {(medicine.TimeOfDay.Contains("Morning") ? "checked" : "")}> Morning");
        sb.AppendLine($"<input type='checkbox' {(medicine.TimeOfDay.Contains("Noon") ? "checked" : "")}> Noon");
        sb.AppendLine($"<input type='checkbox' {(medicine.TimeOfDay.Contains("Evening") ? "checked" : "")}> Evening");
        sb.AppendLine($"<input type='checkbox' {(medicine.TimeOfDay.Contains("Night") ? "checked" : "")}> Night</p>");

        sb.AppendLine($"<p><input type='checkbox' {(medicine.FoodTimings == "Before Food" ? "checked" : "")}> Before Food");
        sb.AppendLine($"<input type='checkbox' {(medicine.FoodTimings == "After Food" ? "checked" : "")}> After Food</p>");
        sb.AppendLine("<div class='line'></div>");
      }
    }


    sb.AppendLine("<p>Doctor's Signature: __________________________________</p>");
    sb.AppendLine("</div>");

    // Footer Section
    sb.AppendLine("<div class='footer'>");
    sb.AppendLine("<p>Medical Care Clinic Name</p>");
    sb.AppendLine("<p>123, Lorem Ipsum St. | +00 123 456 789</p>");
    sb.AppendLine("</div>");

    sb.AppendLine("</div>");
    sb.AppendLine("</body>");
    sb.AppendLine("</html>");

    return sb.ToString();
  }
}