using System;
using System.Threading.Tasks;
using Gie.IsatDataPro;
using Newtonsoft.Json;
using Serilog;

namespace IsatDataProImplementation
{
    class Program
    {
        static async Task Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(outputTemplate: "[{Timestamp:u}][{Level:u3}][{Name}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("logs/myapp.txt", outputTemplate: "[{Timestamp:u}][{Level:u3}][{Name}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Initializes IsatData Pro Service
            IsatDataProService service = new("id", "password");
            try
            {
                string version = await IsatDataProMgsApi.GetInfoVersionAsync();
                Console.WriteLine($"MGS API Version: {version}");
            }
            catch { }
            

            var dateTime = await IsatDataProMgsApi.GetInfoUtcTimeAsync();
            Console.WriteLine($"MGS API DateTime: {dateTime}");

            //ForwardMessage[] messages = Array.Empty<ForwardMessage>();
            var result = await IsatDataProMgsApi.GetForwardStatusesAsync("id", "pass", DateTime.Now);

            service.Start(30);

            Console.ReadLine();

                      
        }
    }
}
