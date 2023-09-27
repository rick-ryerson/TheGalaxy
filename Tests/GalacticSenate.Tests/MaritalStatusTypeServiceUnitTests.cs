using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library;
using GalacticSenate.Library.MaritalStatusType;
using GalacticSenate.Library.MaritalStatusType.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Tests {
   [TestClass]
   public class MaritalStatusTypeServiceUnitTests : GalacticSenateFixture {
      private readonly IMaritalStatusTypeService MaritalStatusTypeService;

      public MaritalStatusTypeServiceUnitTests() : base("DataContext") {
         var maritalStatusTypeRepository = unitOfWork.GetMaritalStatusTypeRepository();
         MaritalStatusTypeService = new MaritalStatusTypeService(unitOfWork, eventBusMock.Object, eventFactory, NullLogger<MaritalStatusTypeService>.Instance);
      }
      [TestMethod]
      public async Task Add_Test() {
         var addResponse = await MaritalStatusTypeService.AddAsync(new AddMaritalStatusTypeRequest
         {
            Value = "Fourth"
         });

         Assert.IsTrue(addResponse.Status == StatusEnum.Successful);
      }
      [TestMethod]
      public async Task AddMissingValue_Test() {
         var request = new AddMaritalStatusTypeRequest
         {
            Value = ""
         };

         var response = await MaritalStatusTypeService.AddAsync(request);

         Assert.IsTrue(response.Status == StatusEnum.Failed);
         Assert.IsTrue(response.Messages.Contains("Value cannot be null. (Parameter 'Value')"));
      }
      [TestMethod]
      public async Task AddDuplicate_Test() {
         var request = new AddMaritalStatusTypeRequest
         {
            Value = "Duplicate"
         };

         var addResponse1 = await MaritalStatusTypeService.AddAsync(request);

         var addResponse2 = await MaritalStatusTypeService.AddAsync(request);

         Assert.IsTrue(addResponse1.Status == StatusEnum.Successful);
         Assert.IsTrue(addResponse2.Messages.Contains($"MaritalStatusType with value {request.Value} already exists."));
      }
      [TestMethod]
      public async Task ReadPaged_Test() {
         var readResponse = await MaritalStatusTypeService.ReadAsync(new ReadMaritalStatusTypeMultiRequest
         {
            PageIndex = 0,
            PageSize = 2
         });

         Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
         Assert.IsTrue(readResponse.Results.Count == 2);
      }
      [TestMethod]
      public async Task ReadAll_Test() {
         var readResponse = await MaritalStatusTypeService.ReadAsync(new ReadMaritalStatusTypeMultiRequest
         {
            PageIndex = 0,
            PageSize = int.MaxValue
         });

         Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
      }
      [TestMethod]
      public async Task UpdateExisting_Test() {
         var getResponse = await MaritalStatusTypeService.ReadAsync(new ReadMaritalStatusTypeMultiRequest
         {
            PageIndex = 0,
            PageSize = 1
         });

         var request = new UpdateMaritalStatusTypeRequest
         {
            Id = getResponse.Results.First().Id,
            NewValue = string.Concat("updated-", getResponse.Results.First().Value)
         };

         var updateResponse = await MaritalStatusTypeService.UpdateAsync(request);

         Assert.IsTrue(updateResponse.Status == StatusEnum.Successful);
         Assert.IsTrue(updateResponse.Results.FirstOrDefault().Value == request.NewValue);
      }
      [TestMethod]
      public async Task UpdateNonExisting_Test() {
         var request = new UpdateMaritalStatusTypeRequest
         {
            Id = -1,
            NewValue = "test"
         };

         var updateResponse = await MaritalStatusTypeService.UpdateAsync(request);

         Assert.IsTrue(updateResponse.Status == StatusEnum.Failed);
         Assert.IsTrue(updateResponse.Messages.Contains($"MaritalStatusType with id {request.Id} does not exist."));
      }
   }
}
