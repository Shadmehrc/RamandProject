using Application.IRepository;
using Application.Tools;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UserEndpoint.Configs
{
    public static class AddServices
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IConfigRepository, ConfigRepository>();

            services.AddTransient<ConfigService>();


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


        }
    }
}
