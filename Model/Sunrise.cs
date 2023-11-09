using System.ComponentModel.DataAnnotations.Schema;

namespace SolarWatch.Model;

using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Sunrises
{
    private TimeOnly _sunriseTime;

    public int Id { get; init; }

    // This is the property you map to the database as a TimeSpan for storage
    public TimeSpan SunriseTimeDb
    {
        get => _sunriseTime.ToTimeSpan();
        set => _sunriseTime = TimeOnly.FromTimeSpan(value);
    }

    // This is not mapped to the database, but used in your application logic
    [NotMapped]
    public TimeOnly SunriseTime
    {
        get => _sunriseTime;
        set => _sunriseTime = value;
    }
}

