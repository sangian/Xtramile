using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.OpenWeatherMap;

namespace Xtramile.WeatherInfra.External.OpenWeatherMap
{
    public class OpenWeatherMapApiClientImpl : OpenWeatherMapApiClient
    {
        public AppResultDto<CurrentWeatherResponseDto> GetCurrentWeather(CurrentWeatherRequestDto request)
        {
            throw new System.NotImplementedException();
        }
    }
}
