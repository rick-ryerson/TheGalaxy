using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library;
using GalacticSenate.Library.Gender;
using GalacticSenate.Library.Gender.Requests;
using GalacticSenate.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Tests
{
    [TestClass]
   public class GenderServiceUnitTests : GenderServicesFixture {
      private readonly IGenderService genderService;

      public GenderServiceUnitTests() : base("DataContext") {
         genderService = new GenderService(unitOfWork, genderRepository, eventBusMock.Object, genderEventsFactory, NullLogger<GenderService>.Instance);

      }
      [TestMethod]
      public async Task Add_Test() {
         var addResponse = await genderService.AddAsync(new AddGenderRequest
         {
            Value = "Another"
         });

         Assert.IsTrue(addResponse.Status == StatusEnum.Successful);
      }

      [TestMethod]
      public void AddMissingValue_Test() {
         var request = new AddGenderRequest
         {
            Value = ""
         };

         Assert.ThrowsException<ArgumentNullException>(() => (genderService.AddAsync(request)).GetAwaiter().GetResult());
      }
      [TestMethod]
      public async Task AddDuplicate_Test() {
         var request = new AddGenderRequest
         {
            Value = "Male"
         };

         var response = await genderService.AddAsync(request);

         Assert.IsTrue(response.Status == StatusEnum.Successful);
         Assert.IsTrue(response.Messages.Contains($"Gender with value {request.Value} already exists."));
      }
      [TestMethod]
      public async Task ReadPaged_Test() {
         await genderService.AddAsync(new AddGenderRequest { Value = "Paged 1" });
         await genderService.AddAsync(new AddGenderRequest { Value = "Paged 2" });
         await genderService.AddAsync(new AddGenderRequest { Value = "Paged 3" });
         await genderService.AddAsync(new AddGenderRequest { Value = "Paged 4" });
         await genderService.AddAsync(new AddGenderRequest { Value = "Paged 5" });

         var response = await genderService.ReadAsync(new ReadGenderMultiRequest
         {
            PageIndex = 0,
            PageSize = 5
         });

         Assert.IsTrue(response.Status == StatusEnum.Successful);
         Assert.IsTrue(response.Results.Count == 5);
      }
      [TestMethod]
      public async Task ReadAll_Test() {
         var readResponse = await genderService.ReadAsync(new ReadGenderMultiRequest
         {
            PageIndex = 0,
            PageSize = int.MaxValue
         });

         Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
         Assert.IsTrue(readResponse?.Results.Count > 1);
      }
      [TestMethod]
      public async Task ReadExactExists_Test() {
         var response = await genderService.ReadAsync(new ReadGenderValueRequest { Exact = true, Value = "Male" });

         Assert.IsTrue(response.Status == StatusEnum.Successful);
         Assert.IsTrue(response.Results.Count == 1);
      }
      [TestMethod]
      public async Task ReadExactNotExists_Test() {

         var response2 = await genderService.ReadAsync(new ReadGenderValueRequest { Exact = true, Value = "Mail" });

         Assert.IsTrue(response2.Status == StatusEnum.Successful);
         Assert.IsTrue(response2.Results.Count == 0);
      }
      [TestMethod]
      public async Task UpdateExisting_Test() {
         var addResponse = await genderService.AddAsync(new AddGenderRequest { Value = "ToUpdate" });

         var request = new UpdateGenderRequest
         {
            Id = addResponse.Results.First().Id,
            NewValue = string.Concat("updated-", addResponse.Results.First().Value)
         };

         var updateResponse = await genderService.UpdateAsync(request);

         Assert.IsTrue(updateResponse.Status == StatusEnum.Successful);
         Assert.IsTrue(updateResponse.Results.FirstOrDefault().Value == request.NewValue);
      }
      [TestMethod]
      public async Task UpdateNonExisting_Test() {
         var request = new UpdateGenderRequest
         {
            Id = -1,
            NewValue = "test"
         };

         var updateResponse = await genderService.UpdateAsync(request);

         Assert.IsTrue(updateResponse.Status == StatusEnum.Failed);
         Assert.IsTrue(updateResponse.Messages.Contains($"Gender with id {request.Id} does not exist."));
      }
   }
}
