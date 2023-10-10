using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Domain.Model;
using System.Collections.Generic;
using System.Linq;

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

         var dif = add.Where(a => !existing.Any(e => a.Value == e.Value));

         if (!dif.Any())
            return;

         dataContext.AddRange(dif);
      }
   }
}
