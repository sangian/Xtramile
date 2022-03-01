namespace Xtramile.WeatherApp.Weather
{
    public interface TemperatureService
    {
        double ConvertFromKelvinToFahrenheit(double kelvin);
        double ConvertFromKelvinToCelsius(double kelvin);
        double ConvertFromFahrenheitToKelvin(double fahrenheit);
        double ConvertFromFahrenheitToCelsius(double fahrenheit);
        double ConvertFromCelsiusToFahrenheit(double celsius);
        double ConvertFromCelsiusToKelvin(double celsius);
        double CalculateDewPointInCelsius(double tempInCelsius, int humidity);
    }
}
