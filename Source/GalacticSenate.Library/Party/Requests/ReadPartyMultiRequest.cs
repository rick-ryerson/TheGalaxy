using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Party.Requests
{
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
