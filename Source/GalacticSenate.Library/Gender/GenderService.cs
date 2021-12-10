using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Library.Gender.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Gender
{
    public interface IGenderService
    {
        Task<ModelResponse<Model.Gender, AddGenderRequest>> AddAsync(AddGenderRequest request);
        Task<BasicResponse<DeleteGenderRequest>> DeleteAsync(DeleteGenderRequest request);
        Task<ModelResponse<Model.Gender, ReadGenderMultiRequest>> ReadAsync(ReadGenderMultiRequest request);
        Task<ModelResponse<Model.Gender, ReadGenderRequest>> ReadAsync(ReadGenderRequest request);
        Task<ModelResponse<Model.Gender, ReadGenderValueRequest>> ReadAsync(ReadGenderValueRequest request);
        Task<ModelResponse<Model.Gender, UpdateGenderRequest>> UpdateAsync(UpdateGenderRequest request);
    }

    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork<DataContext> unitOfWork;
        private readonly IGenderRepository genderRepository;

        public GenderService(IUnitOfWork<DataContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.genderRepository = unitOfWork.GetGenderRepository() ?? throw new ApplicationException("Couldn't create gender repository");
        }

        public async Task<ModelResponse<Model.Gender, AddGenderRequest>> AddAsync(AddGenderRequest request)
        {
            var response = new ModelResponse<Model.Gender, AddGenderRequest>(DateTime.Now, request);

            var existing = await genderRepository.GetExactAsync(request.Value);

            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));
                if (string.IsNullOrEmpty(request.Value))
                    throw new ArgumentNullException(nameof(request.Value));

                if (existing is null)
                {
                    existing = await genderRepository.AddAsync(new Model.Gender { Value = request.Value });
                    unitOfWork.Save();

                    response.Messages.Add($"Gender with value {request.Value} added.");
                }
                else
                {
                    response.Messages.Add($"Gender with value {request.Value} already exists.");
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
        public async Task<ModelResponse<Model.Gender, UpdateGenderRequest>> UpdateAsync(UpdateGenderRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.NewValue))
                throw new ArgumentNullException(nameof(request.NewValue));

            var response = new ModelResponse<Model.Gender, UpdateGenderRequest>(DateTime.Now, request);

            Model.Gender existing = null;

            try
            {
                existing = await genderRepository.GetAsync(request.Id);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            if (existing is null)
            {
                response.Messages.Add($"Gender with id {request.Id} does not exist.");
                response.Status = StatusEnum.Failed;
            }
            else
            {
                try
                {
                    var oldValue = existing.Value;

                    if (oldValue == request.NewValue)
                    {
                        response.Messages.Add($"Gender with id {existing.Id} already has a value of {oldValue}.");
                    }
                    else
                    {
                        existing.Value = request.NewValue;

                        genderRepository.Update(existing);
                        unitOfWork.Save();

                        response.Messages.Add($"Gender with id {existing.Id} updated from {oldValue} to {existing.Value}.");
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

        public async Task<ModelResponse<Model.Gender, ReadGenderMultiRequest>> ReadAsync(ReadGenderMultiRequest request)
        {
            var response = new ModelResponse<Model.Gender, ReadGenderMultiRequest>(DateTime.Now, request);

            try
            {
                response.Results.AddRange(genderRepository.Get(request.PageIndex, request.PageSize));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            return await Task.Run(() => { return response.Finalize(); });
        }
        public async Task<ModelResponse<Model.Gender, ReadGenderValueRequest>> ReadAsync(ReadGenderValueRequest request)
        {
            var response = new ModelResponse<Model.Gender, ReadGenderValueRequest>(DateTime.Now, request);

            try
            {
                if (request.Exact)
                    response.Results.Add(await genderRepository.GetExactAsync(request.Value));
                else
                    response.Results.AddRange(genderRepository.GetContains(request.Value));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }
        public async Task<ModelResponse<Model.Gender, ReadGenderRequest>> ReadAsync(ReadGenderRequest request)
        {
            var response = new ModelResponse<Model.Gender, ReadGenderRequest>(DateTime.Now, request);

            try
            {
                var gender = await genderRepository.GetAsync(request.Id);

                if (gender != null)
                    response.Results.Add(await genderRepository.GetAsync(request.Id));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }

        public async Task<BasicResponse<DeleteGenderRequest>> DeleteAsync(DeleteGenderRequest request)
        {
            var response = new BasicResponse<DeleteGenderRequest>(DateTime.Now, request);

            try
            {
                await genderRepository.DeleteAsync(request.Id);
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
