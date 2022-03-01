using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xtramile.WeatherAPI;
using Xtramile.WeatherAPI.Controllers;
using Xtramile.WeatherAPI.Shared.Dtos;
using Xtramile.WeatherApp.Cities;
using Xtramile.WeatherApp.Common.Dtos;
using Xunit;

namespace Xtramile.WeatherAPITest
{
    public class CityControllerTest
    {
        [Theory]
        [InlineData("australia")]
        [InlineData("indonesia")]
        public void ShouldReturnCitiesOfCountry(string country)
        {
            var cityServiceMock = new Mock<CityService>();
            cityServiceMock.Setup(m => m.GetCitiesByCountry(It.IsAny<GetCitiesByCountryRequest>()))
                .Returns(new AppResultDto<IList<CityDto>>
                {
                    Succeeded = true,
                    Status = 200,
                    Data = new List<CityDto>
                    {
                        new CityDto { Name = "Melbourne", Country = "Australia" },
                        new CityDto { Name = "Brisbane", Country = "Australia" },
                        new CityDto { Name = "Jakarta", Country = "Indonesia" },
                        new CityDto { Name = "Jayapura", Country = "Indonesia" },
                    }
                });

            var cityController = new CityController(cityServiceMock.Object);

            var result = cityController.GetCitiesByCountry(new GetCitiesByCountryApiRequestDto
            {
                Country = country
            });

            var actionResult = Assert.IsType<ActionResult<AppResultDto<IList<CityDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Theory]
        [InlineData("new zealand")]
        [InlineData("papua new guinea")]
        public void ShouldReturnNotFound(string country)
        {
            var cityServiceMock = new Mock<CityService>();
            cityServiceMock.Setup(m => m.GetCitiesByCountry(It.IsAny<GetCitiesByCountryRequest>()))
                .Returns(new AppResultDto<IList<CityDto>>
                {
                    Succeeded = false,
                    Status = 404,
                    Errors = new string[] { "Invalid country." }
                });

            var cityController = new CityController(cityServiceMock.Object);

            var result = cityController.GetCitiesByCountry(new GetCitiesByCountryApiRequestDto
            {
                Country = country
            });

            var actionResult = Assert.IsType<ActionResult<AppResultDto<IList<CityDto>>>>(result);
            var objectResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [Theory]
        [InlineData("TooLongCountryNameTooLongCountryNameTooLongCountryNameTooLongCountryName")]
        public async Task ShouldReturnBadRequest(string country)
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var client = server.CreateClient();

            using var response = await client.GetAsync($"/api/city?Country={country}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
