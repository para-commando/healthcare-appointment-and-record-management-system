using System.Text;
using Microsoft.AspNetCore.Mvc;
using patient_management.apis.services;
using patient_management.database.models;
using Microsoft.EntityFrameworkCore;
using patient_management.database;
using patient_management.database.contracts;
using Microsoft.AspNetCore.Authorization;
using patient_management.apis.extensions;

[ApiController]
[Route("[controller]")]
public class PatientDetailsController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public PatientDetailsController(postgresHealthCareDbContext context)
    {
        _context = context;
    }

    // Create a new patient detail
    [AllowAnonymous]
    [HttpPost("create-patient-details")]
    public async Task<IActionResult> CreatePatientDetails([FromBody] CreatePatientDetailsValidation patientDetails)
    {

        var patientEntity = patientDetails.ReturnAnEntityObject();

        _context.PatientDetails.Add(patientEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPatientDetailById), new { id = patientEntity.Id }, patientEntity);
    }

    // Get a patient detail by PatientContact
    [HttpPost("get-patient-details")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPatientDetailById([FromBody] SearchPatientDetails reqBody)
    {
        
        var patient = await _context.PatientDetails.FindAsync(PatientContact);
        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }

    // Get all patient details
    [HttpGet("get-all-patient-details")]
     [AllowAnonymous]
    public async Task<IActionResult> GetAllPatientDetails()
    {
        var patients = await _context.PatientDetails.ToListAsync();
        return Ok(patients);
    }


}
