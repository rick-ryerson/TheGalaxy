using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Library.Requests {
    public class AddMaritalStatusTypeRequest {
        public string Value { get; set; }
    }

    public class DeleteMaritalStatusTypeRequest {
        public int Id { get; set; }
    }

    public class ReadMaritalStatusTypeMultiRequest {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadMaritalStatusTypeRequest {
        public int Id { get; set; }
    }

    public class ReadMaritalStatusTypeValueRequest {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }

    public class UpdateMaritalStatusTypeRequest {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
