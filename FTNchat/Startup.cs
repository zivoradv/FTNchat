using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FTNchat.Data;
using Microsoft.Extensions.Configuration;

namespace FTNchat
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FTNchatContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                                 new MySqlServerVersion(new Version(8, 1, 0))); // Specify your MySQL version
            });

            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use the property names as-is
                        options.JsonSerializerOptions.WriteIndented = true; // Format the JSON for readability
                    });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });


            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
    {
        Console.WriteLine("CORS middleware executing...");
        await next.Invoke();
    });

            app.UseCors("AllowAnyOrigin"); // Enable CORS before routing

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
