using Microsoft.EntityFrameworkCore;
using GalacticSenate.Data.Implementations.EntityFramework;
using System;
using Microsoft.Extensions.Hosting;
using GalacticSenate.Data.Seeding;
using Microsoft.Extensions.DependencyInjection;
using GalacticSenate.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using GalacticSenate.Data.Extensions;
using GalacticSenate.Library.Extensions;

using EventBus.RabbitMQ;

namespace GalacticSenate.ConsoleApp {
   class Program {
      static void Main(string[] args) {
         Console.WriteLine("Hello World from GalacticSenate.ConsoleApp!");

         Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
               IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

               var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

               var efDataSettings = new EfDataSettings()
               {
                  ConnectionString = Configuration.GetConnectionString("DataContext")
               };

               var eventBusSettings = EventBusSettings.Bind(Configuration);

               services.AddPeopleAndOrganizations(eventBusSettings, efDataSettings);
            })
            .Build()
            .SeedData()
            .Run();
      }
   }
}
