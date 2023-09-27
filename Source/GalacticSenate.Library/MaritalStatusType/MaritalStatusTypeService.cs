using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.MaritalStatusType.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.MaritalStatusType {
   public interface IMaritalStatusTypeService {
      Task<ModelResponse<Model.MaritalStatusType, AddMaritalStatusTypeRequest>> AddAsync(AddMaritalStatusTypeRequest request);
      Task<BasicResponse<DeleteMaritalStatusTypeRequest>> DeleteAsync(DeleteMaritalStatusTypeRequest request);
      Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeMultiRequest>> ReadAsync(ReadMaritalStatusTypeMultiRequest request);
      Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeRequest>> ReadAsync(ReadMaritalStatusTypeRequest request);
      Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeValueRequest>> ReadAsync(ReadMaritalStatusTypeValueRequest request);
      Task<ModelResponse<Model.MaritalStatusType, UpdateMaritalStatusTypeRequest>> UpdateAsync(UpdateMaritalStatusTypeRequest request);
   }

   public class MaritalStatusTypeService : IMaritalStatusTypeService {
      private readonly IUnitOfWork<DataContext> unitOfWork;
      private readonly IEventBus eventBus;
      private readonly IEventFactory eventFactory;
      private readonly IMaritalStatusTypeRepository maritalStatusTypeRepository;

      public MaritalStatusTypeService(IUnitOfWork<DataContext> unitOfWork, IEventBus eventBus, IEventFactory eventFactory) {
         this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
         this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
         this.eventFactory = eventFactory ?? throw new ArgumentNullException(nameof(eventFactory));
         this.maritalStatusTypeRepository = unitOfWork.GetMaritalStatusTypeRepository() ?? throw new ApplicationException("Couldn't create maritalStatusType repository");
      }

      public async Task<ModelResponse<Model.MaritalStatusType, AddMaritalStatusTypeRequest>> AddAsync(AddMaritalStatusTypeRequest request) {
         var response = new ModelResponse<Model.MaritalStatusType, AddMaritalStatusTypeRequest>(DateTime.Now, request);

         var existing = await maritalStatusTypeRepository.GetExactAsync(request.Value);

         try {
            if (request is null)
               throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.Value))
               throw new ArgumentNullException(nameof(request.Value));

            if (existing is null) {
               existing = await maritalStatusTypeRepository.AddAsync(new Model.MaritalStatusType { Value = request.Value });
               unitOfWork.Save();

               eventBus.Publish(eventFactory.CreateCreated(existing));

               response.Messages.Add($"MaritalStatusType with value {request.Value} added.");
            } else {
               response.Messages.Add($"MaritalStatusType with value {request.Value} already exists.");
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
      public async Task<ModelResponse<Model.MaritalStatusType, UpdateMaritalStatusTypeRequest>> UpdateAsync(UpdateMaritalStatusTypeRequest request) {
         if (request is null)
            throw new ArgumentNullException(nameof(request));
         if (string.IsNullOrEmpty(request.NewValue))
            throw new ArgumentNullException(nameof(request.NewValue));

         var response = new ModelResponse<Model.MaritalStatusType, UpdateMaritalStatusTypeRequest>(DateTime.Now, request);

         Model.MaritalStatusType existingItem = null;

         try {
            existingItem = await maritalStatusTypeRepository.GetAsync(request.Id);
         }
         catch (Exception ex) {
            response.Messages.Add(ex.Message);
            response.Status = StatusEnum.Failed;
         }

         if (existingItem is null) {
            response.Messages.Add($"MaritalStatusType with id {request.Id} does not exist.");
            response.Status = StatusEnum.Failed;
         } else {
            try {
               var oldValue = existingItem.Value;
               var newItem = existingItem;

               if (oldValue == request.NewValue) {
                  response.Messages.Add($"MaritalStatusType with id {existingItem.Id} already has a value of {oldValue}.");
               } else {
                  newItem.Value = request.NewValue;

                  maritalStatusTypeRepository.Update(newItem);
                  unitOfWork.Save();

                  eventBus.Publish(eventFactory.CreateUpdated(newItem, existingItem));
                  response.Messages.Add($"MaritalStatusType with id {existingItem.Id} updated from {oldValue} to {existingItem.Value}.");
               }

               response.Results.Add(existingItem);

               response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
               response.Messages.Add(ex.Message);
               response.Status = StatusEnum.Failed;
            }
         }

         return response.Finalize();
      }

      public async Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeMultiRequest>> ReadAsync(ReadMaritalStatusTypeMultiRequest request) {
         var response = new ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeMultiRequest>(DateTime.Now, request);

         try {
            response.Results.AddRange(maritalStatusTypeRepository.Get(request.PageIndex, request.PageSize));

            response.Status = StatusEnum.Successful;
         }
         catch (Exception ex) {
            response.Messages.Add(ex.Message);
            response.Status = StatusEnum.Failed;
         }

         return await Task.Run(() => { return response.Finalize(); });
      }
      public async Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeValueRequest>> ReadAsync(ReadMaritalStatusTypeValueRequest request) {
         var response = new ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeValueRequest>(DateTime.Now, request);

         try {
            if (request.Exact)
               response.Results.Add(await maritalStatusTypeRepository.GetExactAsync(request.Value));
            else
               response.Results.AddRange(maritalStatusTypeRepository.GetContains(request.Value));

            response.Status = StatusEnum.Successful;
         }
         catch (Exception ex) {
            response.Messages.Add(ex.Message);
            response.Status = StatusEnum.Failed;
         }
         return response.Finalize();
      }
      public async Task<ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeRequest>> ReadAsync(ReadMaritalStatusTypeRequest request) {
         var response = new ModelResponse<Model.MaritalStatusType, ReadMaritalStatusTypeRequest>(DateTime.Now, request);

         try {
            var maritalStatus = await maritalStatusTypeRepository.GetAsync(request.Id);

            if (maritalStatus != null)
               response.Results.Add(maritalStatus);

            response.Status = StatusEnum.Successful;
         }
         catch (Exception ex) {
            response.Messages.Add(ex.Message);
            response.Status = StatusEnum.Failed;
         }
         return response.Finalize();
      }

      public async Task<BasicResponse<DeleteMaritalStatusTypeRequest>> DeleteAsync(DeleteMaritalStatusTypeRequest request) {
         var response = new BasicResponse<DeleteMaritalStatusTypeRequest>(DateTime.Now, request);

         try {
            await maritalStatusTypeRepository.DeleteAsync(request.Id);
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
