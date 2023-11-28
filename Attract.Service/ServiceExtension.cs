using Attract.Service.IService;
using Attract.Service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Attract.Service
{
    public static class ServiceExtension
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ICatgeoryService,CategoryService>();
        }
    }
}
