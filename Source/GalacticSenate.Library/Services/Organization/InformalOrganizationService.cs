using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
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
    public interface IInformalOrganizationService {
        Task<ModelResponse<Model.InformalOrganization, AddInformalOrganizationRequest>> AddAsync(AddInformalOrganizationRequest request);
    }
    public class InformalOrganizationService : OrganizationService, IInformalOrganizationService {
        public InformalOrganizationService(IUnitOfWork<DataContext> unitOfWork,
            IInformalOrganizationRepository informalOrganizationRepository,
            IOrganizationNameRepository organizationNameRepository,
            IOrganizationNameValueRepository organizationNameValueRepository,
            IEventBus eventBus,
            IEventsFactory<Model.Party, Guid> partyEventsFactory,
            IEventsFactory<Model.OrganizationNameValue, int> organizationNameValueEventsFactory,
            ILogger logger) :
            base(unitOfWork, informalOrganizationRepository, organizationNameRepository, organizationNameValueRepository, eventBus, partyEventsFactory, organizationNameValueEventsFactory, logger) {
        }

        public Task<ModelResponse<InformalOrganization, AddInformalOrganizationRequest>> AddAsync(AddInformalOrganizationRequest request) {
            throw new NotImplementedException();
        }
    }
}
