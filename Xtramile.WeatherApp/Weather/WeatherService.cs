using System.Threading;
using System.Threading.Tasks;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.Weather
{
    public interface WeatherService
    {
        Task<AppResultDto<WeatherDto>> GetWeatherByCity(GetWeatherByCityRequest request, CancellationToken cancellationToken);
    }
}
