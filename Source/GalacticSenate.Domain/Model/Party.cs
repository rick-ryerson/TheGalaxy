using System;

namespace GalacticSenate.Domain.Model {
   public class Party {
      public Guid Id { get; set; }

      public PartyRole PartyRole { get; set; }
   }
}
