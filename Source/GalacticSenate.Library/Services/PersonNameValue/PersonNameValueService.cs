using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Services.PersonNameValue.Events;
using GalacticSenate.Library.Services.PersonNameValue.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.PersonNameValue {
   public interface IPersonNameValueService
    {
        Task<ModelResponse<Model.PersonNameValue, AddPersonNameValueRequest>> AddAsync(AddPersonNameValueRequest request);
        Task<BasicResponse<DeletePersonNameValueRequest>> DeleteAsync(DeletePersonNameValueRequest request);
        Task<ModelResponse<Model.PersonNameValue, ReadPersonNameValueMultiRequest>> ReadAsync(ReadPersonNameValueMultiRequest request);
        Task<ModelResponse<Model.PersonNameValue, ReadPersonNameValueRequest>> ReadAsync(ReadPersonNameValueRequest request);
        Task<ModelResponse<Model.PersonNameValue, ReadPersonNameValueValueRequest>> ReadAsync(ReadPersonNameValueValueRequest request);
        Task<ModelResponse<Model.PersonNameValue, UpdatePersonNameValueRequest>> UpdateAsync(UpdatePersonNameValueRequest request);
    }

    public class PersonNameValueService : BasicServiceBase, IPersonNameValueService
    {
        private readonly IPersonNameValueRepository personNameValueRepository;
        private readonly IPersonNameValueEventsFactory personNameValueEventsFactory;

        public PersonNameValueService(IUnitOfWork<DataContext> unitOfWork,
           IPersonNameValueRepository personNameValueRepository,
           IEventBus eventBus,
           IPersonNameValueEventsFactory personNameValueEventsFactory,
           ILogger<PersonNameValueService> logger) : base(unitOfWork, eventBus, logger)
        {
            this.personNameValueRepository = personNameValueRepository ?? throw new ArgumentNullException(nameof(personNameValueRepository));
            this.personNameValueEventsFactory = personNameValueEventsFactory ?? throw new ArgumentNullException(nameof(personNameValueEventsFactory));
        }

        public async Task<ModelResponse<Model.PersonNameValue, AddPersonNameValueRequest>> AddAsync(AddPersonNameValueRequest request)
        {
            var response = new ModelResponse<Model.PersonNameValue, AddPersonNameValueRequest>(DateTime.Now, request);

            var existing = await personNameValueRepository.GetExactAsync(request.Value);

            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));
                if (string.IsNullOrEmpty(request.Value))
                    throw new ArgumentNullException(nameof(request.Value));

                if (existing is null)
                {
                    existing = await personNameValueRepository.AddAsync(new Model.PersonNameValue { Value = request.Value });
                    unitOfWork.Save();

                    response.Messages.Add($"PersonNameValue with value {request.Value} added.");
                }
                else
                {
                    response.Messages.Add($"PersonNameValue with value {request.Value} already exists.");
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
        public async Task<ModelResponse<Model.PersonNameValue, UpdatePersonNameValueRequest>> UpdateAsync(UpdatePersonNameValueRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.NewValue))
                throw new ArgumentNullException(nameof(request.NewValue));

            var response = new ModelResponse<Model.PersonNameValue, UpdatePersonNameValueRequest>(DateTime.Now, request);

            Model.PersonNameValue existing = null;

            try
            {
                existing = await personNameValueRepository.GetAsync(request.Id);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            if (existing is null)
            {
                response.Messages.Add($"PersonNameValue with id {request.Id} does not exist.");
                response.Status = StatusEnum.Failed;
            }
            else
            {
                try
                {
                    var oldValue = existing.Value;

                    if (oldValue == request.NewValue)
                    {
                        response.Messages.Add($"PersonNameValue with id {existing.Id} already has a value of {oldValue}.");
                    }
                    else
                    {
                        existing.Value = request.NewValue;

                        personNameValueRepository.Update(existing);
                        unitOfWork.Save();

                        response.Messages.Add($"PersonNameValue with id {existing.Id} updated from {oldValue} to {existing.Value}.");
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

        public async Task<ModelResponse<Model.PersonNameValue, ReadPersonNameValueMultiRequest>> ReadAsync(ReadPersonNameValueMultiRequest request)
        {
            var response = new ModelResponse<Model.PersonNameValue, ReadPersonNameValueMultiRequest>(DateTime.Now, request);

            try
            {
                response.Results.AddRange(personNameValueRepository.Get(request.PageIndex, request.PageSize));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            return await Task.Run(() => { return response.Finalize(); });
        }
        public async Task<ModelResponse<Model.PersonNameValue, ReadPersonNameValueValueRequest>> ReadAsync(ReadPersonNameValueValueRequest request)
        {
            var response = new ModelResponse<Model.PersonNameValue, ReadPersonNameValueValueRequest>(DateTime.Now, request);

            try
            {
                if (request.Exact)
                    response.Results.Add(await personNameValueRepository.GetExactAsync(request.Value));
                else
                    response.Results.AddRange(personNameValueRepository.GetContains(request.Value));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }
        public async Task<ModelResponse<Model.PersonNameValue, ReadPersonNameValueRequest>> ReadAsync(ReadPersonNameValueRequest request)
        {
            var response = new ModelResponse<Model.PersonNameValue, ReadPersonNameValueRequest>(DateTime.Now, request);

            try
            {
                var personNameValue = await personNameValueRepository.GetAsync(request.Id);

                if (personNameValue != null)
                    response.Results.Add(await personNameValueRepository.GetAsync(request.Id));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }

        public async Task<BasicResponse<DeletePersonNameValueRequest>> DeleteAsync(DeletePersonNameValueRequest request)
        {
            var response = new BasicResponse<DeletePersonNameValueRequest>(DateTime.Now, request);

            try
            {
                await personNameValueRepository.DeleteAsync(request.Id);
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
