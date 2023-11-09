using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarWatch.Model;

namespace SolarWatch.Database;

using Microsoft.EntityFrameworkCore;


public class SolarApiContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Sunrises> Sunrises { get; set; }
    
    public SolarApiContext(DbContextOptions<SolarApiContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            d => d.ToDateTime(new TimeOnly()), // Convert from DateOnly to DateTime
            d => DateOnly.FromDateTime(d)); // Convert from DateTime to DateOnly

        builder.Entity<Sunrises>(entity =>
        {
            entity.Property(e => e.DateOfSunrise)
                .HasConversion(dateOnlyConverter);
        });
        
        builder.Entity<Sunrises>().Ignore(s => s.SunriseTime);
        builder.Entity<City>()
            .HasIndex(u => u.Name)
            .IsUnique();
    
        builder.Entity<City>()
            .HasData(
                new City { Id = 1, Name = "London", Country = "England", Latitude = 51.509865, Longitude = -0.118092 },
                new City { Id = 2, Name = "Budapest", Country = "Hungary", Latitude = 47.497913, Longitude = 19.040236 },
                new City { Id = 3, Name = "Paris", Country = "France", Latitude = 48.864716, Longitude = 2.349014 }
            );
    }
}