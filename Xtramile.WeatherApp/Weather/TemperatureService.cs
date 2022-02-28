namespace Xtramile.WeatherApp.Weather
{
    public interface TemperatureService
    {
        decimal ConvertFromKelvinToFahrenheit(decimal kelvin);
        decimal ConvertFromKelvinToCelsius(decimal kelvin);
        decimal ConvertFromFahrenheitToKelvin(decimal fahrenheit);
        decimal ConvertFromFahrenheitToCelsius(decimal fahrenheit);
        decimal ConvertFromCelsiusToFahrenheit(decimal celsius);
        decimal ConvertFromCelsiusToKelvin(decimal celsius);
        decimal CalculateDewPoint(decimal celsiusAirTemp, int relativeHumidity);
    }
}
