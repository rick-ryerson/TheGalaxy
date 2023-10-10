using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Services.PersonNameValue.Requests
{
    public class UpdatePersonNameValueRequest
    {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
