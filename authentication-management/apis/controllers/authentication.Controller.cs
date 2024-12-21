using Microsoft.AspNetCore.Mvc;
using authentication_management.database;
using Microsoft.AspNetCore.Authorization;
using authentication_management.apis.extensions;
using authentication_management.database.contracts;
using authentication_management.database.models;
using authentication_management.database.extensions;
using Microsoft.EntityFrameworkCore;
using RestSharp;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public AuthenticationController(postgresHealthCareDbContext context)
    {
        _context = context;
    }

    [HttpPost("check-field-authenticity")]
    [AllowAnonymous]
    public async Task<IActionResult> IsDataUnique([FromBody] ValidationRequest request)
    {
        if (string.IsNullOrEmpty(request.Field) || string.IsNullOrEmpty(request.Value))
        {
            return BadRequest(new { Message = "Invalid request. Field and Value are required." });
        }

        bool exists = false;

        // Dynamically validate based on the field
        switch (request.Field.ToLower())
        {

            case "userName":
                exists = await _context.Authentication.AnyAsync(s => s.UserName == request.Value);
                break;

            case "nationalUniqueId":
                exists = await _context.Staff.AnyAsync(s => s.UniqueId == request.Value);
                break;

            case "staffUniqueId":
                exists = await _context.Staff.AnyAsync(s => s.StaffUniqueId == request.Value);
                break;

            case "email":
                exists = await _context.Staff.AnyAsync(s => s.Email == request.Value);
                break;

            case "Contact":
                exists = await _context.Staff.AnyAsync(s => s.Contact == request.Value);
                break;

            default:
                return BadRequest(new { Message = "Invalid field name." });
        }

        // Return validation result
        return Ok(new
        {
            Field = request.Field,
            Value = request.Value,
            Exists = exists,
            Message = exists ? $"{request.Field} already exists." : $"{request.Field} is available."
        });
    }
    [HttpPost("signup")]
    public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
    {
        var staffExistence = await _context.Staff.FirstOrDefaultAsync(s => s.StaffUniqueId == registerUser.StaffUniqueId || s.Email == registerUser.Email || s.UniqueId == registerUser.NationalUniqueId || s.Contact == registerUser.Contact);
        var authenticationExistence = await _context.Authentication.FirstOrDefaultAsync(s => s.UserName == registerUser.UserName);
        if (staffExistence != null || authenticationExistence != null)
        {
            return BadRequest(new { message = "data already exists." });
        }

        if (registerUser.IsDoctor == true)
        {
            var doctorApiClient = new RestClient("http://localhost:5008");
            var doctorDetailsApiRequest = new RestRequest("DoctorDetails/create-doctor-details", Method.Post)
                                   .AddHeader("Content-Type", "application/json")
                                   .AddJsonBody(new
                                   {
                                       DoctorName = registerUser.FullName,
                                       DoctorSpecialization = registerUser.Specialization,
                                       DoctorContact = registerUser.Contact,
                                       DoctorAddress = registerUser.Address,
                                       DoctorUniqueId = registerUser.NationalUniqueId,
                                       DoctorDateOfJoining = registerUser.DateOfJoining
                                   });
            var doctorDetailsApiResponse = await doctorApiClient.ExecuteAsync(doctorDetailsApiRequest);
            if (!doctorDetailsApiResponse.IsSuccessful)
            {
                return StatusCode(500, new { Message = "Doctor data creation failed" });
            }

        }

        var staff = registerUser.ReturnAnStaffsObject();
        var authentication = registerUser.ReturnAnAuthenticationObject();
        _context.Staff.Add(staff);
        _context.Authentication.Add(authentication);
        await _context.SaveChangesAsync();
        return Ok(registerUser);
    }

}
