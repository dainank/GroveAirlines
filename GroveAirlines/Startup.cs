using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace GroveAirlines
{
    class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  // called by HostBuilder (enables usage of controllers/endpoints)
        {
            app.UseRouting();   // routing & routing decisions for the service
            app.UseEndpoints(endpoints => endpoints.MapControllers());  // endpoint pattern for routing web requests (maps all controllers in our service)
        }

        public void ConfigureServices(IServiceCollection services)  // registering startup with host
        {
            services.AddControllers();
        }
    }
}
