namespace Xtramile.WeatherApp.Weather
{
    public interface TemperatureService
    {
        decimal ConvertFromKelvinToFahrenheit(decimal kelvin);
        decimal ConvertFromKelvinToCelcius(decimal kelvin);
        decimal ConvertFromFahrenheitToCelcius(decimal fahrenheit);
        decimal CalculateDewPoint(decimal celciusAirTemp, int relativeHumidity);
    }
}
