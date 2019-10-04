using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Infrastructure.EF;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BikeStore {
  public class Program {
    //  public static void Main(string[] args) {
    //    CreateWebHostBuilder(args).Build().Run();
    //  }

    //  public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //      WebHost.CreateDefaultBuilder(args)
    //          .UseStartup<Startup>();
    //}

    public static void Main(string[] xArgs) {
      BuildWebHost(xArgs)
      .UpdateDatabase()
      .Run();
    }

    public static IWebHost BuildWebHost(string[] xArgs) =>
        WebHost.CreateDefaultBuilder(xArgs)
            .UseStartup<Startup>()
            .Build();
  }

}
