using System.Text.Json;

namespace SolarWatch.Service;

public class JsonProcessor : IJsonProcessor
{
    public SolarWatch ProcessSolar(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        
        string sunriseTime = results.GetProperty("sunrise").GetString(); 
        
        SolarWatch solarProps = new SolarWatch
        {
            Sunrise = sunriseTime 
        };

        return solarProps;
    }

    public double[] ProcessCity(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement rootElement = json.RootElement;
    
        if (rootElement.ValueKind == JsonValueKind.Array && rootElement.GetArrayLength() > 0)
        {
            JsonElement firstElement = rootElement[0];

            double cityLat = firstElement.GetProperty("lat").GetDouble();
            double cityLon = firstElement.GetProperty("lon").GetDouble();

            double[] coordinates = { cityLat, cityLon };
            return coordinates;
        }
        else
        {
            throw new FormatException("The provided strings are not in a correct double format.");
        }
    }

    private static DateTime GetDateTimeFromUnixTimeStamp(long timeStamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timeStamp);
        DateTime dateTime = dateTimeOffset.UtcDateTime;

        return dateTime;
    }
}