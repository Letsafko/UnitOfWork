using Application.CreateNewOrder;
using Autofac;
using FluentValidation.AspNetCore;
using Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Presentation.Modules;
using Presentation.Modules.Autofac;

namespace Presentation
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///  Creates an instance of <see cref="Startup"/>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;
        private const string DatabaseScheme = "Database";

        /// <summary>
        /// Configure service collection.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<DatabaseConfiguration>(_configuration.GetSection(DatabaseScheme))
                .AddHttpContextAccessor()
                .AddRouting(options => options.LowercaseUrls = true)
                .AddSwagger()
                .AddMvc()
                .AddControllersAsServices()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.UseCamelCasing(true);
                })
                .Services
                .AddControllers()
                .AddFluentValidation(cfg 
                    => cfg.RegisterValidatorsFromAssemblyContaining<CreateNewOrderCommandValidator>());
        }

        /// <summary>
        /// Configure application.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCustomSwagger();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        /// <summary>
        ///     Configures autofac container.
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new WebApiModule());
        }
    }
}