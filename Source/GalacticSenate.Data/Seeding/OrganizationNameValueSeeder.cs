using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticSenate.Data.Seeding {
   public class OrganizationNameValueSeeder {
      public static void Seed(DataContext dataContext) {
         var existing = dataContext.OrganizationNameValues.ToList();

         var add = new List<OrganizationNameValue>()
         {
            new OrganizationNameValue() { Value = "McDonald's Corporation" },
            new OrganizationNameValue() { Value = "Cleveland Clinic" }
         };

         var dif = add.Where(a => !existing.Any(e => e.Value == a.Value));

         if (!dif.Any())
            return;

         dataContext.AddRange(dif);
      }
   }
}
