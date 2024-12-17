
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


    [HttpPut("update-appointment/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDetailsContract updateAppointmentDetails)
    {

        try
        {
            var existingAppointment = await _context.AppointmentDetails.FindAsync(id);
            if (existingAppointment == null)
            {
                return NotFound($"Appointment with ID {id} not found.");
            }

            var client = new RestClient("http://localhost:5004");
            var request = new RestRequest("PatientDetails/get-patient-details", Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(new { id = updateAppointmentDetails.PatientId });


            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Request failed: {response.StatusCode} - {response.ErrorMessage}");
                return StatusCode((int)response.StatusCode, new { Message = "No patient found for the given id" });
            }
            Console.WriteLine($"Response Content: {response.Content}");

            var properties = updateAppointmentDetails.GetType().GetProperties();

            foreach (var property in properties)
            {
                var updatedValue = property.GetValue(updateAppointmentDetails);

                if (updatedValue != null)
                {
                    // Ensure the type matches and the property is writable
                    var targetProperty = typeof(AppointmentDetails).GetProperty(property.Name);
                    // checking if the property is writable and the type of property in AppointmentDetails model from where existingAppointment data is taken is matching to the one sent in the request payload
                    if (targetProperty != null && targetProperty.CanWrite &&
                        targetProperty.PropertyType.IsAssignableFrom(property.PropertyType))
                    {
                        targetProperty.SetValue(existingAppointment, updatedValue);
                    }
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
            return Ok("Appointment Updated Successfully");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return StatusCode(500, new { Message = "An error occurred while updating the appointment." });
        }

    }

    [HttpDelete("delete-appointment/{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointmentToDelete = await _context.AppointmentDetails.FindAsync(id);
        if (appointmentToDelete == null)
        {
            return NotFound($"Appointment with ID {id} not found.");
        }

        _context.AppointmentDetails.Remove(appointmentToDelete);
        await _context.SaveChangesAsync();

        return Ok($"Appointment with ID {id} deleted successfully.");
    }
}
