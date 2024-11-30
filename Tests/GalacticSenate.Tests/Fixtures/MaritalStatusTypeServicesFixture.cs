using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Tests.Fixtures {
   public abstract class MaritalStatusTypeServicesFixture : GalacticSenateFixture {
      protected readonly IMaritalStatusTypeRepository maritalStatusTypeRepository;
      protected readonly IEventsFactory<Model.MaritalStatusType, int> maritalStatusTypeEventsFactory;

      protected MaritalStatusTypeServicesFixture(string databaseName) : base(databaseName) {
         this.maritalStatusTypeRepository = new MaritalStatusTypeRepository(unitOfWork);
         this.maritalStatusTypeEventsFactory = new EventsFactory<Model.MaritalStatusType, int>();
      }
   }
}
