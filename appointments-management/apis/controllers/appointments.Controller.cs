
using appointment_details.database.models;
using appointments_management.apis.extensions;
using Microsoft.AspNetCore.Mvc;
using appointment_details.apis.contracts;
using appointment_details.database;
using RestSharp;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using RestSharp.Authenticators;
using appointments_management.database.contracts;
using appointment_details.database.extensions;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("[controller]")]
[Authorize(Policy = "alpha-doc")]
public class AppointmentsController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public AppointmentsController(postgresHealthCareDbContext context)
    {
        _context = context;
    }

    [HttpPost("find-appointments")]

    public async Task<IActionResult> FindAppointments([FromBody] SearchAppointments searchAppointments)
    {
        try
        {
            Console.WriteLine(searchAppointments);
            List<AppointmentDetails>? result = await _context.AppointmentDetails.ApplyFilters(searchAppointments)
                       .ToListAsync();
            if (result?.Count == 0)
            {
                // _logger.Log(LogLevel.Warning, "Data not found");
                return NotFound("Data not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return StatusCode(500, new { Message = "An error occurred while Finding the appointments." });
        }

    }

    [HttpPost("book-appointment")]

    public async Task<IActionResult> BookAppointment([FromBody] AppointmentDetailsContract appointmentDetails)
    {

        try
        {

            var patientApiClient = new RestClient("http://localhost:5004");
            var doctorApiClient = new RestClient("http://localhost:5008");

            var patientDetailsApiRequest = new RestRequest("PatientDetails/get-patient-details", Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(new { id = appointmentDetails.PatientId });

            var doctorDetailsApiRequest = new RestRequest("DoctorDetails/get-doctor-details", Method.Post)
                          .AddHeader("Content-Type", "application/json")
                          .AddJsonBody(new { id = appointmentDetails.DoctorId });

            var patientDetailsApiResponse = await patientApiClient.ExecuteAsync(patientDetailsApiRequest);

            var doctorDetailsApiResponse = await doctorApiClient.ExecuteAsync(doctorDetailsApiRequest);


            if (!patientDetailsApiResponse.IsSuccessful || !doctorDetailsApiResponse.IsSuccessful)
            {
                return StatusCode(500, new { Message = "No patient/doctor found for the given id, please check doctor/patient id" });
            }

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

    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDetailsContract updateAppointmentDetails)
    {

        try
        {
            var existingAppointment = await _context.AppointmentDetails.FindAsync(id);
            if (existingAppointment == null)
            {
                return NotFound($"Appointment with ID {id} not found.");
            }

            var patientApiClient = new RestClient("http://localhost:5004");
            var doctorApiClient = new RestClient("http://localhost:5008");

            var patientDetailsApiRequest = new RestRequest("PatientDetails/get-patient-details", Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(new { id = updateAppointmentDetails.PatientId });

            var doctorDetailsApiRequest = new RestRequest("DoctorDetails/get-doctor-details", Method.Post)
                          .AddHeader("Content-Type", "application/json")
                          .AddJsonBody(new { id = updateAppointmentDetails.DoctorId });

            var patientDetailsApiResponse = await patientApiClient.ExecuteAsync(patientDetailsApiRequest);

            var doctorDetailsApiResponse = await doctorApiClient.ExecuteAsync(doctorDetailsApiRequest);


            if (!patientDetailsApiResponse.IsSuccessful || !doctorDetailsApiResponse.IsSuccessful)
            {
                return StatusCode(500, new { Message = "No patient/doctor found for the given id, please check doctor/patient id" });
            }

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
