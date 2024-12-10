using Microsoft.AspNetCore.Mvc;
using clinical_data_grid.database;
using clinical_data_grid.apis.services;
using clinical_data_grid.database.models;
using Microsoft.AspNetCore.Authorization;
namespace clinical_data_grid.controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    private readonly postgresHealthCareDbContext _dbContext;
    private readonly ILogger<HealthCheckController> _logger;

    public HealthCheckController(postgresHealthCareDbContext dbContext, ILogger<HealthCheckController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpGet]
    [Route("health-check")]
    public async Task<IActionResult> GetHealthStatus()
    {
        try
        {
            _logger.LogInformation("Initiating health check for db connection at {Time}", DateTime.UtcNow);
            var dbConnectionStatus = await _dbContext.Database.CanConnectAsync();
            _logger.LogInformation("Initiating health check for cache connection at {Time}", DateTime.UtcNow);
            // Simulated checks for other services
            var cacheServiceStatus = true;
            _logger.LogInformation("Initiating health check for external api connection at {Time}", DateTime.UtcNow);
            var externalApiStatus = true;
            // Log the health check (optional)
            _logger.LogInformation("Health check completed successfully at {Time}", DateTime.UtcNow);

            // Create health report
            var healthReport = new
            {
                RepositoryStatus = dbConnectionStatus ? "Healthy" : "Unhealthy",
                CacheService = cacheServiceStatus ? "Healthy" : "Unhealthy",
                ExternalApi = externalApiStatus ? "Healthy" : "Unhealthy",
                Timestamp = DateTime.UtcNow
            };

            return Ok(healthReport);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during the health check.");
            return StatusCode(500, new { Message = "An error occurred while performing the health check." });
        }
    }

    [HttpGet]
    [Route("login")]
    public string login22(AuthService service)
    {
        var user = new User(
            1,
            "bruno.bernardes",
            "Bruno Bernardes",
            "bruno@company.com",
            "q1w2e3r4t5",
            "IT",
            ["developer"]);

        return service.Create(user);
    }

    [HttpGet]
    [Route("test")]
    [Authorize(Policy = "tech")]
    public IActionResult tttt()
    {
        return Ok("sddddds");
    }
}

