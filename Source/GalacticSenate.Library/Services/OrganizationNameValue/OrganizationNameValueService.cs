using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Services.OrganizationNameValue.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.OrganizationNameValue {
   public interface IOrganizationNameValueService
    {
        Task<ModelResponse<Model.OrganizationNameValue, AddOrganizationNameValueRequest>> AddAsync(AddOrganizationNameValueRequest request);
        Task<BasicResponse<DeleteOrganizationNameValueRequest>> DeleteAsync(DeleteOrganizationNameValueRequest request);
        Task<ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueMultiRequest>> ReadAsync(ReadOrganizationNameValueMultiRequest request);
        Task<ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueRequest>> ReadAsync(ReadOrganizationNameValueRequest request);
        Task<ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueValueRequest>> ReadAsync(ReadOrganizationNameValueValueRequest request);
        Task<ModelResponse<Model.OrganizationNameValue, UpdateOrganizationNameValueRequest>> UpdateAsync(UpdateOrganizationNameValueRequest request);
    }

    public class OrganizationNameValueService : BasicServiceBase, IOrganizationNameValueService
    {
        private readonly IOrganizationNameValueRepository organizationNameValueRepository;
        private readonly IEventsFactory<Model.OrganizationNameValue, int> eventFactory;

        public OrganizationNameValueService(IUnitOfWork<DataContext> unitOfWork,
           IOrganizationNameValueRepository organizationNameValueRepository,
           IEventBus eventBus,
           IEventsFactory<Model.OrganizationNameValue, int> eventFactory,
           ILogger<OrganizationNameValueService> logger) : base(unitOfWork, eventBus, logger)
        {
            this.organizationNameValueRepository = organizationNameValueRepository ?? throw new ArgumentNullException(nameof(organizationNameValueRepository));
            this.eventFactory = eventFactory ?? throw new ArgumentNullException(nameof(eventFactory));
        }

        public async Task<ModelResponse<Model.OrganizationNameValue, AddOrganizationNameValueRequest>> AddAsync(AddOrganizationNameValueRequest request)
        {
            var response = new ModelResponse<Model.OrganizationNameValue, AddOrganizationNameValueRequest>(DateTime.Now, request);

            var existing = await organizationNameValueRepository.GetExactAsync(request.Value);

            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));
                if (string.IsNullOrEmpty(request.Value))
                    throw new ArgumentNullException(nameof(request.Value));

                if (existing is null)
                {
                    existing = await organizationNameValueRepository.AddAsync(new Model.OrganizationNameValue { Value = request.Value });
                    unitOfWork.Save();

                    response.Messages.Add($"OrganizationNameValue with value {request.Value} added.");
                }
                else
                {
                    response.Messages.Add($"OrganizationNameValue with value {request.Value} already exists.");
                }

                response.Results.Add(existing);

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }
        public async Task<ModelResponse<Model.OrganizationNameValue, UpdateOrganizationNameValueRequest>> UpdateAsync(UpdateOrganizationNameValueRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.NewValue))
                throw new ArgumentNullException(nameof(request.NewValue));

            var response = new ModelResponse<Model.OrganizationNameValue, UpdateOrganizationNameValueRequest>(DateTime.Now, request);

            Model.OrganizationNameValue existing = null;

            try
            {
                existing = await organizationNameValueRepository.GetAsync(request.Id);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            if (existing is null)
            {
                response.Messages.Add($"OrganizationNameValue with id {request.Id} does not exist.");
                response.Status = StatusEnum.Failed;
            }
            else
            {
                try
                {
                    var oldValue = existing.Value;

                    if (oldValue == request.NewValue)
                    {
                        response.Messages.Add($"OrganizationNameValue with id {existing.Id} already has a value of {oldValue}.");
                    }
                    else
                    {
                        existing.Value = request.NewValue;

                        organizationNameValueRepository.Update(existing);
                        unitOfWork.Save();

                        response.Messages.Add($"OrganizationNameValue with id {existing.Id} updated from {oldValue} to {existing.Value}.");
                    }

                    response.Results.Add(existing);

                    response.Status = StatusEnum.Successful;
                }
                catch (Exception ex)
                {
                    response.Messages.Add(ex.Message);
                    response.Status = StatusEnum.Failed;
                }
            }

            return response.Finalize();
        }

        public async Task<ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueMultiRequest>> ReadAsync(ReadOrganizationNameValueMultiRequest request)
        {
            var response = new ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueMultiRequest>(DateTime.Now, request);

            try
            {
                response.Results.AddRange(organizationNameValueRepository.Get(request.PageIndex, request.PageSize));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            return await Task.Run(() => { return response.Finalize(); });
        }
        public async Task<ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueValueRequest>> ReadAsync(ReadOrganizationNameValueValueRequest request)
        {
            var response = new ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueValueRequest>(DateTime.Now, request);

            try
            {
                if (request.Exact)
                    response.Results.Add(await organizationNameValueRepository.GetExactAsync(request.Value));
                else
                    response.Results.AddRange(organizationNameValueRepository.GetContains(request.Value));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }
        public async Task<ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueRequest>> ReadAsync(ReadOrganizationNameValueRequest request)
        {
            var response = new ModelResponse<Model.OrganizationNameValue, ReadOrganizationNameValueRequest>(DateTime.Now, request);

            try
            {
                var OrganizationNameValue = await organizationNameValueRepository.GetAsync(request.Id);

                if (OrganizationNameValue != null)
                    response.Results.Add(await organizationNameValueRepository.GetAsync(request.Id));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }

        public async Task<BasicResponse<DeleteOrganizationNameValueRequest>> DeleteAsync(DeleteOrganizationNameValueRequest request)
        {
            var response = new BasicResponse<DeleteOrganizationNameValueRequest>(DateTime.Now, request);

            try
            {
                await organizationNameValueRepository.DeleteAsync(request.Id);
                unitOfWork.Save();

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
