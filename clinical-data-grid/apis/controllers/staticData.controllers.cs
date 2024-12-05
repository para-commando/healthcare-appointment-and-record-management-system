using Microsoft.AspNetCore.Mvc;
using clinical_data_grid.database;
using Microsoft.EntityFrameworkCore;
using clinical_data_grid.database.models;
using clinical_data_grid.apis.services;
namespace clinical_data_grid.controllers;
[ApiController]
[Route("api/[controller]")]
public class StaticDataController : ControllerBase
{
    private readonly postgresHealthCareDbContext _dbContext;
    private readonly CustomLogger<HealthCheckController> _logger;

    public StaticDataController(postgresHealthCareDbContext dbContext, CustomLogger<HealthCheckController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;

    }

    [HttpGet]
    [Route("get-all-health-static-data")]
    public async Task<IActionResult> GetAllHealthStaticData([FromQuery] int lastId = 0, [FromQuery] int pageSize = 10)
    {
        try
        {
            _logger.SetCustomMessage("GetAllHealthStaticData");

            _logger.Log(LogLevel.Information, "Executing query to fetch all health static data");

            List<ClinicalHealthStaticData>? result = await _dbContext.ClinicalHealthStaticData.Where(b => b.Id > lastId).Take(pageSize).ToListAsync();

            if (result == null || result.Count == 0)
            {
                _logger.Log(LogLevel.Warning, "No data available");
                return NoContent();  // Return No Content if no data found in the database. 204 No Content.
            }
            _logger.Log(LogLevel.Information, "returning available data..");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, "An error occurred in GetAllHealthStaticData", ex);

            return StatusCode(500, new { Message = "An error occurred in GetAllHealthStaticData" });
        }
    }

    [HttpPost]
    [Route("search-health-static-data")]
    public  IActionResult SearchHealthStaticData([FromBody] SearchHealthStaticData reqBody)
    {
        try
        {
            _logger.SetCustomMessage("SearchHealthStaticData");

            _logger.Log(LogLevel.Information, "Executing query to fetch all health static data");

            // List<ClinicalHealthStaticData>? result = await _dbContext.ClinicalHealthStaticData.Where(b => b.Id > lastId).Take(pageSize).ToListAsync();

            // if (result == null || result.Count == 0)
            // {
            //     _logger.Log(LogLevel.Warning, "No data available");
            //     return NoContent();  // Return No Content if no data found in the database. 204 No Content.
            // }
            // _logger.Log(LogLevel.Information, "returning available data..");

            return Ok(reqBody);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, "An error occurred in GetAllHealthStaticData", ex);

            return StatusCode(500, new { Message = "An error occurred in GetAllHealthStaticData" });
        }
    }
}
