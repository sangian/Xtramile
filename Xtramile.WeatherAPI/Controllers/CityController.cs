using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xtramile.WeatherAPI.Shared.Dtos;
using Xtramile.WeatherApp.Cities;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly CityService cityService;

        public CityController(CityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<AppResultDto<IList<CityDto>>> GetCitiesByCountry([FromQuery] GetCitiesByCountryApiRequestDto request)
        {
            AppResultDto<IList<CityDto>> result = cityService.GetCitiesByCountry(new GetCitiesByCountryRequest
            {
                Country = request.Country
            });

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return StatusCode(result.Status, result);
        }
    }
}
