using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xtramile.WeatherApp.Cities;
using Xtramile.WeatherApp.Common.Configurations;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherApp.Countries;
using Xtramile.WeatherApp.OpenWeatherMap;
using Xtramile.WeatherApp.Weather;
using Xtramile.WeatherInfra.External.OpenWeatherMap;
using Xtramile.WeatherInfra.Persistence.Repositories;

namespace Xtramile.WeatherInfra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // read configurations
            var owmConfigSection = configuration.GetSection(OpenWeatherMapConfiguration.ConfigKey);

            // inject configurations
            services.AddOptions<OpenWeatherMapConfiguration>().Bind(owmConfigSection);

            services.AddSingleton<TemperatureService, XtramileTemperatureService>();

            services.AddHttpClient<OpenWeatherMapApiClient, OpenWeatherMapApiClientImpl>();
            services.AddScoped<WeatherService, OpenWeatherMapWeatherService>();

            services.AddScoped<CountryRepository, MockCountryRepository>();
            services.AddScoped<CityRepository, MockCityRepository>();
            services.AddScoped<CountryService, MockCountryService>();
            services.AddScoped<CityService, MockCityService>();

            return services;
        }
    }
}
