using System;

namespace GalacticSenate.Library.Services.Party.Requests {
   public class ReadPartyMultiRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadPartyRequest
    {
        public Guid Id { get; set; }
    }
}
