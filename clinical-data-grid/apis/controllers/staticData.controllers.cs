using Microsoft.AspNetCore.Mvc;
using clinical_data_grid.database;
using Microsoft.EntityFrameworkCore;
using clinical_data_grid.database.models;
using clinical_data_grid.apis.services;
using clinical_data_grid.database.extensions;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<IActionResult> SearchHealthStaticData([FromBody] SearchHealthStaticData reqBody)
    {
        try
        {
            _logger.SetCustomMessage("SearchHealthStaticData");

            _logger.Log(LogLevel.Information, "Executing query to fetch specific health static data based on filters");

            List<ClinicalHealthStaticData>? result = await _dbContext.ClinicalHealthStaticData.ApplyFilters(reqBody)
            .ToListAsync();
            if (result?.Count == 0)
            {
                _logger.Log(LogLevel.Warning, "Data not found");

                return NotFound("Data not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, "An error occurred in SearchHealthStaticData", ex);

            return StatusCode(500, new { Message = "An error occurred in SearchHealthStaticData" });
        }
    }

    [HttpPut]
    [Route("update-health-static-data")]
    public async Task<IActionResult> UpdateHealthStaticData([FromQuery] int id, [FromBody] UpdateHealthStaticData updatePayload)
    {
        try
        {

            _logger.SetCustomMessage("UpdateHealthStaticData");

            _logger.Log(LogLevel.Information, "Executing query to fetch health static data matching given id");

            ClinicalHealthStaticData? result = await _dbContext.ClinicalHealthStaticData
      .Where(b => b.Id == id)
      .FirstOrDefaultAsync();

            Console.WriteLine(result);
            if (result == null)
            {
                _logger.Log(LogLevel.Warning, "No data available to update");
                return NotFound("No data available to update");  // Return No Content if no data found in the database. 204 No Content.
            }
            int noOfColsUpdated = await _dbContext.ClinicalHealthStaticData.UpdateHealthStatic(id, updatePayload);

            _logger.Log(LogLevel.Information, $"noOfColsUpdated {noOfColsUpdated}");

            return Ok("updation successful");
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, "An error occurred in UpdateHealthStaticData", ex);

            return StatusCode(500, new { Message = "An error occurred in UpdateHealthStaticData" });
        }
    }

    [HttpDelete]
    [Route("delete-health-static-data")]
    public async Task<IActionResult> DeleteHealthStaticData([FromQuery] int id)
    {
        try
        {
            _logger.SetCustomMessage("DeleteHealthStaticData");

            _logger.Log(LogLevel.Information, "Executing query to fetch health static data matching given id");

            ClinicalHealthStaticData? result = await _dbContext.ClinicalHealthStaticData
      .Where(b => b.Id == id)
      .FirstOrDefaultAsync();

            Console.WriteLine(result);
            if (result == null)
            {
                _logger.Log(LogLevel.Warning, "No data found for deletion");
                return NotFound("No data found for deletion");  // Return No Content if no data found in the database. 204 No Content.
            }
            int deletionResult = await _dbContext.ClinicalHealthStaticData.DeleteHealthStatic(id);

            _logger.Log(LogLevel.Information, $"deletionResult {deletionResult}");

            return Ok("deletion successful");
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, "An error occurred in DeleteHealthStaticData", ex);

            return StatusCode(500, new { Message = "An error occurred in DeleteHealthStaticData" });
        }
    }

}
