using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Services.Gender.Requests
{
    public class UpdateGenderRequest
    {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
