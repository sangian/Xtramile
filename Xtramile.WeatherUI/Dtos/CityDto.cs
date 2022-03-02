using System.Text.Json.Serialization;

namespace Xtramile.WeatherUI.Dtos
{
    public class CityDto
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
