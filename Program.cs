using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AEP_WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder => {
                    webHostBuilder
                        .UseStartup<Startup>();
                        // Per eseguire il deploy su IIS, decommentare il codice sottostante e lanciare il comando: dotnet publish --configuration Release
                });
    }
}
