using GroveAirlines.DatabaseLayer;
using GroveAirlines.RepositoryLayer;
using GroveAirlines.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace GroveAirlines
{
    internal class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  // called by HostBuilder (enables usage of controllers/endpoints)
        {
            app.UseRouting();   // routing & routing decisions for the service
            app.UseEndpoints(endpoints => endpoints.MapControllers());  // endpoint pattern for routing web requests (maps all controllers in our service)
        }

        public void ConfigureServices(IServiceCollection services)  // registering startup with host
        {
            //controllers
            services.AddControllers();

            //services
            services.AddTransient(typeof(FlightService), typeof(FlightService));
            services.AddTransient(typeof(BookingService), typeof(BookingService));
            services.AddTransient(typeof(AirportService), typeof(AirportService));

            //repositories
            services.AddTransient(typeof(FlightRepository), typeof(FlightRepository));
            services.AddTransient(typeof(AirportRepository), typeof(AirportRepository));
            services.AddTransient(typeof(BookingRepository), typeof(BookingRepository));
            services.AddTransient(typeof(CustomerRepository), typeof(CustomerRepository));

            // database
            services.AddDbContext<GroveAirlinesContext>(ServiceLifetime.Transient);
            services.AddTransient(typeof(GroveAirlinesContext), typeof(GroveAirlinesContext));
        }
    }
}
