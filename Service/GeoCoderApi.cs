namespace SolarWatch.Service;

public class GeoCoderApi : IGeoCoderProvider
{
    private readonly ILogger<GeoCoderApi> _logger;
    private readonly HttpClient _client;

    public GeoCoderApi(ILogger<GeoCoderApi> logger, HttpClient client)
    {
        _logger = logger;
        _client = client; 
    }

    public async Task<string> GetCityPropsAsync(string city)
    {
        string apiKey = "e753c2fab4a9ec904941b28a420d1bd6";
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}";
        try
        {
            _logger.LogInformation("Calling OpenWeather API with url: {url}", url);
            string jsonResponse = await _client.GetStringAsync(url); 
            _logger.LogInformation("JSON RESPONSE: " + jsonResponse);
            return jsonResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}