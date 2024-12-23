﻿using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Data.Interfaces.Repositories {
    public interface IFamilyRepository : IInformalOrganizationRepository, IRepository<Family, Guid> {
    }
}
