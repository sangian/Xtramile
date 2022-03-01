using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Xtramile.WeatherAPI.Shared.Dtos;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Weather;

namespace Xtramile.WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService weatherService;

        public WeatherController(WeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<AppResultDto<WeatherDto>>> GetWeatherByCityAsync([FromQuery] GetWeatherByCityApiRequestDto request, CancellationToken cancellationToken)
        {
            AppResultDto<WeatherDto> result = await weatherService.GetWeatherByCity(new GetWeatherByCityRequest
            {
                City = request.City
            }, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return StatusCode(result.Status, result);
        }
    }
}
