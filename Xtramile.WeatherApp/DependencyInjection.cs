using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Xtramile.WeatherApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
