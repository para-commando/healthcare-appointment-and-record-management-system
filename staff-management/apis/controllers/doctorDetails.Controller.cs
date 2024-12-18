using Microsoft.AspNetCore.Mvc;
using staff_management.database.models;
using Microsoft.EntityFrameworkCore;
using staff_management.database;
using Microsoft.AspNetCore.Authorization;
using staff_management.apis.extensions;
using staff_management.database.extensions;
[ApiController]
[Route("[controller]")]
public class DoctorDetailsController : ControllerBase
{
    private readonly postgresHealthCareDbContext _context;

    public DoctorDetailsController(postgresHealthCareDbContext context)
    {
        _context = context;
    }

    
}
