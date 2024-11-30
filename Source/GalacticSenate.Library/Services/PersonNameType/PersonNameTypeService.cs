using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Services.PersonNameType.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.PersonNameType {
    public interface IPersonNameTypeService {
        Task<ModelResponse<Model.PersonNameType, AddPersonNameTypeRequest>> AddAsync(AddPersonNameTypeRequest request);
        Task<BasicResponse<DeletePersonNameTypeRequest>> DeleteAsync(DeletePersonNameTypeRequest request);
        Task<ModelResponse<Model.PersonNameType, ReadPersonNameTypeMultiRequest>> ReadAsync(ReadPersonNameTypeMultiRequest request);
        Task<ModelResponse<Model.PersonNameType, ReadPersonNameTypeRequest>> ReadAsync(ReadPersonNameTypeRequest request);
        Task<ModelResponse<Model.PersonNameType, ReadPersonNameTypeValueRequest>> ReadAsync(ReadPersonNameTypeValueRequest request);
        Task<ModelResponse<Model.PersonNameType, UpdatePersonNameTypeRequest>> UpdateAsync(UpdatePersonNameTypeRequest request);
    }
    public class PersonNameTypeService : BasicServiceBase, IPersonNameTypeService {
        private readonly IPersonNameTypeRepository personNameTypeRepository;
        private readonly IEventsFactory<Model.PersonNameType, int> personNameTypeEventsFactory;

        public PersonNameTypeService(IUnitOfWork<DataContext> unitOfWork,
           IPersonNameTypeRepository personNameTypeRepository,
           IEventBus eventBus,
           IEventsFactory<Model.PersonNameType, int> personNameTypeEventsFactory,
           ILogger<PersonNameTypeService> logger) : base(unitOfWork, eventBus, logger) {
            this.personNameTypeRepository = personNameTypeRepository ?? throw new ArgumentNullException(nameof(personNameTypeRepository));
            this.personNameTypeEventsFactory = personNameTypeEventsFactory ?? throw new ArgumentNullException(nameof(personNameTypeEventsFactory));
        }

        public async Task<ModelResponse<Model.PersonNameType, AddPersonNameTypeRequest>> AddAsync(AddPersonNameTypeRequest request) {
            var response = new ModelResponse<Model.PersonNameType, AddPersonNameTypeRequest>(DateTime.Now, request);

            var existing = await personNameTypeRepository.GetExactAsync(request.Value);

            try {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));
                if (string.IsNullOrEmpty(request.Value))
                    throw new ArgumentNullException(nameof(request.Value));

                if (existing is null) {
                    existing = await personNameTypeRepository.AddAsync(new Model.PersonNameType { Value = request.Value });
                    unitOfWork.Save();

                    response.Messages.Add($"PersonNameType with value {request.Value} added.");
                } else {
                    response.Messages.Add($"PersonNameType with value {request.Value} already exists.");
                }

                response.Results.Add(existing);

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }

        public async Task<BasicResponse<DeletePersonNameTypeRequest>> DeleteAsync(DeletePersonNameTypeRequest request) {
            var response = new BasicResponse<DeletePersonNameTypeRequest>(DateTime.Now, request);

            try {
                await personNameTypeRepository.DeleteAsync(request.Id);
                unitOfWork.Save();

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }

        public async Task<ModelResponse<Model.PersonNameType, ReadPersonNameTypeMultiRequest>> ReadAsync(ReadPersonNameTypeMultiRequest request) {
            var response = new ModelResponse<Model.PersonNameType, ReadPersonNameTypeMultiRequest>(DateTime.Now, request);

            try {
                response.Results.AddRange(personNameTypeRepository.Get(request.PageIndex, request.PageSize));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            return await Task.Run(() => { return response.Finalize(); });
        }
        public async Task<ModelResponse<Model.PersonNameType, ReadPersonNameTypeValueRequest>> ReadAsync(ReadPersonNameTypeValueRequest request) {
            var response = new ModelResponse<Model.PersonNameType, ReadPersonNameTypeValueRequest>(DateTime.Now, request);

            try {
                if (request.Exact)
                    response.Results.Add(await personNameTypeRepository.GetExactAsync(request.Value));
                else
                    response.Results.AddRange(personNameTypeRepository.GetContains(request.Value));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }
        public async Task<ModelResponse<Model.PersonNameType, ReadPersonNameTypeRequest>> ReadAsync(ReadPersonNameTypeRequest request) {
            var response = new ModelResponse<Model.PersonNameType, ReadPersonNameTypeRequest>(DateTime.Now, request);

            try {
                var gender = await personNameTypeRepository.GetAsync(request.Id);

                if (gender != null)
                    response.Results.Add(await personNameTypeRepository.GetAsync(request.Id));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }

        public async Task<ModelResponse<Model.PersonNameType, UpdatePersonNameTypeRequest>> UpdateAsync(UpdatePersonNameTypeRequest request) {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.NewValue))
                throw new ArgumentNullException(nameof(request.NewValue));

            var response = new ModelResponse<Model.PersonNameType, UpdatePersonNameTypeRequest>(DateTime.Now, request);

            Model.PersonNameType existing = null;

            try {
                existing = await personNameTypeRepository.GetAsync(request.Id);
            }
            catch (Exception ex) {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            if (existing is null) {
                response.Messages.Add($"PersonNameType with id {request.Id} does not exist.");
                response.Status = StatusEnum.Failed;
            } else {
                try {
                    var oldValue = existing.Value;

                    if (oldValue == request.NewValue) {
                        response.Messages.Add($"PersonNameType with id {existing.Id} already has a value of {oldValue}.");
                    } else {
                        existing.Value = request.NewValue;

                        personNameTypeRepository.Update(existing);
                        unitOfWork.Save();

                        response.Messages.Add($"PersonNameType with id {existing.Id} updated from {oldValue} to {existing.Value}.");
                    }

                    response.Results.Add(existing);

                    response.Status = StatusEnum.Successful;
                }
                catch (Exception ex) {
                    response.Messages.Add(ex.Message);
                    response.Status = StatusEnum.Failed;
                }
            }

            return response.Finalize();
        }
    }
}
