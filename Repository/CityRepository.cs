using Microsoft.EntityFrameworkCore;
using SolarWatch.Database;
using SolarWatch.Model;

namespace SolarWatch.Repository;

public class CityRepository : ICityRepository
{
    private readonly SolarApiContext _context;

    public CityRepository(SolarApiContext context)
    {
        _context = context;
    }
    
    public IEnumerable<City> GetAll()
    {
        return _context.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        return _context.Cities
            .Include(c => c.Sunrise) // Eagerly load the Sunrise entity
            .FirstOrDefault(c => c.Name == name);
    }


    public void Add(City city)
    {
        _context.Add(city);
        _context.SaveChanges();
    }

    public void Delete(City city)
    {
        _context.Remove(city);
        _context.SaveChanges();
    }

    public void Update(City city)
    {  
        _context.Update(city);
        _context.SaveChanges();
    }
}