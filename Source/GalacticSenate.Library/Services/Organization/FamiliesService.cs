using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Model;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.Organization {
    public interface IFamilyService {
        Task<ModelResponse<Model.Family, AddFamilyRequest>> AddAsync(AddFamilyRequest request);
    }
    public class FamilyService : InformalOrganizationService, IFamilyService {
        public FamilyService(IUnitOfWork<DataContext> unitOfWork,
            IInformalOrganizationRepository informalOrganizationRepository,
            IOrganizationNameRepository organizationNameRepository,
            IOrganizationNameValueRepository organizationNameValueRepository,
            IEventBus eventBus,
            IEventsFactory eventsFactory,
            ILogger logger) : base(unitOfWork, 
                informalOrganizationRepository, 
                organizationNameRepository, 
                organizationNameValueRepository, 
                eventBus, 
                eventsFactory, logger) {
        }

        public Task<ModelResponse<Family, AddFamilyRequest>> AddAsync(AddFamilyRequest request) {
            throw new NotImplementedException();
        }
    }
}
