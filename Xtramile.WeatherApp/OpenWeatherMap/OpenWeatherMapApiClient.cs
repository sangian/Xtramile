using System.Threading;
using System.Threading.Tasks;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.OpenWeatherMap
{
    public interface OpenWeatherMapApiClient
    {
        Task<AppResultDto<CurrentWeatherResponseDto>> GetCurrentWeather(CurrentWeatherRequestDto request, CancellationToken cancellationToken);
    }
}
