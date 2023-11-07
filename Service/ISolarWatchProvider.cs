namespace SolarWatch.Service;

public interface ISolarWatchProvider
{
    Task<string> GetSolarPropsAsync(double lat, double lon, DateOnly date);
}