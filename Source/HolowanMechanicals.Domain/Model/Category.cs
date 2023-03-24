using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolowanMechanicals.Domain.Model {
   public class Category {
      public int Id { get; set; }
      public string Description { get; set; }
   }
   public class UsageCategory : Category { }
   public class IndustryCategory : Category { }
   public class MaterialsCategory : Category { }

   public class CategoryClassification {
      
   }
}
