using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<TemperatureService, XtramileTemperatureService>();

            services.AddHttpClient<OpenWeatherMapApiClient, OpenWeatherMapApiClientImplementation>();
            services.AddScoped<WeatherService, OpenWeatherMapWeatherService>();

            services.AddScoped<CountryRepository, MockCountryRepository>();
            services.AddScoped<CityRepository, MockCityRepository>();
            services.AddScoped<CountryService, MockCountryService>();
            services.AddScoped<CityService, MockCityService>();

            return services;
        }
    }
}
