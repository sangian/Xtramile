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
using Xtramile.WeatherApp.OpenWeatherMap;
using Xtramile.WeatherInfra.External.OpenWeatherMap;
using Xunit;

namespace Xtramile.WeatherAppTest
{
    public class OpenWeatherMapApiClientTest
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

            var cts = new CancellationTokenSource();

            var apiResponse = await apiClient.GetCurrentWeather(new CurrentWeatherRequestDto
            {
                CityName = city
            }, cts.Token);

            Assert.True(apiResponse.Succeeded);
        }

        [Theory]
        [InlineData("Jayapura")]
        public async Task ShouldNotSucceededWhenTimedOut(string city)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.RequestTimeout,
                Content = null
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
                ApiTimeout = 500
            };
            IOptions<OpenWeatherMapConfiguration> optionsMock = Options.Create(configurationMock);

            var loggerMock = new Mock<ILogger<OpenWeatherMapApiClientImpl>>();

            OpenWeatherMapApiClient apiClient = new OpenWeatherMapApiClientImpl(httpClientMock, optionsMock, loggerMock.Object);

            var cts = new CancellationTokenSource();

            var apiResponse = await apiClient.GetCurrentWeather(new CurrentWeatherRequestDto
            {
                CityName = city
            }, cts.Token);

            Assert.False(apiResponse.Succeeded);
            Assert.Equal(0, apiResponse.Status);
        }
    }
}
