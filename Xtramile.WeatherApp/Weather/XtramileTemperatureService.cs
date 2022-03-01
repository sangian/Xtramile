using System;

namespace Xtramile.WeatherApp.Weather
{
    public class XtramileTemperatureService : TemperatureService
    {
        public double CalculateDewPointInCelsius(double tempInCelsius, int humidity)
        {
            // Formula reference:
            // https://iridl.ldeo.columbia.edu/dochelp/QA/Basic/dewpoint.html

            double dewPoint = tempInCelsius - ((100 - humidity) / 5);

            return Math.Round(dewPoint, 2);
        }

        public double ConvertFromCelsiusToFahrenheit(double celsius)
        {
            double fahrenheit = (celsius * 1.8) + 32;
            return Math.Round(fahrenheit, 2);
        }

        public double ConvertFromCelsiusToKelvin(double celsius)
        {
            double kelvin = celsius + 273;
            return Math.Round(kelvin, 2);
        }

        public double ConvertFromFahrenheitToCelsius(double fahrenheit)
        {
            double celsius = 0.555 * (fahrenheit - 32);
            return Math.Round(celsius, 2);
        }

        public double ConvertFromFahrenheitToKelvin(double fahrenheit)
        {
            double kelvin = 0.555 * (fahrenheit - 32) + 273;
            return Math.Round(kelvin, 2);
        }

        public double ConvertFromKelvinToCelsius(double kelvin)
        {
            double celsius = kelvin - 273;
            return Math.Round(celsius, 2);
        }

        public double ConvertFromKelvinToFahrenheit(double kelvin)
        {
            double fahrenheit = 1.8 * (kelvin - 273) + 32;
            return Math.Round(fahrenheit, 2);
        }
    }
}
