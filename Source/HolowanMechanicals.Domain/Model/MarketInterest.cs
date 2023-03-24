using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolowanMechanicals.Domain.Model {
   public class MarketInterest {
      public int CategoryId { get; set; }

      public DateTime From { get; set; }
      public DateTime? Thru { get; set; }

      public virtual Category Category { get; set; }
   }
}
