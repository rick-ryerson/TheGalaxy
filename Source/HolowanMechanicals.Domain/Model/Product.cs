using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolowanMechanicals.Domain.Model {
   public class Product {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public DateOnly Introduced { get; set; }
      public DateOnly SalesDiscontinuation { get; set; }
      public DateOnly SupportDiscontinuation { get; set; }
   }
   public class Good : Product {
      //
   }
   public class Service : Product {
      //
   }

   public class CategoryRollup {
      public int MadeOfCategoryId { get; set; }
      public int PartOfCategoryId { get; set; }

      public Category MadeOfCategory { get; set; }
      public Category PartOfCategory { get; set; }
   }
}
