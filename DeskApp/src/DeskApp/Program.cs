using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace DeskApp
{
    public class Program
    {
        public static string address()
        {
            return "";
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseUrls("http://localhost:60000", "http://localhost:60001")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();


            //if (DateTime.Now < DateTime.Parse("7/22/2016"))
            //{
            host.Run();
            //}
        }
    }
}
