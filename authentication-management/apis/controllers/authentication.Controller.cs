using Microsoft.AspNetCore.Mvc;
using authentication_management.database;
using Microsoft.AspNetCore.Authorization;
using authentication_management.apis.extensions;
using authentication_management.database.contracts;
using authentication_management.database.models;
using authentication_management.database.extensions;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public AuthenticationController(postgresHealthCareDbContext context)
    {
        _context = context;
    }
    [HttpPost("signup")]
    public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
    {


        return Ok(registerUser);
    }

}
