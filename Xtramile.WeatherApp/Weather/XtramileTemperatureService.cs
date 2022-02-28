namespace Xtramile.WeatherApp.Weather
{
    public class XtramileTemperatureService : TemperatureService
    {
        public decimal CalculateDewPoint(decimal celsiusAirTemp, int relativeHumidity)
        {
            /*
             * Formula reference: https://iridl.ldeo.columbia.edu/dochelp/QA/Basic/dewpoint.html
             */

            return celsiusAirTemp - ((100 - relativeHumidity) / 5);
        }

        public decimal ConvertFromCelsiusToFahrenheit(decimal celsius)
        {
            return (celsius * (9 / 5)) + 32;
        }

        public decimal ConvertFromCelsiusToKelvin(decimal celsius)
        {
            return celsius + 273.15m;
        }

        public decimal ConvertFromFahrenheitToCelsius(decimal fahrenheit)
        {
            return ((fahrenheit - 32) * 5) / 9;
        }

        public decimal ConvertFromFahrenheitToKelvin(decimal fahrenheit)
        {
            return (5 / 9) * (fahrenheit + 459.67m);
        }

        public decimal ConvertFromKelvinToCelsius(decimal kelvin)
        {
            return kelvin - 273.15m;
        }

        public decimal ConvertFromKelvinToFahrenheit(decimal kelvin)
        {
           return 1.8m * (kelvin - 273) + 32;
        }
    }
}
