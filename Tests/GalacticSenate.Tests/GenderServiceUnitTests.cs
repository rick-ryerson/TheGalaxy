using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library;
using GalacticSenate.Library.Gender;
using GalacticSenate.Library.Gender.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GalacticSenate.Tests
{
    [TestClass]
    public class GenderServiceUnitTests : DatabaseFixture
    {
        private readonly IGenderService genderService;
        private readonly UnitOfWork<DataContext> unitOfWork;

        public GenderServiceUnitTests() : base("DataContext")
        {
            unitOfWork = new UnitOfWork<DataContext>(dataContext);
            var genderRepository = RepositoryFactory.GenderRepository(unitOfWork);
            genderService = new GenderService(unitOfWork, genderRepository);
        }
        [TestMethod]
        public void Add_Test()
        {
            var addResponse = genderService.Add(new AddGenderRequest
            {
                Value = "Fourth"
            });

            Assert.IsTrue(addResponse.Status == StatusEnum.Successful);
        }
        [TestMethod]
        public void AddMissingValue_Test()
        {
            var request = new AddGenderRequest
            {
                Value = ""
            };

            var response = genderService.Add(request);

            Assert.IsTrue(response.Status == StatusEnum.Failed);
            Assert.IsTrue(response.Messages.Contains("Value cannot be null. (Parameter 'Value')"));
        }
        [TestMethod]
        public void AddDuplicate_Test()
        {
            var request = new AddGenderRequest
            {
                Value = "Duplicate"
            };

            var addResponse1 = genderService.Add(request);

            var addResponse2 = genderService.Add(request);

            Assert.IsTrue(addResponse1.Status == StatusEnum.Successful);
            Assert.IsTrue(addResponse2.Messages.Contains($"Gender with value {request.Value} already exists."));
        }
        [TestMethod]
        public void ReadPaged_Test()
        {
            var readResponse = genderService.Read(new ReadGenderMultiRequest
            {
                PageIndex = 0,
                PageSize = 2
            });

            Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
            Assert.IsTrue(readResponse.Results.Count == 2);
        }
        [TestMethod]
        public void ReadAll_Test()
        {
            var readResponse = genderService.Read(new ReadGenderMultiRequest
            {
                PageIndex = 0,
                PageSize = int.MaxValue
            });

            Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
        }
        [TestMethod]
        public void UpdateExisting_Test()
        {
            var getResponse = genderService.Read(new ReadGenderMultiRequest
            {
                PageIndex = 0,
                PageSize = 1
            });

            var request = new UpdateGenderRequest
            {
                Id = getResponse.Results.First().Id,
                NewValue = string.Concat("updated-", getResponse.Results.First().Value)
            };

            var updateResponse = genderService.Update(request);

            Assert.IsTrue(updateResponse.Status == StatusEnum.Successful);
            Assert.IsTrue(updateResponse.Results.FirstOrDefault().Value == request.NewValue);
        }
        [TestMethod]
        public void UpdateNonExisting_Test()
        {
            var request = new UpdateGenderRequest
            {
                Id = -1,
                NewValue = "test"
            };

            var updateResponse = genderService.Update(request);

            Assert.IsTrue(updateResponse.Status == StatusEnum.Failed);
            Assert.IsTrue(updateResponse.Messages.Contains($"Gender with id {request.Id} does not exist."));
        }
    }
}
