using Xtramile.WeatherApp.Common.Mappings;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherApp.Countries
{
    public class CountryDto : IMapFrom<Country>
    {
        public string Name { get; set; }
    }
}
