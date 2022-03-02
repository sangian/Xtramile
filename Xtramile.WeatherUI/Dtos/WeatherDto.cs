using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Xtramile.WeatherUI.Dtos
{
    public class WeatherDto
    {
        [JsonPropertyName("location")]
        public WeatherLocationDto Location { get; set; }

        [JsonPropertyName("conditions")]
        public IList<WeatherConditionDto> Conditions { get; set; }

        [JsonPropertyName("temperature")]
        public WeatherTemperature Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("dewPoint")]
        public WeatherTemperature DewPoint { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }

        [JsonPropertyName("wind")]
        public WeatherWindDto Wind { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }
    }

    public class WeatherLocationDto
    {
        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }
    }

    public class WeatherWindDto
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }

        [JsonPropertyName("degree")]
        public double Degree { get; set; }
    }

    public class WeatherConditionDto
    {
        [JsonPropertyName("conndition")]
        public string Condition { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class WeatherTemperature
    {
        [JsonPropertyName("kelvin")]
        public double Kelvin { get; set; }

        [JsonPropertyName("fahrenheit")]
        public double Fahrenheit { get; set; }

        [JsonPropertyName("celsius")]
        public double Celsius { get; set; }
    }
}
