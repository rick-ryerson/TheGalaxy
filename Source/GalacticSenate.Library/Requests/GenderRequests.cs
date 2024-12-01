using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Requests {
    public class AddGenderRequest {
        public string Value { get; set; }
    }

    public class DeleteGenderRequest {
        public int Id { get; set; }
    }

    public class ReadGenderMultiRequest {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadGenderRequest {
        public int Id { get; set; }
    }

    public class ReadGenderValueRequest {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }

    public class UpdateGenderRequest {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
