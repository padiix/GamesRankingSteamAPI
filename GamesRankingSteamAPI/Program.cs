using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GamesRankingSteamAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task preparedb = Task.Run(() => 
            { 
                Service.IGDBAPI IGDBAPI = new Service.IGDBAPI();
                IGDBAPI.GetDataAndSendToDatabase(); 
            });
            preparedb.Wait();
            Console.WriteLine("Preparing database: " + preparedb.Status);
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
