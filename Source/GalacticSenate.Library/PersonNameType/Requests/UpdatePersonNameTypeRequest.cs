using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.PersonNameType.Requests {
   public class UpdatePersonNameTypeRequest {
      public int Id { get; set; }
      public string NewValue { get; set; }
   }
}
