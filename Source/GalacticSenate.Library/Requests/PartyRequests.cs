using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Library.Requests {
    public class DeletePartyRequest {
        public Guid Id { get; set; }
    }
    public class ReadPartyMultiRequest {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadPartyRequest {
        public Guid Id { get; set; }
    }
}
