using Microsoft.Extensions.DependencyInjection;
using Xtramile.WeatherApp;
using Xtramile.WeatherApp.Cities;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddApplication();

            services.AddSingleton<TemperatureService, XtramileTemperatureService>();

            services.AddHttpClient<OpenWeatherMapApiClientImplementation>();

            services.AddScoped<CountryRepository, MockCountryRepository>();
            services.AddScoped<CityRepository, MockCityRepository>();
            services.AddScoped<CountryService, MockCountryService>();
            services.AddScoped<CityService, MockCityService>();
            services.AddScoped<OpenWeatherMapApiClient, OpenWeatherMapApiClientImplementation>();
            services.AddScoped<WeatherService, OpenWeatherMapWeatherService>();

            return services;
        }
    }
}
