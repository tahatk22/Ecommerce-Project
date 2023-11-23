using Microsoft.Extensions.DependencyInjection;

namespace Attract.Service
{
    public static class ServiceExtension
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IProductTest, ProductTest>();
        }
    }
}
