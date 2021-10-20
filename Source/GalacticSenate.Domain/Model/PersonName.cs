using Celestial.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Domain.Model
{
    public class PersonName
    {
        public Guid PersonId { get; set; }
        public int PersonNameValueId { get; set; }
        public int PersonNameTypeId { get; set; }
        public int Ordinal { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }

        public virtual Person Person { get; set; }
        public virtual PersonNameType PersonNameType { get; set; }
        public virtual PersonNameValue PersonNameValue { get; set; }
    }
}
