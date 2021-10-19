using Kitchen.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen
{
     public class Program
     {
          public static void Main(string[] args)
          {
               CreateHostBuilder(args).Build().Run();
          }

          public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                       webBuilder.UseStartup<Startup>();
                       List<CookingAparatus> cookingAparatus = new() { new CookingAparatus("oven"), new CookingAparatus("stove")};
                       List<Cook> cooks = new()
                       {
                            new Cook(1, "John", Enums.CookRankEnum.ExecutiveChef, 3, "Hey, panini head, are you listening to me?"),
                            new Cook(2, "Leo", Enums.CookRankEnum.LineCook, 2, "Hi")
                       };
                       var kitchen = new KitchenRoom(cooks, cookingAparatus);
                  });
     }
}
