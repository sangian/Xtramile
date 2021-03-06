using System.Text.Json.Serialization;
using Xtramile.WeatherApp.Common.Mappings;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherApp.Cities
{
    public class CityDto : IMapFrom<City>
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
