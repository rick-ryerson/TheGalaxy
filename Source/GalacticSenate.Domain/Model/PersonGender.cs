﻿using System;

namespace GalacticSenate.Domain.Model {
   public class PersonGender
    {
        public Guid PersonId { get; set; }
        public int GenderId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }

        public virtual Person Person { get; set; }
        public virtual Gender Gender { get; set; }
    }
}
