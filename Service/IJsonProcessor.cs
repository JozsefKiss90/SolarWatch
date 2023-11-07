namespace SolarWatch.Service;

public interface IJsonProcessor
{
    SolarWatch ProcessSolar(String data);
    double[] ProcessCity(String data);
}