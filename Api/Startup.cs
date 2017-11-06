using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Api.Data;


namespace Api
{

    /// <summary>
    /// Application startup.
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Creates an application startup.
        /// </summary>
        /// <param name="configuration">Configuration to use.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// Gets the configuration.
        /// </summary>
        IConfiguration Configuration
        {
            get;
        }


        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseOptions = new DatabaseOptions();
            Configuration.GetSection("Database").Bind(databaseOptions);
            var connectionString = databaseOptions.ConnectionString;
            var databaseProvider = databaseOptions.Provider;

            if (databaseProvider.Equals("MySql", StringComparison.InvariantCultureIgnoreCase))
            {
                services.AddDbContext<HeroContext>(options => options.UseMySql(connectionString));
            }
            else
            {
                services.AddDbContext<HeroContext>(options => options.UseInMemoryDatabase(connectionString));
            }
            
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddCors(options =>
                options.AddPolicy("AllowAll", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

            services.AddMvc();
        }

        
        /// <summary>
        /// Configures the middleware used by the application.
        /// </summary>
        /// <param name="app">Application to configure.</param>
        /// <param name="heroContext">Hero context to use.</param>
        public void Configure(IApplicationBuilder app, HeroContext heroContext)
        {
            app.UseCors("AllowAll");

            app.UseMvc();

            heroContext.Database.EnsureCreated();
        }

    }

}
