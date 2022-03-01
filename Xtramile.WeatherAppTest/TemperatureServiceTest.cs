using Xtramile.WeatherApp.Weather;
using Xunit;

namespace Xtramile.WeatherAppTest
{
    public class TemperatureServiceTest
    {
        [Theory]
        [InlineData(83.17)]
        public void ShouldConvertFromFahrenheitToCelsius(double fahrenheit)
        {
            TemperatureService temperatureService = new XtramileTemperatureService();
            
            double celsius = temperatureService.ConvertFromFahrenheitToCelsius(fahrenheit);

            Assert.Equal(28.4, celsius);
        }

        [Theory]
        [InlineData(28.4, 76)]
        public void ShouldCalculateDewPointInCelsius(double tempInCelsius, int humidity)
        {
            TemperatureService temperatureService = new XtramileTemperatureService();

            double dewPointInCelsius = temperatureService.CalculateDewPointInCelsius(tempInCelsius, humidity);

            Assert.Equal(24.4, dewPointInCelsius);
        }
    }
}
