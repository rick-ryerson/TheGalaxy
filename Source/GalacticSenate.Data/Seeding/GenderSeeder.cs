using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticSenate.Data.Seeding {
   public class GenderSeeder {
      public static void Seed(DataContext dataContext) {
         var existing = dataContext.Genders.ToList();

         var add = new List<Gender>()
         {
            new Gender() { Value = "Male" },
            new Gender() { Value = "Female" }
         };

         var dif = add.Where(a => !existing.Any(e => e.Value == a.Value));

         if (!dif.Any())
            return;

         dataContext.AddRange(dif);
      }
   }
}
