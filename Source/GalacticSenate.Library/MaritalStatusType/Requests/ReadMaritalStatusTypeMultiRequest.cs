using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.MaritalStatusType.Requests
{
    public class ReadMaritalStatusTypeMultiRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadMaritalStatusTypeRequest
    {
        public int Id { get; set; }
    }

    public class ReadMaritalStatusTypeValueRequest
    {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }
}
