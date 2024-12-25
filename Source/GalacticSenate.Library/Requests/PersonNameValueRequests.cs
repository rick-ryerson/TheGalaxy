using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Library.Requests {
    public class AddPersonNameValueRequest {
        public string Value { get; set; }
    }
    public class DeletePersonNameValueRequest {
        public int Id { get; set; }
    }
    public class ReadPersonNameValueMultiRequest {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadPersonNameValueRequest {
        public int Id { get; set; }
    }

    public class ReadPersonNameValueValueRequest {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }
    public class UpdatePersonNameValueRequest {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
