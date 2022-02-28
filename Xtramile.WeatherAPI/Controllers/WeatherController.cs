using Microsoft.AspNetCore.Mvc;
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
    }
}
