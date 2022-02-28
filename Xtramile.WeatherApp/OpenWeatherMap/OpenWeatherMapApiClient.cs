using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.OpenWeatherMap
{
    public interface OpenWeatherMapApiClient
    {
        AppResultDto<CurrentWeatherResponseDto> GetCurrentWeather(CurrentWeatherRequestDto request);
    }
}
