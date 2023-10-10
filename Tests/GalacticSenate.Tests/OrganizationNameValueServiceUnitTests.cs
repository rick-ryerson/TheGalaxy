using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library;
using GalacticSenate.Library.OrganizationNameValue;
using GalacticSenate.Library.OrganizationNameValue.Requests;
using GalacticSenate.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Tests {
   [TestClass]
   public class OrganizationNameValueServiceUnitTests : OrganizationNameValueServicesFixture {
      private readonly IOrganizationNameValueService organizationNameValueService;

      public OrganizationNameValueServiceUnitTests() : base("DataContext") {
         var organizationNameValueRepository = new OrganizationNameValueRepository(unitOfWork);
         organizationNameValueService = new OrganizationNameValueService(unitOfWork,
            organizationNameValueRepository,
            eventBusMock.Object,
            organizationNameValueEventsFactory,
            NullLogger<OrganizationNameValueService>.Instance);
      }
      [TestMethod]
      public async Task Add_Test() {
         var addResponse = await organizationNameValueService.AddAsync(new AddOrganizationNameValueRequest
         {
            Value = "Fourth"
         });

         Assert.IsTrue(addResponse.Status == StatusEnum.Successful);
      }
      [TestMethod]
      public async Task AddMissingValue_Test() {
         var request = new AddOrganizationNameValueRequest
         {
            Value = ""
         };

         var response = await organizationNameValueService.AddAsync(request);

         Assert.IsTrue(response.Status == StatusEnum.Failed);
         Assert.IsTrue(response.Messages.Contains("Value cannot be null. (Parameter 'Value')"));
      }
      [TestMethod]
      public async Task AddDuplicate_Test() {
         var request = new AddOrganizationNameValueRequest
         {
            Value = "Duplicate"
         };

         var addResponse1 = await organizationNameValueService.AddAsync(request);

         var addResponse2 = await organizationNameValueService.AddAsync(request);

         Assert.IsTrue(addResponse1.Status == StatusEnum.Successful);
         Assert.IsTrue(addResponse2.Messages.Contains($"OrganizationNameValue with value {request.Value} already exists."));
      }
      [TestMethod]
      public async Task ReadPaged_Test() {
         var readResponse = await organizationNameValueService.ReadAsync(new ReadOrganizationNameValueMultiRequest
         {
            PageIndex = 0,
            PageSize = 2
         });

         Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
         Assert.IsTrue(readResponse.Results.Count == 2);
      }
      [TestMethod]
      public async Task ReadAll_Test() {
         var readResponse = await organizationNameValueService.ReadAsync(new ReadOrganizationNameValueMultiRequest
         {
            PageIndex = 0,
            PageSize = int.MaxValue
         });

         Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
      }
      [TestMethod]
      public async Task UpdateExisting_Test() {
         var getResponse = await organizationNameValueService.ReadAsync(new ReadOrganizationNameValueMultiRequest
         {
            PageIndex = 0,
            PageSize = 1
         });

         var request = new UpdateOrganizationNameValueRequest
         {
            Id = getResponse.Results.First().Id,
            NewValue = string.Concat("updated-", getResponse.Results.First().Value)
         };

         var updateResponse = await organizationNameValueService.UpdateAsync(request);

         Assert.IsTrue(updateResponse.Status == StatusEnum.Successful);
         Assert.IsTrue(updateResponse.Results.FirstOrDefault().Value == request.NewValue);
      }
      [TestMethod]
      public async Task UpdateNonExisting_Test() {
         var request = new UpdateOrganizationNameValueRequest
         {
            Id = -1,
            NewValue = "test"
         };

         var updateResponse = await organizationNameValueService.UpdateAsync(request);

         Assert.IsTrue(updateResponse.Status == StatusEnum.Failed);
         Assert.IsTrue(updateResponse.Messages.Contains($"OrganizationNameValue with id {request.Id} does not exist."));
      }
   }
}
