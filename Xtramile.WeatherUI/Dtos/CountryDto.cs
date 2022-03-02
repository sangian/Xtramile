using System.Text.Json.Serialization;

namespace Xtramile.WeatherUI.Dtos
{
    public class CountryDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
