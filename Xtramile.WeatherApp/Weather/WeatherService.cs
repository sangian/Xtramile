using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.Weather
{
    public interface WeatherService
    {
        AppResultDto<WeatherDto> GetWeatherByCity(GetWeatherByCityRequest request);
    }
}
