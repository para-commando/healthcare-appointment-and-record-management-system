using System.Text;
using Microsoft.AspNetCore.Mvc;
using patient_management.apis.services;
using patient_management.database.models;
using Microsoft.EntityFrameworkCore;
using patient_management.database;
using patient_management.database.contracts;
using Microsoft.AspNetCore.Authorization;
using patient_management.apis.extensions;
using patient_management.database.extensions;
[ApiController]
[Route("[controller]")]
[Authorize(Policy = "alpha-doc")]
public class PatientDetailsController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public PatientDetailsController(postgresHealthCareDbContext context)
    {
        _context = context;
    }

    // Create a new patient detail

    [HttpPost("create-patient-details")]
    public async Task<IActionResult> CreatePatientDetails([FromBody] CreatePatientDetailsValidation patientDetails)
    {

        var patientEntity = patientDetails.ReturnAnEntityObject();

        _context.PatientDetails.Add(patientEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPatientDetailById), new { id = patientEntity.Id }, patientEntity);
    }

    [HttpPost("get-patient-details")]

    public async Task<IActionResult> GetPatientDetailById([FromBody] SearchPatientDetails reqBody)
    {
        Console.WriteLine(reqBody);
        List<PatientDetails>? result = await _context.PatientDetails.ApplyFilters(reqBody)
                   .ToListAsync();
        if (result?.Count == 0)
        {
            // _logger.Log(LogLevel.Warning, "Data not found");
            return NotFound("Data not found");
        }

        return Ok(result);
    }


    [HttpGet("get-all-patient-details")]

    public async Task<IActionResult> GetAllPatientDetails()
    {
        var patients = await _context.PatientDetails.ToListAsync();
        return Ok(patients);
    }

    [HttpPut("update-patient-details/{id}")]


    public async Task<IActionResult> UpdatePatientDetails(int id, [FromBody] UpdatePatientDetails updatedDetails)
    {
        var existingPatient = await _context.PatientDetails.FindAsync(id);
        if (existingPatient == null)
        {
            return NotFound($"Patient with ID {id} not found.");
        }

        var properties = updatedDetails.GetType().GetProperties();

        foreach (var property in properties)
        {
            var updatedValue = property.GetValue(updatedDetails);

            if (updatedValue != null)
            {
                // Ensure the type matches and the property is writable
                var targetProperty = typeof(PatientDetails).GetProperty(property.Name);
                // checking if the property is writable and the type of property in PatientDetails model from where existingPatient data is taken is matching to the one sent in the request payload
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


    // Delete Patient Details by ID
    [HttpDelete("delete-patient-details/{id}")]


    public async Task<IActionResult> DeletePatientDetails(int id)
    {
        var patientToDelete = await _context.PatientDetails.FindAsync(id);
        if (patientToDelete == null)
        {
            return NotFound($"Patient with ID {id} not found.");
        }

        _context.PatientDetails.Remove(patientToDelete);
        await _context.SaveChangesAsync();

        return Ok($"Patient with ID {id} deleted successfully.");
    }
}
