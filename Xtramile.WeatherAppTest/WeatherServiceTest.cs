using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xtramile.WeatherApp.Common.Configurations;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.OpenWeatherMap;
using Xtramile.WeatherApp.Weather;
using Xtramile.WeatherInfra.External.OpenWeatherMap;
using Xunit;

namespace Xtramile.WeatherAppTest
{
    public class WeatherServiceTest
    {
        [Theory]
        [InlineData("Melbourne")]
        [InlineData("Jayapura")]
        public async Task ShouldReturnWeatherOfCity(string city)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""coord"":{""lon"":106.8451,""lat"":-6.2146},""weather"":[{""id"":802,""main"":""Clouds"",""description"":""scattered clouds"",""icon"":""03d""}],""base"":""stations"",""main"":{""temp"":83.17,""feels_like"":90.5,""temp_min"":80.69,""temp_max"":86.09,""pressure"":1007,""humidity"":76},""visibility"":6000,""wind"":{""speed"":9.22,""deg"":250},""clouds"":{""all"":40},""dt"":1646128195,""sys"":{""type"":1,""id"":9383,""country"":""ID"",""sunrise"":1646089108,""sunset"":1646133119},""timezone"":25200,""id"":1642911,""name"":""Jakarta"",""cod"":200}", Encoding.UTF8, "application/json")
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClientMock = new HttpClient(handlerMock.Object);

            var configurationMock = new OpenWeatherMapConfiguration
            {
                ApiKey = "DummyApiKey",
                Units = "imperial",
                ApiTimeout = 5000
            };
            IOptions<OpenWeatherMapConfiguration> optionsMock = Options.Create(configurationMock);

            var loggerMock = new Mock<ILogger<OpenWeatherMapApiClientImpl>>();

            OpenWeatherMapApiClient apiClient = new OpenWeatherMapApiClientImpl(httpClientMock, optionsMock, loggerMock.Object);

            TemperatureService temperatureService = new XtramileTemperatureService();

            WeatherService weatherService = new OpenWeatherMapWeatherService(apiClient, temperatureService);

            var cts = new CancellationTokenSource();

            AppResultDto<WeatherDto> serviceResult = await weatherService.GetWeatherByCity(new GetWeatherByCityRequest
            {
                City = city
            }, cts.Token);

            Assert.True(serviceResult.Succeeded);
            Assert.Equal(83.17, serviceResult.Data.Temperature.Fahrenheit);
            Assert.Equal(28.4, serviceResult.Data.Temperature.Celsius);
            Assert.Equal(75.92, serviceResult.Data.DewPoint.Fahrenheit);
            Assert.Equal(24.4, serviceResult.Data.DewPoint.Celsius);
        }
    }
}
