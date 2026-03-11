using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

// Established connection to GitHub: https://github.com/StranoGianni/MeterReadingAPI
// Comment that verify the connection
namespace EnsekMeterReadingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
