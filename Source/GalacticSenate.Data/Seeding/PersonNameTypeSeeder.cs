using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticSenate.Data.Seeding {
   public class PersonNameTypeSeeder {
      public static void Seed(DataContext dataContext) {
         var existing = dataContext.PersonNameTypes.ToList();

         var add = new List<PersonNameType>()
         {
            new PersonNameType() { Value = "Given" },
            new PersonNameType() { Value = "Family" },
            new PersonNameType() { Value = "Middle" }
         };

         var diff = add.Where(a => !existing.Any(e => a.Value == e.Value));
      }
   }
}
