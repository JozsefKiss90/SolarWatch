using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SolarWatch.Service; 

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]
public class SolarWatchController : ControllerBase 
{
    private readonly ISolarWatchProvider _solarWatchProvider;
    private readonly IGeoCoderProvider _geoCoderProvider;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly ILogger<SolarWatchController> _logger;

    public SolarWatchController(
        ILogger<SolarWatchController> logger, 
        ISolarWatchProvider SolarWatchProvider, 
        IJsonProcessor jsonProcessor, 
        IGeoCoderProvider geoCoderProvider
        )
    {
        _logger = logger;
        _solarWatchProvider = SolarWatchProvider;
        _jsonProcessor = jsonProcessor;
        _geoCoderProvider = geoCoderProvider;
    }

    [HttpGet("sunrise", Name = "GetSunrise")]
    public async Task<ActionResult<SolarWatch>> GetSunrise([Required] string city, [Required] DateOnly date)
    {   
        try
        {
            string geoCoderApiResponse = await _geoCoderProvider.GetCityPropsAsync(city);
            var geoData = _jsonProcessor.ProcessCity(geoCoderApiResponse);
            string solarApiResponse = await _solarWatchProvider.GetSolarPropsAsync(geoData[0], geoData[1], date);
            var solarData = _jsonProcessor.ProcessSolar(solarApiResponse); 
            return Ok(solarData);  
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting solar data");
            return NotFound("Error getting solar data");
        }
    }
    
    [HttpGet("sunset", Name = "GetSunset")]
    public SolarWatch GetSunset([Required] DateOnly date, [Required] string city)
    {
        return new SolarWatch
        {
            //  Date = DateOnly.FromDateTime(DateTime.Now),
            //City = city
        };
    }
}