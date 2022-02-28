using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Countries;

namespace Xtramile.WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly CountryService countryService;

        public CountryController(CountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<AppResultDto<IList<CountryDto>>> GetCountries()
        { 
            AppResultDto<IList<CountryDto>> result = countryService.GetCountries();

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return StatusCode(result.Status, result);
        }
    }
}
