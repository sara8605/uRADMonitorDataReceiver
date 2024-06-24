using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using uRADMonitorDataReceiver.Models;

namespace uRADMonitorDataReceiver
{
    public sealed class Startup
    {
        public static void ConfigureServices(WebApplicationBuilder applicationBuilder)
        {
            IServiceCollection services = applicationBuilder.Services;
            ConfigurationManager configuration = applicationBuilder.Configuration;

            // MVC services

            services.AddControllers();

            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "uRADMonitor", Version = "v1" });
            });


            // App Services

            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("AppDbContext");
                options
                    .UseSqlServer(connectionString, b => b.MigrationsAssembly("uRADMonitor"));
            });

        }

        public static void ConfigurePipeline(WebApplication app)
        {
            //app.UseStaticFiles("/view");
            app.UseHttpsRedirection();

            app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CAMERA API V1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "uRADMonitor Api";
            });

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "uRADMonitor API V1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "uRADMonitor Api";
            });


            app.MapDefaultControllerRoute();

        }
    }
}
