using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Model;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services {
    public interface IOrganizationService {
        Task<ModelResponse<Model.Organization, AddOrganizationRequest>> AddAsync(AddOrganizationRequest request);
        Task<ModelResponse<Model.Organization, ReadOrganizationMultiRequest>> ReadAsync(ReadOrganizationMultiRequest request);
        Task<ModelResponse<Model.Organization, ReadOrganizationRequest>> ReadAsync(ReadOrganizationRequest request);
    }
    public class OrganizationService : PartyService, IOrganizationService {
        private readonly IOrganizationRepository organizationRepository;
        protected readonly IOrganizationNameRepository organizationNameRepository;
        protected readonly IOrganizationNameValueRepository organizationNameValueRepository;

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

        public async Task<ModelResponse<Model.Organization, AddOrganizationRequest>> AddAsync(AddOrganizationRequest request) {
            var response = new ModelResponse<Model.Organization, AddOrganizationRequest>(DateTime.Now, request);

            try {
                if (request is not null) {
                    var partyResponse = AddAsync((AddPartyRequest)request);
                    var organization = await ((IRepository<Model.Organization, Guid>)organizationRepository).GetAsync(request.Id);

                    if (organization is null) {
                        organization = await organizationRepository.AddAsync(new Model.Organization
                        {
                            Id = partyResponse.Result.Results.FirstOrDefault().Id,
                        });

                        unitOfWork.Save();
                        eventBus.Publish(eventsFactory.Created(organization));
                        response.Messages.Add($"Organization with id {request.Id} added.");
                    } else {
                        response.Messages.Add($"Organization with id {request.Id} already exists.");
                    }
                    response.Results.Add(organization);
                    response.Status = StatusEnum.Successful;
                } else
                    throw new ArgumentNullException(nameof(request));
            }
            catch (Exception ex) {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }

        public async Task<ModelResponse<Organization, ReadOrganizationMultiRequest>> ReadAsync(ReadOrganizationMultiRequest request) {
            var response = new ModelResponse<Organization, ReadOrganizationMultiRequest>(DateTime.Now, request);

            try {
                var organizations = ((IRepository<Organization, Guid>)organizationRepository).Get(request.PageIndex, request.PageSize);
                foreach (var organization in organizations) {
                    var partyResponse = await ((IPartyService)this).ReadAsync(new ReadPartyRequest { Id = organization.Id });
                    organization.Party = partyResponse.Results.First();

                    response.Results.Add(organization);

                    response.Status = StatusEnum.Successful;
                }
            }
            catch (Exception ex) {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }

        public async Task<ModelResponse<Organization, ReadOrganizationRequest>> ReadAsync(ReadOrganizationRequest request) {
            var response = new ModelResponse<Organization, ReadOrganizationRequest>(DateTime.Now, request);

            try {
                var organization = await ((IRepository<Organization, Guid>)organizationRepository).GetAsync(request.Id);

                if (organization != null) {
                    var partyResponse = await ((IPartyService)this).ReadAsync(request);
                    organization.Party = partyResponse.Results.First();

                    response.Results.Add(organization);

                    response.Status = StatusEnum.Successful;
                } else {
                    response.Status = StatusEnum.Failed;
                    response.Messages.Add($"There is no related Party. Organization {request.Id} is orphaned.");
                }
            }
            catch (Exception ex) {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }
    }
}
