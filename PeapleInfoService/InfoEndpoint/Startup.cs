using Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserEndpoint.Configs;
using MediatR;
using System;
using System.Reflection;
using Application.Commands.CreateUserCommand;
using Application.Query.GetUserList.GetUserListQuery;
using Application.Tools;

namespace UserEndpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        private ConfigModel _configs;

        public void ConfigureServices(IServiceCollection services)
        {

            
            
            services.AddService();
            services.AddControllers();


            var sp = services.BuildServiceProvider();
            var configService = sp.GetService<ConfigService>();
          _configs = configService.GetConfigs().GetAwaiter().GetResult();
          
          services.AddMediatR(typeof(CreateUserCommand));
            services.AddMediatR(typeof(GetUserListQuery));

            services.AddJwtAuthorization(_configs);
            services.AddSwaggerService();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwaggerService(env);
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
