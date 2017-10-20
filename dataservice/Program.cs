using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NorthwindModel;

namespace dataservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataAccess.refreshEmployees().Wait();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
