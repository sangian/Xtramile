using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xtramile.WeatherApp.Common.Configurations;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.OpenWeatherMap;

namespace Xtramile.WeatherInfra.External.OpenWeatherMap
{
    public class OpenWeatherMapApiClientImpl : OpenWeatherMapApiClient
    {
        private readonly HttpClient apiClient;
        private readonly OpenWeatherMapConfiguration configuration;
        private readonly ILogger<OpenWeatherMapApiClientImpl> logger;

        public OpenWeatherMapApiClientImpl(HttpClient apiClient, IOptions<OpenWeatherMapConfiguration> configuration, ILogger<OpenWeatherMapApiClientImpl> logger)
        {
            this.apiClient = apiClient;
            this.configuration = configuration.Value;
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
                    RequestUri = new Uri($"http://api.openweathermap.org/data/2.5/weather?q={request.CityName}&units={configuration.Units}&appid={configuration.ApiKey}"),
                    Content = null
                };

                // set timeout
                apiClient.Timeout = TimeSpan.FromMilliseconds(configuration.ApiTimeout);

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

                    // More details see API errors section at the following page: https://openweathermap.org/faq

                    switch (response.Status)
                    {
                        case 401:
                            response.Errors = new string[] { "Invalid API key." };
                            break;
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
                            response.Errors = new string[] { "Server error. Please contact API provider." };
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Guid requestId = Guid.NewGuid();

                logger.LogWarning("Exception when executing {Class}.{Method} with RequestId={ReqId} Request={Req} Exception={@Exception}", nameof(OpenWeatherMapApiClientImpl), nameof(GetCurrentWeather), requestId, request, e);

                response.Succeeded = false;
                response.Status = 0;
                response.Errors = new string[] { $"Unknown error. Please refer to log with RequestId:{requestId} for detail." };
            }

            logger.LogDebug("OpenWeatherMapApiClientImpl.GetCurrentWeather Response={@Response}", response);

            return response;
        }
    }
}
