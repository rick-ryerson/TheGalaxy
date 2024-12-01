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
        private readonly IEventsFactory<Model.OrganizationNameValue, int> organizationNameValueEventsFactory;

        public OrganizationService(IUnitOfWork<DataContext> unitOfWork,
           IPartyRepository partyRepository,
           IOrganizationRepository organizationRepository,
           IOrganizationNameRepository organizationNameRepository,
           IOrganizationNameValueRepository organizationNameValueRepository,
           IEventBus eventBus,
           IEventsFactory<Model.Party, Guid> partyEventsFactory,
           IEventsFactory<Model.OrganizationNameValue, int> organizationNameValueEventsFactory,
           ILogger logger) : base(unitOfWork, partyRepository, eventBus, partyEventsFactory, logger) { 
            
            // base(unitOfWork, eventBus, logger) {

            this.partyRepository = partyRepository ?? throw new ArgumentNullException(nameof(partyRepository));
            this.organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
            this.organizationNameRepository = organizationNameRepository ?? throw new ArgumentNullException(nameof(organizationNameRepository));
            this.organizationNameValueRepository = organizationNameValueRepository ?? throw new ArgumentNullException(nameof(organizationNameValueRepository));
            this.organizationNameValueEventsFactory = organizationNameValueEventsFactory;
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
