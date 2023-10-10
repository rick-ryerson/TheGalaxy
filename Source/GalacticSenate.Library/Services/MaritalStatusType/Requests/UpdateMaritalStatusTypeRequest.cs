using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Services.MaritalStatusType.Requests
{
    public class UpdateMaritalStatusTypeRequest
    {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
