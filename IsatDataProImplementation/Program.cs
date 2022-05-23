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
            IsatDataProService service = new();

            Console.ReadLine();


            
                       
        }
    }
}
