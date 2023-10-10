﻿using System;

namespace GalacticSenate.Domain.Model {
   public class PersonMaritalStatus
    {
        public Guid PersonId { get; set; }
        public int MaritalStatusId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }

        public virtual Person Person { get; set; }
        public virtual MaritalStatusType MaritalStatus { get; set; }
    }
}
