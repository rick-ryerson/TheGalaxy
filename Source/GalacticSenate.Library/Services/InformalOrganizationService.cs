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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services {
    public interface IInformalOrganizationService {
        Task<ModelResponse<InformalOrganization, AddInformalOrganizationRequest>> AddAsync(AddInformalOrganizationRequest request);
        Task<ModelResponse<InformalOrganization, ReadInformalOrganizationMultiRequest>> GetAsync(ReadInformalOrganizationMultiRequest request);
        Task<ModelResponse<InformalOrganization, ReadInformalOrganizationRequest>> GetAsync(ReadInformalOrganizationRequest request);
    }
    public class InformalOrganizationService(IUnitOfWork<DataContext> unitOfWork,
        IInformalOrganizationRepository informalOrganizationRepository,
        IOrganizationNameService organizationNameService,
        IEventBus eventBus,
        IEventsFactory eventsFactory,
        ILogger logger) : OrganizationService(unitOfWork,
            informalOrganizationRepository,
            organizationNameService,
            eventBus,
            eventsFactory,
            logger), IInformalOrganizationService {
        public async Task<ModelResponse<InformalOrganization, ReadInformalOrganizationRequest>> GetAsync(ReadInformalOrganizationRequest request) {
            var response = new ModelResponse<InformalOrganization, ReadInformalOrganizationRequest>(DateTime.Now, request);

            try {
                var informalOrganization = await ((IRepository<InformalOrganization, Guid>)informalOrganizationRepository).GetAsync(request.Id);

                if (informalOrganization != null) {
                    var organizationResponse = await ((IOrganizationService)this).ReadAsync(new ReadOrganizationRequest { Id = informalOrganization.Id });

                    if (organizationResponse.Results.Count > 0) {
                        var organization = organizationResponse.Results.FirstOrDefault();

                        informalOrganization.Organization = organization;

                        response.Results.Add(informalOrganization);
                    }

                    response.Status = StatusEnum.Successful;
                } else {
                    response.Status = StatusEnum.Failed;
                    response.Messages.Add($"There is no related Organization, Informal Organization {informalOrganization.Id} is orphaned.");
                }
            }
            catch (Exception ex) {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }

        async Task<ModelResponse<InformalOrganization, AddInformalOrganizationRequest>> IInformalOrganizationService.AddAsync(AddInformalOrganizationRequest request) {
            var response = new ModelResponse<InformalOrganization, AddInformalOrganizationRequest>(DateTime.Now, request);

            try {
                if (request is not null) {
                    var organizationResponse = await AddAsync((AddOrganizationRequest)request);
                    var informalOrganization = await ((IRepository<InformalOrganization, Guid>)informalOrganizationRepository).GetAsync(request.Id);

                    if (informalOrganization is null) {
                        informalOrganization = await ((IRepository<InformalOrganization, Guid>)informalOrganizationRepository).AddAsync(new InformalOrganization
                        {
                            Id = organizationResponse.Results.FirstOrDefault().Id,
                        });

                        unitOfWork.Save();
                        eventBus.Publish(eventsFactory.Created(informalOrganization));
                        response.Messages.Add($"Informal Organization with id {request.Id} added.");
                    } else {
                        response.Messages.Add($"Informal Organization with id {request.Id} already exists.");
                    }
                    response.Results.Add(informalOrganization);
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

        async Task<ModelResponse<InformalOrganization, ReadInformalOrganizationMultiRequest>> IInformalOrganizationService.GetAsync(ReadInformalOrganizationMultiRequest request) {
            var response = new ModelResponse<InformalOrganization, ReadInformalOrganizationMultiRequest>(DateTime.Now, request);

            try {
                var informalOrganizations = ((IRepository<InformalOrganization, Guid>)informalOrganizationRepository).Get(request.PageIndex, request.PageSize);
                response.Results.AddRange(informalOrganizations);

                foreach (var informalOrganization in informalOrganizations) {
                    var organization = await ((IRepository<Model.Organization, Guid>)informalOrganizationRepository).GetAsync(informalOrganization.Id);
                    informalOrganization.Organization = organization;
                }

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
