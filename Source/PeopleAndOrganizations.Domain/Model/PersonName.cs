﻿using Celestial.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Domain.Model
{
    public class PersonName : HistoricRelation<Person, PersonNameValue>
    {
        public int Ordinal { get; set; }
    }
}
