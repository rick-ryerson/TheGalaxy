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
using System.Linq;
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
            ILogger logger) :
            base(unitOfWork,
                informalOrganizationRepository,
                organizationNameRepository,
                organizationNameValueRepository,
                eventBus,
                eventsFactory, logger) {
        }

        public async Task<ModelResponse<Family, AddFamilyRequest>> AddAsync(AddFamilyRequest request) {
            var response = new ModelResponse<Model.Family, AddFamilyRequest>(DateTime.Now, request);

            try {
                if (request is not null) {
                    var informalOrganizationResponse = this.AddAsync((AddInformalOrganizationRequest)request);
                    var family = await ((IRepository<Model.Family, Guid>)organizationRepository).GetAsync(request.Id);

                    if (family is null) {
                        family = await ((IRepository<Model.Family, Guid>)organizationRepository).AddAsync(new Model.Family
                        {
                            Id = informalOrganizationResponse.Result.Results.FirstOrDefault().Id,
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
    }
}
