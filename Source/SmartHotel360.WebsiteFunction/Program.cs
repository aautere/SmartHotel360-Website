using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace SmartHotel360.WebsiteFunction
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureFunctionsWorkerDefaults();
        }
    }
}