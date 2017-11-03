using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api.Models;


namespace Api
{

    /// <summary>
    /// Application startup.
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HeroContext>(options => options.UseInMemoryDatabase("Heroes"));
            
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
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowAll");

            app.UseMvc();
        }

    }

}
