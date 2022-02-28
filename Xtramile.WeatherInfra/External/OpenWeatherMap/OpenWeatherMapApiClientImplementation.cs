using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.OpenWeatherMap;

namespace Xtramile.WeatherInfra.External.OpenWeatherMap
{
    public class OpenWeatherMapApiClientImplementation : OpenWeatherMapApiClient
    {
        private static readonly string ApiKey = "50b1eac3ebc361291f52b9629180f008";

        private readonly HttpClient apiClient;
        private readonly ILogger<OpenWeatherMapApiClientImplementation> logger;

        public OpenWeatherMapApiClientImplementation(HttpClient apiClient, ILogger<OpenWeatherMapApiClientImplementation> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
        }

        public async Task<AppResultDto<CurrentWeatherResponseDto>> GetCurrentWeather(CurrentWeatherRequestDto request, CancellationToken cancellationToken)
        {
            var response = new AppResultDto<CurrentWeatherResponseDto>();

            try
            {
                var reqMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"http://api.openweathermap.org/data/2.5/weather?q={request.CityName}&units=imperial&appid={ApiKey}"),
                    Content = null
                };

                using var resMessage = await apiClient.SendAsync(reqMessage, cancellationToken);
                var msgStream = await resMessage.Content.ReadAsStreamAsync();
                response.Status = ((int)resMessage.StatusCode);

                if (resMessage.IsSuccessStatusCode)
                {
                    response.Succeeded = true;
                    response.Status = 200;
                    response.Data = await JsonSerializer.DeserializeAsync<CurrentWeatherResponseDto>(msgStream, cancellationToken: cancellationToken);
                }
                else
                {
                    response.Succeeded = false;
                    response.Status = (int)resMessage.StatusCode;

                    switch (response.Status) 
                    {
                        case 404:
                            response.Errors = new string[] { "Invalid city name." };
                            break;
                        case 429:
                            response.Errors = new string[] { "Max API calls limit reached." };
                            break;
                        case 500:
                        case 502:
                        case 503:
                        case 504:
                            response.Errors = new string[] { "Unknown error. Please contact API provider." };
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogWarning("Exception when executing {0}.{1} with Request={2} Exception={3}", nameof(OpenWeatherMapApiClientImplementation), nameof(GetCurrentWeather), request, e.Message);

                throw;
            }

            logger.LogDebug("OpenWeatherMapApiClientImpl.GetCurrentWeather Result={@Result}", response);

            return response;
        }
    }
}
