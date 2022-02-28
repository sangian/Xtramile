using System.Text.Json.Serialization;
using Xtramile.WeatherApp.Common.Mappings;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherApp.Countries
{
    public class CountryDto : IMapFrom<Country>
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
