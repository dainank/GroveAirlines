using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace GroveAirlines
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InitializeHost();
        }

        private static void InitializeHost() =>
          Host.CreateDefaultBuilder()   // HOST BUILDER (with defaults)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
                builder.UseUrls("http://0.0.0.0:8080"); // point to URL/PORT
            }).Build().Run();
    }
}