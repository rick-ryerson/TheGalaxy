using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Model;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Requests;
using Microsoft.Extensions.Logging;
using Polly.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services {
    public interface IFamilyService {
        Task<ModelResponse<Family, AddFamilyRequest>> AddAsync(AddFamilyRequest request);
        Task<ModelResponse<Family, ReadFamilyMultiRequest>> ReadAsync(ReadFamilyMultiRequest request);
    }
    public class FamilyService : InformalOrganizationService, IFamilyService {
        private readonly IFamilyRepository familyRepository;

        public FamilyService(IUnitOfWork<DataContext> unitOfWork,
            IFamilyRepository familyRepository,
            IOrganizationNameService organizationNameService,
            IEventBus eventBus,
            IEventsFactory eventsFactory,
            ILogger logger) :
            base(unitOfWork,
                familyRepository,
                organizationNameService,
                eventBus,
                eventsFactory, logger) {
            this.familyRepository = familyRepository;
        }

        public async Task<ModelResponse<Family, AddFamilyRequest>> AddAsync(AddFamilyRequest request) {
            var response = new ModelResponse<Family, AddFamilyRequest>(DateTime.Now, request);

            try {
                if (request is not null) {
                    ModelResponse<InformalOrganization, AddInformalOrganizationRequest> informalOrganizationResponse = await ((IInformalOrganizationService)this).AddAsync((AddInformalOrganizationRequest)request);
                    response.Messages.AddRange(informalOrganizationResponse.Messages);
                    var family = await ((IRepository<Family, Guid>)familyRepository).GetAsync(request.Id);

                    if (family is null) {
                        family = await ((IRepository<Family, Guid>)familyRepository).AddAsync(new Family
                        {
                            Id = informalOrganizationResponse.Results.First().Id,
                            InformalOrganizationId = informalOrganizationResponse.Results.First().Id
                        });

                        unitOfWork.Save();
                        eventBus.Publish(eventsFactory.Created(family));
                        response.Messages.Add($"Family with id {request.Id} added.");
                    } else {
                        response.Messages.Add($"Family with id {request.Id} already exists.");
                    }
                    response.Results.Add(family);
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
        private async Task<(Party, List<string>)> GetPartySync(Guid id) {
            var partyResponse = await ((IPartyService)this).ReadAsync(new ReadPartyRequest { Id = id });
            var result = partyResponse.Results.FirstOrDefault();

            return (result, partyResponse.Messages);
        }
        private async Task<(Organization, List<string>)> GetOrganizationSync(Guid id) {
            var organizationResponse = await ((IOrganizationService)this).ReadAsync(new ReadOrganizationRequest { Id = id });
            var result = organizationResponse.Results.FirstOrDefault();
            var messages = new List<string>();

            if (result != null) {
                var x = await GetPartySync(id);
                result.Party = x.Item1;
                messages.AddRange(x.Item2);
            }

            return (result, organizationResponse.Messages);
        }
        private async Task<(InformalOrganization, List<string>)> GetInformalOrganizationAsync(Guid id) {
            var informalOrganizationResponse = await ((IInformalOrganizationService)this).GetAsync(new ReadInformalOrganizationRequest { Id = id });
            var result = informalOrganizationResponse.Results.FirstOrDefault();
            var messages = new List<string>();

            if (result != null) {

                var x = await GetOrganizationSync(id);
                result.Organization = x.Item1;
                messages.AddRange(x.Item2);
            }
            return (result, messages);

        }
        public async Task<ModelResponse<Family, ReadFamilyMultiRequest>> ReadAsync(ReadFamilyMultiRequest request) {
            var response = new ModelResponse<Family, ReadFamilyMultiRequest>(DateTime.Now, request);

            try {
                var families = ((IRepository<Family, Guid>)familyRepository).Get(request.PageIndex, request.PageSize);

                foreach (var family in families) {
                    var x = await GetInformalOrganizationAsync(family.Id);

                    family.InformalOrganization = x.Item1;
                    response.Messages.AddRange(x.Item2);

                    response.Results.Add(family);
                }

                response.Results.AddRange(families);
                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }
    }
}