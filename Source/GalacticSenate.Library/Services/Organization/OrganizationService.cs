using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Requests;
using GalacticSenate.Library.Services.Party;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.Organization {
    public interface IOrganizationService {
        Task<ModelResponse<Model.Organization, AddOrganizationRequest>> AddAsync(AddOrganizationRequest request);
    }
    public class OrganizationService : PartyService, IOrganizationService {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IPartyRepository partyRepository;
        private readonly IOrganizationNameRepository organizationNameRepository;
        private readonly IOrganizationNameValueRepository organizationNameValueRepository;

        public OrganizationService(IUnitOfWork<DataContext> unitOfWork,
           IOrganizationRepository organizationRepository,
           IOrganizationNameRepository organizationNameRepository,
           IOrganizationNameValueRepository organizationNameValueRepository,
           IEventBus eventBus,
           IEventsFactory eventsFactory,
           ILogger logger) :
            base(unitOfWork,
               organizationRepository,
               eventBus,
               eventsFactory,
               logger) {

            // base(unitOfWork, eventBus, logger) {

            this.partyRepository = partyRepository ?? throw new ArgumentNullException(nameof(partyRepository));
            this.organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
            this.organizationNameRepository = organizationNameRepository ?? throw new ArgumentNullException(nameof(organizationNameRepository));
            this.organizationNameValueRepository = organizationNameValueRepository ?? throw new ArgumentNullException(nameof(organizationNameValueRepository));
        }
        private async Task<Model.OrganizationNameValue> AddOrganizationNameValueAsync(string value) {
            var nameValue = await organizationNameValueRepository.GetExactAsync(value);

            if (nameValue == null) {
                nameValue = await organizationNameValueRepository.AddAsync(new Model.OrganizationNameValue { Value = value });
            }

            return nameValue;
        }
        private Task<Model.OrganizationName> AddOrganizationNameAsync(Guid organizationId, string organizationNameValue) {
            throw new NotImplementedException();
        }

        public Task<ModelResponse<Model.Organization, AddOrganizationRequest>> AddAsync(AddOrganizationRequest request) {
            throw new NotImplementedException();
        }
    }
}
