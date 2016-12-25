namespace NancyWebApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();

            var host = new WebHostBuilder()
                .UseContentRoot(path)
                .UseUrls(NancyWebAppConfig.Url)
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}