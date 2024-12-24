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
    }
    public class InformalOrganizationService : OrganizationService, IInformalOrganizationService {
        public InformalOrganizationService(IUnitOfWork<DataContext> unitOfWork,
            IInformalOrganizationRepository informalOrganizationRepository,
            IOrganizationNameRepository organizationNameRepository,
            IOrganizationNameValueRepository organizationNameValueRepository,
            IEventBus eventBus,
            IEventsFactory eventsFactory,
            ILogger logger) :
            base(unitOfWork,
                informalOrganizationRepository,
                organizationNameRepository,
                organizationNameValueRepository,
                eventBus,
                eventsFactory,
                logger) {
        }

        public async Task<ModelResponse<InformalOrganization, AddInformalOrganizationRequest>> AddAsync(AddInformalOrganizationRequest request) {
            var response = new ModelResponse<InformalOrganization, AddInformalOrganizationRequest>(DateTime.Now, request);

            try {
                if (request is not null) {
                    var organizationResponse = AddAsync((AddOrganizationRequest)request);
                    var informalOrganization = await ((IRepository<InformalOrganization, Guid>)organizationRepository).GetAsync(request.Id);

                    if (informalOrganization is null) {
                        informalOrganization = await ((IRepository<InformalOrganization, Guid>)organizationRepository).AddAsync(new InformalOrganization
                        {
                            Id = organizationResponse.Result.Results.FirstOrDefault().Id,
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
    }
}
