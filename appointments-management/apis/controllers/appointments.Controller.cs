
using appointment_details.database.models;
using patient_management.apis.extensions;
using Microsoft.AspNetCore.Mvc;
using appointment_details.apis.contracts;
using appointment_details.database;
using RestSharp;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using RestSharp.Authenticators;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public AppointmentsController(postgresHealthCareDbContext context)
    {
        _context = context;
    }


    [HttpPost("book-appointment")]
    [AllowAnonymous]
    public async Task<IActionResult> BookAppointment([FromBody] AppointmentDetailsContract appointmentDetails)
    {

        try
        {
            var client = new RestClient("http://localhost:5004");
            var request = new RestRequest("PatientDetails/get-patient-details", Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(new { id = appointmentDetails.PatientId });


            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Request failed: {response.StatusCode} - {response.ErrorMessage}");
                return StatusCode((int)response.StatusCode, new { Message = "No patient found for the given id" });
            }
            Console.WriteLine($"Response Content: {response.Content}");

            // 4. Map Appointment Entity and Save
            var appointmentEntityObj = appointmentDetails.ReturnAnEntityObject();

            _context.AppointmentDetails.Add(appointmentEntityObj);
            await _context.SaveChangesAsync();

            return Ok("Appointment Booked Successfully");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return StatusCode(500, new { Message = "An error occurred while booking the appointment." });
        }

    }
}
