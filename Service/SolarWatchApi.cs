using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SolarWatch.Service;

public class SolarWatchApi : ISolarWatchProvider
{
    private readonly ILogger<SolarWatchApi> _logger;
    private readonly HttpClient _client;

    public SolarWatchApi(ILogger<SolarWatchApi> logger, HttpClient client)
    {
        _logger = logger;
        _client = client; 
    }

    public async Task<string> GetSolarPropsAsync(double lat, double lon, DateOnly date)
    {
        var dateString = date.ToString("yyyy-MM-dd");
        var url = $"https://api.sunrise-sunset.org/json?lat={lat}&lon={lon}&date={dateString}";

        try 
        {
            _logger.LogInformation("Calling SolarWatch API with url: {url}", url);
            string jsonResponse = await _client.GetStringAsync(url);  
            return jsonResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}