using System;
using System.Collections.Generic;

namespace Xtramile.WeatherApp.Weather
{
    public class WeatherDto
    {
        public DateTimeOffset Timestamp { get; set; }
        public WeatherLocationDto Location { get; set; }
        public WeatherWindDto Wind { get; set; }
        public int Visibility { get; set; }
        public IList<WeatherCondition> Conditions { get; set; }
        public WeatherTemperature Temperature { get; set; }
        public int Humidity { get; set; }
        public WeatherTemperature DewPoint { get; set; }
        public int Pressure { get; set; }
    }

    public class WeatherLocationDto
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class WeatherWindDto
    {
        public decimal Speed { get; set; }
        public decimal Degree { get; set; }
    }

    public class WeatherCondition
    {
        public string Condition { get; set; }
        public string Description { get; set; }
    }

    public class WeatherTemperature
    {
        public decimal Kelvin { get; set; }
        public decimal Fahrenheit { get; set; }
        public decimal Celcius { get; set; }
    }
}
