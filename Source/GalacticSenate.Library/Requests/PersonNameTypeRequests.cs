using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Library.Requests {
    public class AddPersonNameTypeRequest {
        public string Value { get; set; }
    }
    public class DeletePersonNameTypeRequest {
        public int Id { get; set; }
    }
    public class ReadPersonNameTypeMultiRequest {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadPersonNameTypeRequest {
        public int Id { get; set; }
    }

    public class ReadPersonNameTypeValueRequest {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }
    public class UpdatePersonNameTypeRequest {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
