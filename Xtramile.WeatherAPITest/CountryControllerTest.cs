using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xtramile.WeatherAPI.Controllers;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Countries;
using Xunit;

namespace Xtramile.WeatherAPITest
{
    public class CountryControllerTest
    {
        [Fact]
        public void ShouldReturnCountries()
        {
            var countryServiceMock = new Mock<CountryService>();
            countryServiceMock.Setup(m => m.GetCountries())
                .Returns(new AppResultDto<IList<CountryDto>>
                {
                    Succeeded = true,
                    Status = 200,
                    Data = new List<CountryDto>
                    {
                        new CountryDto { Name = "Australia" },
                        new CountryDto { Name = "Indonesia" }
                    }
                });

            var countryController = new CountryController(countryServiceMock.Object);

            var result = countryController.GetCountries();

            var actionResult = Assert.IsType<ActionResult<AppResultDto<IList<CountryDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}
