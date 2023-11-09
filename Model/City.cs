namespace SolarWatch.Model;

public class City
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Country { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    public Sunrises Sunrise { get; set; }
    
    public int? SunriseId { get; set; }
}
