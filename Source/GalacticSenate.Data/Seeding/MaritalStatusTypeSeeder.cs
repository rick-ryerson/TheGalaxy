using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticSenate.Data.Seeding {
   public class MaritalStatusTypeSeeder {
      public static void Seed(DataContext dataContext) {
         var existing = dataContext.MaritalStatusTypes.ToList();

         var add = new List<MaritalStatusType>()
         {
            new MaritalStatusType() { Value = "Single" },
            new MaritalStatusType() { Value = "Married" },
            new MaritalStatusType() { Value = "Divorced" }
         };

         var dif = add.Where(a => !existing.Any(e => a.Value == e.Value));

         if (!dif.Any())
            return;

         dataContext.AddRange(dif);
      }
   }
}
