using Xtramile.WeatherApp.Common.Mappings;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherApp.Cities
{
    public class CityDto : IMapFrom<City>
    {
        public string Country { get; set; }
        public string Name { get; set; }
    }
}
