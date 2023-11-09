using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;
using SolarWatch.Database;
using SolarWatch.Model;
using SolarWatch.Repository;
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
    private readonly ICityRepository _cityRepository;
    private readonly SolarApiContext _solarApiContext;
    public SolarWatchController(
        ILogger<SolarWatchController> logger, 
        ISolarWatchProvider SolarWatchProvider, 
        IJsonProcessor jsonProcessor, 
        IGeoCoderProvider geoCoderProvider,
        ICityRepository cityRepository,
        SolarApiContext solarApiContext
        )
    {
        _logger = logger;
        _solarWatchProvider = SolarWatchProvider;
        _jsonProcessor = jsonProcessor;
        _geoCoderProvider = geoCoderProvider;
        _cityRepository = cityRepository;
        _solarApiContext = solarApiContext;
    }

    [HttpGet("sunrise", Name = "GetSunrise")]
    public async Task<ActionResult<Sunrises>> GetSunrise([Required] string cityName, [Required] DateOnly date)
    {   
        var city = _cityRepository.GetByName(cityName);
        if (city == null)
        {
            return NotFound($"City {cityName} not found");
        }
        try
        {
            if (city.SunriseId == null)
            {
                string geoCoderApiResponse = await _geoCoderProvider.GetCityPropsAsync(cityName);
                var geoData = _jsonProcessor.ProcessCity(geoCoderApiResponse);
                string solarApiResponse = await _solarWatchProvider.GetSolarPropsAsync(geoData[0], geoData[1], date);
                var solarData = _jsonProcessor.ProcessSolar(solarApiResponse);
                string timeString = solarData.Sunrise;
                TimeOnly sunriseTime = TimeOnly.ParseExact(timeString, "h:mm:ss tt", CultureInfo.InvariantCulture);
                Sunrises sunrise = new Sunrises();
                sunrise.SunriseTime = sunriseTime;
                _solarApiContext.Sunrises.Add(sunrise);
                _solarApiContext.SaveChanges();
                city.SunriseId = sunrise.Id;
                _solarApiContext.SaveChanges();
                //update and return city sunrise
            }
      
            return Ok(city.Sunrise);  
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