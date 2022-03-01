using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xtramile.WeatherAPI;
using Xtramile.WeatherAPI.Controllers;
using Xtramile.WeatherAPI.Shared.Dtos;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Weather;
using Xunit;

namespace Xtramile.WeatherAPITest
{
    public class WeatherControllerTest
    {
        [Theory]
        [InlineData("Melbourne")]
        [InlineData("Jayapura")]
        public async Task ShouldReturnWeatherOfCity(string city)
        {
            var weatherServiceMock = new Mock<WeatherService>();
            weatherServiceMock.Setup(m => m.GetWeatherByCity(It.IsAny<GetWeatherByCityRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AppResultDto<WeatherDto>
                {
                    Succeeded = true,
                    Status = 200,
                    Data = new WeatherDto
                    {
                        Location = new WeatherLocationDto
                        {
                            Latitude = -6.2146m,
                            Longitude = 106.8451m
                        },
                        Conditions = new List<WeatherConditionDto>
                        {  
                            new WeatherConditionDto
                            {
                                Condition = "Clouds",
                                Description = "scattered clouds"
                            }
                        },
                        Temperature = new WeatherTemperature
                        {
                            Fahrenheit = 83.17,
                            Celsius = 28.4,
                            Kelvin = 301.55
                        },
                        Pressure = 1007,
                        Humidity = 76,
                        DewPoint = new WeatherTemperature
                        {
                            Fahrenheit = 75.92,
                            Celsius = 24.4,
                            Kelvin = 297.55
                        },
                        Visibility = 6000,
                        Wind = new WeatherWindDto
                        {
                            Speed = 9.22,
                            Degree = 250
                        },
                        Timestamp = DateTimeOffset.FromUnixTimeSeconds(1646128195),
                        Timezone = 25200
                    }
                });

            var weatherController = new WeatherController(weatherServiceMock.Object);

            var cts = new CancellationTokenSource();

            var result = await weatherController.GetWeatherByCityAsync(new GetWeatherByCityApiRequestDto
            {
                City = city
            }, cts.Token);

            var actionResult = Assert.IsType<ActionResult<AppResultDto<WeatherDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Theory]
        [InlineData("InvalidCityName")]
        public async Task ShouldReturnNotFound(string city)
        {
            var weatherServiceMock = new Mock<WeatherService>();
            weatherServiceMock.Setup(m => m.GetWeatherByCity(It.IsAny<GetWeatherByCityRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AppResultDto<WeatherDto>
                {
                    Succeeded = false,
                    Status = 404,
                    Errors = new string[] { "Invalid city name." }
                });

            var weatherController = new WeatherController(weatherServiceMock.Object);

            var cts = new CancellationTokenSource();

            var result = await weatherController.GetWeatherByCityAsync(new GetWeatherByCityApiRequestDto
            {
                City = city
            }, cts.Token);

            var actionResult = Assert.IsType<ActionResult<AppResultDto<WeatherDto>>>(result);
            var objectResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [Theory]
        [InlineData("TooLongCityNameTooLongCityNameTooLongCityNameTooLongCityName")]
        public async Task ShouldReturnBadRequest(string city)
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var client = server.CreateClient();

            using var response = await client.GetAsync($"/api/weather?City={city}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
