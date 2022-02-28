using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.OpenWeatherMap;

namespace Xtramile.WeatherApp.Weather
{
    public class OpenWeatherMapWeatherService : WeatherService
    {
        private readonly OpenWeatherMapApiClient openWeatherMapApi;
        private readonly TemperatureService temperatureService;

        public OpenWeatherMapWeatherService(OpenWeatherMapApiClient openWeatherMapApi, TemperatureService temperatureService)
        {
            this.openWeatherMapApi = openWeatherMapApi;
            this.temperatureService = temperatureService;
        }

        public async Task<AppResultDto<WeatherDto>> GetWeatherByCity(GetWeatherByCityRequest request, CancellationToken cancellationToken)
        {
            var response = new AppResultDto<WeatherDto>();

            var result = await openWeatherMapApi.GetCurrentWeather(new CurrentWeatherRequestDto
            {
                CityName = request.City
            }, cancellationToken);

            response.Succeeded = result.Succeeded;
            response.Status = result.Status;
            response.Errors = result.Errors;

            if (result.Succeeded)
            {
                IList<WeatherCondition> conditions = new List<WeatherCondition>();

                foreach (var condition in result.Data.Conditions)
                {
                    conditions.Add(new WeatherCondition
                    {
                        Condition = condition.Condition,
                        Description = condition.Description
                    });
                }

                decimal tempInCelsius = temperatureService.ConvertFromFahrenheitToCelsius(result.Data.Main.Temperature);
                decimal tempInKelvin = temperatureService.ConvertFromFahrenheitToKelvin(result.Data.Main.Temperature);
                decimal dewPointInCelsius = temperatureService.CalculateDewPoint(tempInCelsius, result.Data.Main.Humidity);
                decimal dewPointInFahrenheit = temperatureService.ConvertFromCelsiusToFahrenheit(dewPointInCelsius);
                decimal dewPointInKelvin = temperatureService.ConvertFromCelsiusToKelvin(dewPointInCelsius);

                response.Data = new WeatherDto
                {
                    Location = new WeatherLocationDto
                    {
                        Latitude = result.Data.Location.Latitude,
                        Longitude = result.Data.Location.Longitude
                    },
                    Conditions = conditions,
                    Temperature = new WeatherTemperature
                    {
                        Celsius = tempInCelsius,
                        Fahrenheit = result.Data.Main.Temperature,
                        Kelvin = tempInKelvin
                    },
                    Humidity = result.Data.Main.Humidity,
                    DewPoint = new WeatherTemperature
                    {
                        Celsius = dewPointInCelsius,
                        Fahrenheit = dewPointInFahrenheit,
                        Kelvin = dewPointInKelvin
                    },
                    Pressure = result.Data.Main.Pressure,
                    Visibility = result.Data.Visibility,
                    Wind = new WeatherWindDto
                    {
                        Speed = result.Data.Wind.Speed,
                        Degree = result.Data.Wind.Degree
                    },
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(result.Data.Timestamp),
                    Timezone = result.Data.Timezone
                };
            }

            return response;
        }
    }
}
