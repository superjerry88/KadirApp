using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KadirApp
{
    public class Program
    {

        /*
         *  public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Accept All HTTP Request Methods from all origins
            app.UseCors(builder =>
                builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseMvc();
        }
         */
        public static void Main(string[] args)
        {
//            var host = new WebHostBuilder()
//                .UseKestrel()
//                .UseContentRoot(Directory.GetCurrentDirectory())
//                .UseIISIntegration()
//                .UseStartup<Startup>()
//                .UseUrls("http://localhost:89001/")
//                .Build();
//
//            host.Run();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    
    }
}
