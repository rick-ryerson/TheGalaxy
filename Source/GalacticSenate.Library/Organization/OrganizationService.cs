using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Organization.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Organization {
   public interface IOrganizationService {
      Task<ModelResponse<Model.Organization, AddOrganizationRequest>> AddAsync(AddOrganizationRequest request);
   }
   public class OrganizationService : IOrganizationService {
      private readonly IUnitOfWork<DataContext> unitOfWork;
      private readonly IOrganizationRepository organizationRepository;
      private readonly IPartyRepository partyRepository;
      private readonly IOrganizationNameRepository organizationNameRepository;
      private readonly IOrganizationNameValueRepository organizationNameValueRepository;

      public OrganizationService(IUnitOfWork<DataContext> unitOfWork) {
         this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
         this.organizationRepository = unitOfWork.GetOrganizationRepository();
         this.partyRepository = unitOfWork.GetPartyRepository();
         this.organizationNameRepository = unitOfWork.GetOrganizationNameRepository();
         this.organizationNameValueRepository = unitOfWork.GetOrganizationNameValueRepository();
      }
      private async Task<Model.OrganizationNameValue> AddOrganizationNameValueAsync(string value) {
         var nameValue = await organizationNameValueRepository.GetExactAsync(value);

         if (nameValue == null) {
            nameValue = await organizationNameValueRepository.AddAsync(new Model.OrganizationNameValue { Value = value });
         }

         return nameValue;
      }
      private async Task<Model.OrganizationName> AddOrganizationNameAsync(Guid organizationId, string organizationNameValue) {
         throw new NotImplementedException();
      }

      public async Task<ModelResponse<Model.Organization, AddOrganizationRequest>> AddAsync(AddOrganizationRequest request) {
         var response = new ModelResponse<Model.Organization, AddOrganizationRequest>(DateTime.Now, request);

         throw new NotImplementedException();

         return response.Finalize();
      }
   }
}
