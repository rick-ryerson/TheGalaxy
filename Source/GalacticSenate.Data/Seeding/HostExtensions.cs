using GalacticSenate.Data.Implementations.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Data.Seeding {
   public static class HostExtensions {
      public static IHost SeedData(this IHost host) {

         using (var context = host.Services.GetService<DataContext>()) {
            GenderSeeder.Seed(context);
            MaritalStatusTypeSeeder.Seed(context);
            PersonNameTypeSeeder.Seed(context);
         }

         return host;
      }
   }
}
