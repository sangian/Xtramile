using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Xtramile.WeatherApp.OpenWeatherMap
{
    public class CurrentWeatherResponseDto
    {
        [JsonPropertyName("coord")]
        public CurrentWeatherLocationDto Location { get; set; }

        [JsonPropertyName("weather")]
        public IEnumerable<CurrentWeatherCondition> Conditions { get; set; }

        [JsonPropertyName("main")]
        public CurrentWeatherMain Main { get; set; }

        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }

        [JsonPropertyName("wind")]
        public CurrentWeatherWindDto Wind { get; set; }

        [JsonPropertyName("dt")]
        public long Timestamp { get; set; }

        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }
    }

    public class CurrentWeatherLocationDto
    {
        [JsonPropertyName("lat")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("lon")]
        public decimal Longitude { get; set; }
    }

    public class CurrentWeatherWindDto
    {
        [JsonPropertyName("speed")]
        public decimal Speed { get; set; }

        [JsonPropertyName("deg")]
        public decimal Degree { get; set; }
    }

    public class CurrentWeatherCondition
    {
        [JsonPropertyName("main")]
        public string Condition { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class CurrentWeatherMain
    {
        [JsonPropertyName("temp")]
        public decimal Temperature { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }
}
