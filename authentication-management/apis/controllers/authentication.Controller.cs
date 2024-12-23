using Microsoft.AspNetCore.Mvc;
using authentication_management.database;
using Microsoft.AspNetCore.Authorization;
using authentication_management.apis.extensions;
using authentication_management.database.contracts;
using authentication_management.database.models;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System.Text;
using System.Security.Cryptography;
using authentication_management.apis.services;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public AuthenticationController(postgresHealthCareDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    [AllowAnonymous]

    public async Task<IActionResult> Login([FromBody] LoginUser payload, AuthService service)
    {

        try
        {
            var authenticationExistence = await _context.Authentication.FirstOrDefaultAsync(s => s.UserName == payload.UserName);

            var staffExistence = await _context.Staff.FirstOrDefaultAsync(s => s.StaffUniqueId == payload.StaffUniqueId);

            if (authenticationExistence == null || staffExistence == null)
            {
                return BadRequest(new { message = "Invalid Username or Password" });
            }

            string[] arrValues = authenticationExistence.Password.Split(':');
            string encryptedDbValue = arrValues[0];
            string salt = arrValues[1];
            byte[] saltedValue = Encoding.UTF8.GetBytes(salt + payload.Password);
            using var hashstr = SHA256.Create();
            byte[] hash = hashstr.ComputeHash(saltedValue);
            string enteredValueToValidate = Convert.ToBase64String(hash);
            var result = encryptedDbValue.Equals(enteredValueToValidate);

            if (!result)
            {
                return BadRequest(new { message = "Invalid Username or Password" });
            }
            var roles = authenticationExistence.Roles.Contains(',') ? authenticationExistence.Roles.Split(',') : new string[] { authenticationExistence.Roles };

            var user = new User(
                       staffExistence.StaffUniqueId,
                       authenticationExistence.UserName,
                       staffExistence.Designation,
                      roles);

            return Ok(service.Create(user));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
            throw;
        }

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
    [AllowAnonymous]
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

    [HttpPost("edit-user-profile")]
    [Authorize(Policy = "alpha-doc")]
    public async Task<IActionResult> EditUserProfile([FromBody] EditUser editUser)
    {
        var staffExistence = await _context.Staff.FirstOrDefaultAsync(s => s.StaffUniqueId == editUser.StaffUniqueId);
        var authenticationExistence = await _context.Authentication.FirstOrDefaultAsync(s => s.UserName == editUser.UserName);
        if (staffExistence == null || authenticationExistence == null)
        {
            return BadRequest(new { message = "User not found." });
        }
        var targetUser = "User";
        if (editUser.IsDoctor == true && editUser.DoctorId > 0)
        {

            var doctorApiClient = new RestClient("http://localhost:5008");
            var doctorDetailsApiRequest = new RestRequest($"DoctorDetails/update-doctor-details/{editUser.DoctorId}", Method.Post)
                                   .AddHeader("Content-Type", "application/json")
                                   .AddJsonBody(new
                                   {
                                       DoctorName = editUser?.FullName ?? null,
                                       DoctorSpecialization = editUser?.Specialization ?? null,
                                       DoctorContact = editUser?.Contact ?? null,
                                       DoctorAddress = editUser?.Address ?? null,
                                       DoctorUniqueId = editUser?.NationalUniqueId ?? null,
                                       DoctorDateOfJoining = editUser?.DateOfJoining ?? null
                                   });
            var doctorDetailsApiResponse = await doctorApiClient.ExecuteAsync(doctorDetailsApiRequest);
            if (!doctorDetailsApiResponse.IsSuccessful)
            {
                return StatusCode(500, new { Message = "Doctor data updation failed" });
            }
            targetUser = "Doctor";
        }

        var properties = editUser.GetType().GetProperties();

        foreach (var property in properties)
        {
            var updatedValue = property.GetValue(editUser);

            if (updatedValue != null)
            {
                var staffTargetProperty = typeof(Staff).GetProperty(property.Name);
                var authTargetProperty = typeof(Authentication).GetProperty(property.Name);

                if (staffTargetProperty != null && staffTargetProperty.CanWrite && staffTargetProperty.PropertyType.IsAssignableFrom(property.PropertyType))
                {
                    staffTargetProperty.SetValue(staffExistence, updatedValue);
                }
                else if (authTargetProperty != null && authTargetProperty.CanWrite && authTargetProperty.PropertyType.IsAssignableFrom(property.PropertyType))
                {
                    authTargetProperty.SetValue(authenticationExistence, updatedValue);
                }
            }
        }
        await _context.SaveChangesAsync();
        return Ok($"{targetUser} Profile updated successfully");
    }

}
