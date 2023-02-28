using Microsoft.EntityFrameworkCore;
using GalacticSenate.Data.Implementations.EntityFramework;
using System;
using Microsoft.Extensions.Hosting;
using GalacticSenate.Data.Seeding;
using Microsoft.Extensions.DependencyInjection;
using GalacticSenate.Data.Interfaces;
using Microsoft.Extensions.Configuration;

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
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

               var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

               services.AddDbContext<DataContext>(options =>
               {

                  options.UseSqlServer(Configuration.GetConnectionString("DataContext"));
               });

               services.AddScoped<IUnitOfWork<DataContext>, UnitOfWork>();
            })
            .Build()
            .SeedData()
            .Run();
      }
   }
}
