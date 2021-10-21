using GalacticSenate.Data.Interfaces;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Library.Gender.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Gender
{
    public interface IGenderService
    {
        ModelResponse<Model.Gender> Add(AddGenderRequest addGenderRequest);
        BasicResponse Delete(DeleteGenderRequest request);
        ModelResponse<Model.Gender> Read(ReadGenderMultiRequest request);
        ModelResponse<Model.Gender> Read(ReadGenderRequest request);
        ModelResponse<Model.Gender> Read(ReadGenderValueRequest request);
        ModelResponse<Model.Gender> Update(UpdateGenderRequest updateGenderRequest);
    }

    public class GenderService : IGenderService
    {
        private readonly IGenderRepository genderRepository;

        public GenderService(IGenderRepository genderRepository)
        {
            this.genderRepository = genderRepository ?? throw new ArgumentNullException(nameof(genderRepository));
        }

        public ModelResponse<Model.Gender> Add(AddGenderRequest addGenderRequest)
        {
            if (addGenderRequest is null)
                throw new ArgumentNullException(nameof(addGenderRequest));
            if (string.IsNullOrEmpty(addGenderRequest.Value))
                throw new ArgumentNullException(nameof(addGenderRequest.Value));

            var response = new ModelResponse<Model.Gender>(DateTime.Now);

            var existing = genderRepository.GetExact(addGenderRequest.Value);

            if (existing is null)
            {
                existing = genderRepository.Add(new Model.Gender { Value = addGenderRequest.Value });

                response.Messages.Add($"Gender with value {addGenderRequest.Value} added.");
            }
            else
            {
                response.Messages.Add($"Gender with value {addGenderRequest.Value} already exists.");
            }

            response.Results.Add(existing);
            response.Status = StatusEnum.Successful;

            response.Duration = DateTime.Now - response.Start;

            return response;
        }
        public ModelResponse<Model.Gender> Update(UpdateGenderRequest updateGenderRequest)
        {
            if (updateGenderRequest is null)
                throw new ArgumentNullException(nameof(updateGenderRequest));
            if (string.IsNullOrEmpty(updateGenderRequest.NewValue))
                throw new ArgumentNullException(nameof(updateGenderRequest.NewValue));

            var response = new ModelResponse<Model.Gender>(DateTime.Now);

            Model.Gender existing = null;

            try
            {
                existing = genderRepository.Get(updateGenderRequest.Id);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            if (existing is null)
            {
                response.Messages.Add($"Gender with id {updateGenderRequest.Id} does not exist.");
                response.Status = StatusEnum.Failed;
            }
            else
            {
                var oldValue = existing.Value;
                existing.Value = updateGenderRequest.NewValue;

                genderRepository.Update(existing);

                response.Messages.Add($"Gender with id {existing.Id} updated from {oldValue} to {existing.Value}.");
                response.Results.Add(existing);

                response.Status = StatusEnum.Successful;
            }

            return response.Finalize();
        }

        public ModelResponse<Model.Gender> Read(ReadGenderMultiRequest request)
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
        public ModelResponse<Model.Gender> Read(ReadGenderValueRequest request)
        {
            var response = new ModelResponse<Model.Gender>(DateTime.Now);

            try
            {
                if (request.Exact)
                    response.Results.Add(genderRepository.GetExact(request.Value));
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
        public ModelResponse<Model.Gender> Read(ReadGenderRequest request)
        {
            var response = new ModelResponse<Model.Gender>(DateTime.Now);

            try
            {
                response.Results.Add(genderRepository.Get(request.Id));

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
                genderRepository.Delete(request.Id);

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
