using Microsoft.AspNetCore.Mvc;
using clinical_data_grid.database;
using Microsoft.EntityFrameworkCore;
using clinical_data_grid.database.models;
using clinical_data_grid.apis.services;
using StackExchange.Redis;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
namespace clinical_data_grid.controllers;
[ApiController]
[Route("api/[controller]")]
public class StaticDataController : ControllerBase
{
    private readonly postgresHealthCareDbContext _dbContext;
    private readonly CustomLogger<StaticDataController> _logger;

    private readonly IDatabase _redis;

    public StaticDataController(postgresHealthCareDbContext dbContext, CustomLogger<StaticDataController> logger, IConnectionMultiplexer muxer)
    {
        _dbContext = dbContext;
        _logger = logger;
        _redis = muxer.GetDatabase();

    }

    [HttpGet]
    [AllowAnonymous]
    [Route("get-all-health-static-data")]
    public async Task<IActionResult> GetAllHealthStaticData([FromQuery] int lastId = 0, [FromQuery] int pageSize = 10)
    {
        try
        {
            _logger.SetCustomMessage("GetAllHealthStaticData");

            string? redisData = "{}";
            var keyName = $"GetAllHealthStaticData:{lastId},{pageSize}";
            _logger.Log(LogLevel.Information, "Searching for cached data");

            redisData = await _redis.StringGetAsync(keyName);
            if (string.IsNullOrEmpty(redisData))
            {
                _logger.Log(LogLevel.Information, "Cached data found, returning");

                _logger.Log(LogLevel.Information, "Cached data not found, Executing query to fetch health static paginated data");

                List<ClinicalHealthStaticData>? result = await _dbContext.ClinicalHealthStaticData.Where(b => b.Id > lastId).Take(pageSize).ToListAsync();
                if (result == null || result.Count == 0)
                {
                    _logger.Log(LogLevel.Warning, "No data available");
                    return NoContent();  // Return No Content if no data found in the database. 204 No Content.
                }
                _logger.Log(LogLevel.Information, "data obtained, saving to redis cache...");
                var serializedResult = JsonSerializer.Serialize(result);
                var setTask = _redis.StringSetAsync(keyName, serializedResult);

                var expireTask = _redis.KeyExpireAsync(keyName, TimeSpan.FromSeconds(3600));
                await Task.WhenAll(setTask, expireTask);
                _logger.Log(LogLevel.Information, "data saved to redis, returning...");
                return Ok(result);

            }
            _logger.Log(LogLevel.Information, "Cached data found, returning");

            var cachedResult = JsonSerializer.Deserialize<List<ClinicalHealthStaticData>>(redisData);
            return Ok(cachedResult);

        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, "An error occurred in GetAllHealthStaticData", ex);

            return StatusCode(500, new { Message = "An error occurred in GetAllHealthStaticData" });
        }
    }



}
