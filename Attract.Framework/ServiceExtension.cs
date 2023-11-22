using Attract.Framework.Repositories;
using Attract.Framework.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Framework
{
    public static class ServiceExtension
    {
        public static void ConfigureFramework(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepositoryFactory, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
