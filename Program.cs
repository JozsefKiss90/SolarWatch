using Microsoft.EntityFrameworkCore;
using SolarWatch.Database;
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
    var db = scope.ServiceProvider.GetRequiredService<SolarApiContext>();
    PrintCities();
    
    void PrintCities()
    {
        foreach (var city in db.Cities)
        {
            Console.WriteLine($"{city.Id}, {city.Name}, {city.Country}, {city.Latitude}, {city.Longitude}");
        }
    }
}

InitializeDb(app);

app.Run();