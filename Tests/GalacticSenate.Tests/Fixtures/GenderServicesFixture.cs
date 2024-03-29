﻿using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Services.Gender.Events;

namespace GalacticSenate.Tests.Fixtures {
   public abstract class GenderServicesFixture : GalacticSenateFixture {
      protected readonly IGenderRepository genderRepository;
      protected readonly IGenderEventsFactory genderEventsFactory;

      protected GenderServicesFixture(string databaseName) : base(databaseName) {
         this.genderRepository = new GenderRepository(unitOfWork);
         this.genderEventsFactory = new GenderEventsFactory();
      }
   }
}
