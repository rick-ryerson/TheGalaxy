using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Services.OrganizationName.Events;
using GalacticSenate.Library.Services.OrganizationName.Requests;
using GalacticSenate.Library.Services.OrganizationNameValue;
using GalacticSenate.Library.Services.OrganizationNameValue.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.OrganizationName {
   public interface IOrganizationNameService
    {
        Task<ModelResponse<Model.OrganizationName, AddOrganizationNameRequest>> AddAsync(AddOrganizationNameRequest request);
    }
    public class OrganizationNameService : BasicServiceBase, IOrganizationNameService
    {

        private readonly IOrganizationNameRepository organizationNameRepository;
        private readonly IOrganizationNameEventsFactory organizationNameEventsFactory;
        private readonly IOrganizationNameValueService organizationNameValueService;

        public OrganizationNameService(IUnitOfWork<DataContext> unitOfWork,
           IOrganizationNameRepository organizationNameRepository,
           IEventBus eventBus,
           IOrganizationNameEventsFactory organizationNameEventsFactory,
           IOrganizationNameValueService organizationNameValueService,
           ILogger<OrganizationNameValueService> logger) : base(unitOfWork, eventBus, logger)
        {
            this.organizationNameRepository = organizationNameRepository ?? throw new ArgumentNullException(nameof(organizationNameRepository));
            this.organizationNameEventsFactory = organizationNameEventsFactory ?? throw new ArgumentNullException(nameof(organizationNameEventsFactory));
            this.organizationNameValueService = organizationNameValueService ?? throw new ArgumentNullException(nameof(organizationNameValueService));
        }

        public async Task<ModelResponse<Model.OrganizationName, AddOrganizationNameRequest>> AddAsync(AddOrganizationNameRequest request)
        {
            var response = new ModelResponse<Model.OrganizationName, AddOrganizationNameRequest>(DateTime.Now, request);

            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.OrganizationName))
                throw new ArgumentNullException(nameof(request.OrganizationName));

            try
            {
                // adding will return existing if present
                var existingNameValueResponse = await organizationNameValueService.AddAsync(new AddOrganizationNameValueRequest
                {
                    Value = request.OrganizationName
                });
                // there must be a result so use First() (not FirstOrDefault())
                var organizationNameValue = existingNameValueResponse.Results.First();

                var organizationName = new Model.OrganizationName
                {
                    FromDate = request.FromDate,
                    OrganizationId = request.OrganizationId,
                    OrganizationNameValueId = organizationNameValue.Id,
                    ThruDate = request.ThruDate,
                };

                organizationName = await organizationNameRepository.AddAsync(organizationName);

                if (organizationName == null)
                {
                    throw new ApplicationException($"Could not create organizationName");
                }

                unitOfWork.Save();

                response.Results.Add(organizationName);

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }
    }
}
