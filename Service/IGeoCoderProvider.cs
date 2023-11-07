using System.Diagnostics;

namespace SolarWatch.Service;

public interface IGeoCoderProvider
{
   Task<string> GetCityPropsAsync(string city);
} 