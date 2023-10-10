using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Domain.Model {
   public class Organization {
      public Guid Id { get; set; }
      public Guid PartyId { get; set; }

      public virtual Party Party { get; set; }

      public virtual List<OrganizationName> Names { get; set; }
   }
}
