using Microsoft.EntityFrameworkCore;
using GalacticSenate.Data.Implementations.EntityFramework;
using System;
using Microsoft.Extensions.Hosting;
using GalacticSenate.Data.Seeding;

namespace GalacticSenate.ConsoleApp {
   class Program {
      static void Main(string[] args) {
         Console.WriteLine("Hello World from GalacticSenate.ConsoleApp!");

         Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {

            })
            .Build()
            .SeedData()
            .Run();
      }
   }
}
