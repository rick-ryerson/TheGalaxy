using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Library.MaritalStatusType.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.MaritalStatusType
{
    public interface IMaritalStatusTypeService
    {
        Task<ModelResponse<Model.MaritalStatusType, AddMaritalStatusTypeRequest>> AddAsync(AddMaritalStatusTypeRequest request);
        Task<BasicResponse<DeleteMaritalStatusTypeRequest>> DeleteAsync(DeleteMaritalStatusTypeRequest request);
        Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeMultiRequest>> ReadAsync(ReadMaritalStatusTypeMultiRequest request);
        Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeRequest>> ReadAsync(ReadMaritalStatusTypeRequest request);
        Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeValueRequest>> ReadAsync(ReadMaritalStatusTypeValueRequest request);
        Task<ModelResponse<Model.MaritalStatusType, UpdateMaritalStatusTypeRequest>> UpdateAsync(UpdateMaritalStatusTypeRequest request);
    }

    public class MaritalStatusTypeService : IMaritalStatusTypeService
    {
        private readonly IUnitOfWork<DataContext> unitOfWork;
        private readonly IMaritalStatusTypeRepository maritalStatusTypeRepository;

        public MaritalStatusTypeService(IUnitOfWork<DataContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.maritalStatusTypeRepository = unitOfWork.GetMaritalStatusTypeRepository() ?? throw new ApplicationException("Couldn't create maritalStatusType repository");
        }

        public async Task<ModelResponse<Model.MaritalStatusType, AddMaritalStatusTypeRequest>> AddAsync(AddMaritalStatusTypeRequest request)
        {
            var response = new ModelResponse<Model.MaritalStatusType, AddMaritalStatusTypeRequest>(DateTime.Now, request);

            var existing = await maritalStatusTypeRepository.GetExactAsync(request.Value);

            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));
                if (string.IsNullOrEmpty(request.Value))
                    throw new ArgumentNullException(nameof(request.Value));

                if (existing is null)
                {
                    existing = await maritalStatusTypeRepository.AddAsync(new Model.MaritalStatusType { Value = request.Value });
                    unitOfWork.Save();

                    response.Messages.Add($"MaritalStatusType with value {request.Value} added.");
                }
                else
                {
                    response.Messages.Add($"MaritalStatusType with value {request.Value} already exists.");
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
        public async Task<ModelResponse<Model.MaritalStatusType, UpdateMaritalStatusTypeRequest>> UpdateAsync(UpdateMaritalStatusTypeRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.NewValue))
                throw new ArgumentNullException(nameof(request.NewValue));

            var response = new ModelResponse<Model.MaritalStatusType, UpdateMaritalStatusTypeRequest>(DateTime.Now, request);

            Model.MaritalStatusType existing = null;

            try
            {
                existing = await maritalStatusTypeRepository.GetAsync(request.Id);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            if (existing is null)
            {
                response.Messages.Add($"MaritalStatusType with id {request.Id} does not exist.");
                response.Status = StatusEnum.Failed;
            }
            else
            {
                try
                {
                    var oldValue = existing.Value;

                    if (oldValue == request.NewValue)
                    {
                        response.Messages.Add($"MaritalStatusType with id {existing.Id} already has a value of {oldValue}.");
                    }
                    else
                    {
                        existing.Value = request.NewValue;

                        maritalStatusTypeRepository.Update(existing);
                        unitOfWork.Save();

                        response.Messages.Add($"MaritalStatusType with id {existing.Id} updated from {oldValue} to {existing.Value}.");
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

        public async Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeMultiRequest>> ReadAsync(ReadMaritalStatusTypeMultiRequest request)
        {
            var response = new ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeMultiRequest>(DateTime.Now, request);

            try
            {
                response.Results.AddRange(maritalStatusTypeRepository.Get(request.PageIndex, request.PageSize));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            return await Task.Run(() => { return response.Finalize(); });
        }
        public async Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeValueRequest>> ReadAsync(ReadMaritalStatusTypeValueRequest request)
        {
            var response = new ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeValueRequest>(DateTime.Now, request);

            try
            {
                if (request.Exact)
                    response.Results.Add(await maritalStatusTypeRepository.GetExactAsync(request.Value));
                else
                    response.Results.AddRange(maritalStatusTypeRepository.GetContains(request.Value));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }
        public async Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeRequest>> ReadAsync(ReadMaritalStatusTypeRequest request)
        {
            var response = new ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeRequest>(DateTime.Now, request);

            try
            {
                var maritalStatus = await maritalStatusTypeRepository.GetAsync(request.Id);

                if (maritalStatus != null)
                    response.Results.Add(maritalStatus);

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }

        public async Task<BasicResponse<DeleteMaritalStatusTypeRequest>> DeleteAsync(DeleteMaritalStatusTypeRequest request)
        {
            var response = new BasicResponse<DeleteMaritalStatusTypeRequest>(DateTime.Now, request);

            try
            {
                await maritalStatusTypeRepository.DeleteAsync(request.Id);
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
