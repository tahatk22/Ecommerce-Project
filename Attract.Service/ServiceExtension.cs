using Attract.Service.IService;
using Attract.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Attract.Service
{
    public static class ServiceExtension
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ICatgeoryService,CategoryService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<ISubCategoryService,SubCategoryService>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<ICustomSubCategoryService,CustomSubCategoryService>();
            services.AddScoped<ICartService,CartService>();
            services.AddScoped<ICartProductService,CartProductService>();
            services.AddScoped<IContactService,ConatctService>();

        }
        /*public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }*/
    }
}
