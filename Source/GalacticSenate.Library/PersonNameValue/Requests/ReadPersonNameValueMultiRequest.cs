using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.PersonNameValue.Requests
{
    public class ReadPersonNameValueMultiRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadPersonNameValueRequest
    {
        public int Id { get; set; }
    }

    public class ReadPersonNameValueValueRequest
    {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }
}
