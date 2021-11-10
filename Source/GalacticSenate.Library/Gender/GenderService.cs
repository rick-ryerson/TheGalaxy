using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
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
        Task<ModelResponse<Model.Gender>> AddAsync(AddGenderRequest request);
        BasicResponse Delete(DeleteGenderRequest request);
        Task<ModelResponse<Model.Gender>> ReadAsync(ReadGenderMultiRequest request);
        Task<ModelResponse<Model.Gender>> ReadAsync(ReadGenderRequest request);
        Task<ModelResponse<Model.Gender>> ReadAsync(ReadGenderValueRequest request);
        Task<ModelResponse<Model.Gender>> UpdateAsync(UpdateGenderRequest request);
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

        public async Task<ModelResponse<Model.Gender>> AddAsync(AddGenderRequest request)
        {
            var response = new ModelResponse<Model.Gender>(DateTime.Now);

            var existing = await genderRepository.GetExactAsync(request.Value);

            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));
                if (string.IsNullOrEmpty(request.Value))
                    throw new ArgumentNullException(nameof(request.Value));

                if (existing is null)
                {
                    existing = genderRepository.Add(new Model.Gender { Value = request.Value });
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
        public async Task<ModelResponse<Model.Gender>> UpdateAsync(UpdateGenderRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.NewValue))
                throw new ArgumentNullException(nameof(request.NewValue));

            var response = new ModelResponse<Model.Gender>(DateTime.Now);

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
                    existing.Value = request.NewValue;

                    genderRepository.Update(existing);
                    unitOfWork.Save();

                    response.Messages.Add($"Gender with id {existing.Id} updated from {oldValue} to {existing.Value}.");
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

        public async Task<ModelResponse<Model.Gender>> ReadAsync(ReadGenderMultiRequest request)
        {
            var response = new ModelResponse<Model.Gender>(DateTime.Now);

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
            return response.Finalize();
        }
        public async Task<ModelResponse<Model.Gender>> ReadAsync(ReadGenderValueRequest request)
        {
            var response = new ModelResponse<Model.Gender>(DateTime.Now);

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
        public async Task<ModelResponse<Model.Gender>> ReadAsync(ReadGenderRequest request)
        {
            var response = new ModelResponse<Model.Gender>(DateTime.Now);

            try
            {
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

        public BasicResponse Delete(DeleteGenderRequest request)
        {
            var response = new BasicResponse(DateTime.Now);

            try
            {
                genderRepository.DeleteAsync(request.Id);
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
