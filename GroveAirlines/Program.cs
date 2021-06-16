using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace GroveAirlines
{
    class Program
    {
        static void Main(string[] args)
        {
            InitalizeHost();
        }

        private static void InitalizeHost() =>
          Host.CreateDefaultBuilder()   // HOST BUILDER (with defaults)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
                builder.UseUrls("http://0.0.0.0:8080"); // point to URL/PORT
            }).Build().Run();
    }
}