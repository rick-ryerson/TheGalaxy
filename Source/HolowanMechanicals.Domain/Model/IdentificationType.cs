using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolowanMechanicals.Domain.Model {
   public class IdentificationType {
      public int Id { get; set; }
      public string Description { get; set; }
   }

   public class GoodIdentification {
      public int Id { get; set; }
      public Guid GoodId { get; set; }
      public string Value { get; set; }

      public Good Good { get; set; }
   }

   public class Upca : GoodIdentification { }
   public class Upce : GoodIdentification { }
   public class Sku : GoodIdentification { }
   public class Isbn : GoodIdentification { }
   public class ManufacturerId : GoodIdentification { }
}
