using Application.Facades;
using Application.Helper;
using Application.IFacades;
using Application.IRepositories;
using Application.IServices;
using Application.Services;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthenticationEndpoint.Configs
{
    public static class AddServices
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationFacade, AuthenticationFacade>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();


            services.AddScoped<ConfigService>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            //var serviceProvider = services.BuildServiceProvider();
            ////var logger = serviceProvider.GetService<ILogger<AuthenticationFacade>>();
            ////services.AddSingleton(typeof(ILogger), logger);        

            //var logger2 = serviceProvider.GetService<ILogger<AuthenticationService>>();
            //services.AddSingleton(typeof(ILogger), logger2);    

        }
    }
}
