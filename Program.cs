using Microsoft.EntityFrameworkCore;
using SolarWatch.Database;
using SolarWatch.Model;
using SolarWatch.Repository;
using SolarWatch.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ISolarWatchProvider, SolarWatchApi>();
builder.Services.AddHttpClient<IGeoCoderProvider, GeoCoderApi>();
builder.Services.AddSingleton<IJsonProcessor, JsonProcessor>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddDbContext<SolarApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SolarApi")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

void InitializeDb(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<SolarApiContext>();
    
    // Ensure the database is created
    dbContext.Database.Migrate();

    PrintCities();
    
    void PrintCities()
    {
        if (!dbContext.Cities.Any())
        {
            // Seed the Cities table
            dbContext.Cities.AddRange(new City[]
            {
                new City { Name = "London", Country = "England", Latitude = 51.509865, Longitude = -0.118092 },
                new City { Name = "Budapest", Country = "Hungary", Latitude = 47.497913, Longitude = 19.040236 },
                new City { Name = "Paris", Country = "France", Latitude = 48.864716, Longitude = 2.349014 }
            });
            dbContext.SaveChanges();
        }

        foreach (var city in dbContext.Cities)
        {
            Console.WriteLine($"{city.Id}, {city.Name}, {city.Country}, {city.Latitude}, {city.Longitude}");
        }
    }
}

InitializeDb(app);

app.Run();