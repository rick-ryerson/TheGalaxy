using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Domain.Model;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Gender.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Gender {
   public interface IGenderService {
      Task<ModelResponse<Model.Gender, AddGenderRequest>> AddAsync(AddGenderRequest request);
      Task<BasicResponse<DeleteGenderRequest>> DeleteAsync(DeleteGenderRequest request);
      Task<ModelResponse<Model.Gender, ReadGenderMultiRequest>> ReadAsync(ReadGenderMultiRequest request);
      Task<ModelResponse<Model.Gender, ReadGenderRequest>> ReadAsync(ReadGenderRequest request);
      Task<ModelResponse<Model.Gender, ReadGenderValueRequest>> ReadAsync(ReadGenderValueRequest request);
      Task<ModelResponse<Model.Gender, UpdateGenderRequest>> UpdateAsync(UpdateGenderRequest request);
   }

   public class GenderService : IGenderService {
      private readonly IUnitOfWork<DataContext> unitOfWork;
      private readonly IEventBus eventBus;
      private readonly IEventFactory eventFactory;
      private readonly IGenderRepository genderRepository;

      public GenderService(IUnitOfWork<DataContext> unitOfWork, IEventBus eventBus, IEventFactory eventFactory) {
         this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
         this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
         this.eventFactory = eventFactory ?? throw new ArgumentNullException(nameof(eventFactory));
         this.genderRepository = unitOfWork.GetGenderRepository() ?? throw new ApplicationException("Couldn't create gender repository");
      }

      public async Task<ModelResponse<Model.Gender, AddGenderRequest>> AddAsync(AddGenderRequest request) {
         var response = new ModelResponse<Model.Gender, AddGenderRequest>(DateTime.Now, request);

         if (request is null)
            throw new ArgumentNullException(nameof(request));
         if (string.IsNullOrEmpty(request.Value))
            throw new ArgumentNullException(nameof(request.Value));

         try {
            var item = await genderRepository.GetExactAsync(request.Value);

            if (item is null) {
               item = await genderRepository.AddAsync(new Model.Gender { Value = request.Value });
               unitOfWork.Save();

               eventBus.Publish(eventFactory.CreateCreated<Model.Gender>(item));

               response.Messages.Add($"Gender with value {request.Value} added.");
            } else {
               response.Messages.Add($"Gender with value {request.Value} already exists.");
            }

            response.Results.Add(item);

            response.Status = StatusEnum.Successful;
         }
         catch (Exception ex) {
            response.Status = StatusEnum.Failed;
            response.Messages.Add(ex.Message);
         }

         return response.Finalize();
      }
      public async Task<ModelResponse<Model.Gender, UpdateGenderRequest>> UpdateAsync(UpdateGenderRequest request) {
         if (request is null)
            throw new ArgumentNullException(nameof(request));
         if (string.IsNullOrEmpty(request.NewValue))
            throw new ArgumentNullException(nameof(request.NewValue));

         var response = new ModelResponse<Model.Gender, UpdateGenderRequest>(DateTime.Now, request);

         Model.Gender existing = null;

         try {
            existing = await genderRepository.GetAsync(request.Id);
         }
         catch (Exception ex) {
            response.Messages.Add(ex.Message);
            response.Status = StatusEnum.Failed;
         }

         if (existing is null) {
            response.Messages.Add($"Gender with id {request.Id} does not exist.");
            response.Status = StatusEnum.Failed;
         } else {
            try {
               var oldObject = existing;
               var newObject = oldObject;

               if (oldObject.Value == request.NewValue) {
                  response.Messages.Add($"Gender with id {newObject.Id} already has a value of {oldObject.Value}.");
               } else {
                  newObject.Value = request.NewValue;

                  genderRepository.Update(newObject);
                  unitOfWork.Save();

                  eventBus.Publish(eventFactory.CreateUpdated(oldObject, newObject));
                  response.Messages.Add($"Gender with id {newObject.Id} updated from {oldObject.Value} to {newObject.Value}.");
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

      public async Task<ModelResponse<Model.Gender, ReadGenderMultiRequest>> ReadAsync(ReadGenderMultiRequest request) {
         var response = new ModelResponse<Model.Gender, ReadGenderMultiRequest>(DateTime.Now, request);

         try {
            response.Results.AddRange(genderRepository.Get(request.PageIndex, request.PageSize));

            response.Status = StatusEnum.Successful;
         }
         catch (Exception ex) {
            response.Messages.Add(ex.Message);
            response.Status = StatusEnum.Failed;
         }

         return await Task.Run(() => { return response.Finalize(); });
      }
      public async Task<ModelResponse<Model.Gender, ReadGenderValueRequest>> ReadAsync(ReadGenderValueRequest request) {
         var response = new ModelResponse<Model.Gender, ReadGenderValueRequest>(DateTime.Now, request);

         try {
            if (request.Exact) {
               var r = await genderRepository.GetExactAsync(request.Value);

               if (r != null) {
                  response.Results.Add(r);
               }
            } else
               response.Results.AddRange(genderRepository.GetContains(request.Value));

            response.Status = StatusEnum.Successful;
         }
         catch (Exception ex) {
            response.Messages.Add(ex.Message);
            response.Status = StatusEnum.Failed;
         }
         return response.Finalize();
      }
      public async Task<ModelResponse<Model.Gender, ReadGenderRequest>> ReadAsync(ReadGenderRequest request) {
         var response = new ModelResponse<Model.Gender, ReadGenderRequest>(DateTime.Now, request);

         try {
            var gender = await genderRepository.GetAsync(request.Id);

            if (gender != null)
               response.Results.Add(await genderRepository.GetAsync(request.Id));

            response.Status = StatusEnum.Successful;
         }
         catch (Exception ex) {
            response.Messages.Add(ex.Message);
            response.Status = StatusEnum.Failed;
         }
         return response.Finalize();
      }

      public async Task<BasicResponse<DeleteGenderRequest>> DeleteAsync(DeleteGenderRequest request) {
         var response = new BasicResponse<DeleteGenderRequest>(DateTime.Now, request);

         try {
            await genderRepository.DeleteAsync(request.Id);
            unitOfWork.Save();


            eventBus.Publish(eventFactory.CreateDeleted(request.Id));

            response.Status = StatusEnum.Successful;
         }
         catch (Exception ex) {
            response.Status = StatusEnum.Failed;
            response.Messages.Add(ex.Message);
         }

         return response.Finalize();
      }
   }
}
