using Microsoft.AspNetCore.Mvc;
using staff_management.database;
using Microsoft.AspNetCore.Authorization;
using staff_management.apis.extensions;
using staff_management.database.contracts;
using staff_management.database.models;
using staff_management.database.extensions;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("[controller]")]
public class DoctorDetailsController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public DoctorDetailsController(postgresHealthCareDbContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    [HttpPost("create-doctor-details")]
    public async Task<IActionResult> CreatePatientDetails([FromBody] CreateDoctorDetailsValidation CreateDoctorDetailsValidation)
    {

        var doctorEntity = CreateDoctorDetailsValidation.ReturnAnEntityObject();

        _context.DoctorDetails.Add(doctorEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreatePatientDetails), new { id = doctorEntity.Id }, doctorEntity);
    }
    [AllowAnonymous]
    [HttpPost("update-doctor-details/{id}")]
    public async Task<IActionResult> UpdatePatientDetails(int id, [FromBody] UpdateDoctorDetailsValidation updateDoctorDetailsValidation)
    {
        var existingPatient = await _context.DoctorDetails.FindAsync(id);
        if (existingPatient == null)
        {
            return NotFound($"Patient with ID {id} not found.");
        }

        var properties = updateDoctorDetailsValidation.GetType().GetProperties();

        foreach (var property in properties)
        {
            var updatedValue = property.GetValue(updateDoctorDetailsValidation);

            if (updatedValue != null)
            {
                // Ensure the type matches and the property is writable
                var targetProperty = typeof(DoctorDetails).GetProperty(property.Name);
                // checking if the property is writable and the type of property in DoctorDetails model from where existingPatient data is taken is matching to the one sent in the request payload
                if (targetProperty != null && targetProperty.CanWrite &&
                    targetProperty.PropertyType.IsAssignableFrom(property.PropertyType))
                {
                    targetProperty.SetValue(existingPatient, updatedValue);
                }
            }
        }

        // Save changes to the database
        await _context.SaveChangesAsync();

        return Ok(existingPatient);
    }

    [HttpDelete("delete-doctor-details/{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> DeleteDoctorDetails(int id)
    {
        var doctorToDelete = await _context.DoctorDetails.FindAsync(id);
        if (doctorToDelete == null)
        {
            return NotFound($"Doctor with ID {id} not found.");
        }

        _context.DoctorDetails.Remove(doctorToDelete);
        await _context.SaveChangesAsync();

        return Ok($"Doctor with ID {id} deleted successfully.");
    }

    [HttpPost("get-doctor-details")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPatientDetailById([FromBody] SearchDoctorDetailsValidation reqBody)
    {
        Console.WriteLine(reqBody);
        List<DoctorDetails>? result = await _context.DoctorDetails.ApplyFilters(reqBody)
                   .ToListAsync();
        if (result?.Count == 0)
        {
            // _logger.Log(LogLevel.Warning, "Data not found");
            return NotFound("Data not found");
        }

        return Ok(result);
    }
}
