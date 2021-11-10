using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library;
using GalacticSenate.Library.Gender;
using GalacticSenate.Library.Gender.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Tests
{
    [TestClass]
    public class GenderServiceUnitTests : DatabaseFixture
    {
        private readonly IGenderService genderService;
        private readonly UnitOfWork unitOfWork;

        public GenderServiceUnitTests() : base("DataContext")
        {
            unitOfWork = new UnitOfWork(dataContext);
            var genderRepository = unitOfWork.GetGenderRepository();
            genderService = new GenderService(unitOfWork);
        }
        [TestMethod]
        public async Task Add_Test()
        {
            var addResponse = await genderService.AddAsync(new AddGenderRequest
            {
                Value = "Fourth"
            });

            Assert.IsTrue(addResponse.Status == StatusEnum.Successful);
        }
        [TestMethod]
        public async Task AddMissingValue_Test()
        {
            var request = new AddGenderRequest
            {
                Value = ""
            };

            var response = await genderService.AddAsync(request);

            Assert.IsTrue(response.Status == StatusEnum.Failed);
            Assert.IsTrue(response.Messages.Contains("Value cannot be null. (Parameter 'Value')"));
        }
        [TestMethod]
        public async Task AddDuplicate_Test()
        {
            var request = new AddGenderRequest
            {
                Value = "Duplicate"
            };

            var addResponse1 = await genderService.AddAsync(request);

            var addResponse2 = await genderService.AddAsync(request);

            Assert.IsTrue(addResponse1.Status == StatusEnum.Successful);
            Assert.IsTrue(addResponse2.Messages.Contains($"Gender with value {request.Value} already exists."));
        }
        [TestMethod]
        public async Task ReadPaged_Test()
        {
            var readResponse = await genderService.ReadAsync(new ReadGenderMultiRequest
            {
                PageIndex = 0,
                PageSize = 2
            });

            Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
            Assert.IsTrue(readResponse.Results.Count == 2);
        }
        [TestMethod]
        public async Task ReadAll_Test()
        {
            var readResponse = await genderService.ReadAsync(new ReadGenderMultiRequest
            {
                PageIndex = 0,
                PageSize = int.MaxValue
            });

            Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
        }
        [TestMethod]
        public async Task UpdateExisting_Test()
        {
            var getResponse = await genderService.ReadAsync(new ReadGenderMultiRequest
            {
                PageIndex = 0,
                PageSize = 1
            });

            var request = new UpdateGenderRequest
            {
                Id = getResponse.Results.First().Id,
                NewValue = string.Concat("updated-", getResponse.Results.First().Value)
            };

            var updateResponse = await genderService.UpdateAsync(request);

            Assert.IsTrue(updateResponse.Status == StatusEnum.Successful);
            Assert.IsTrue(updateResponse.Results.FirstOrDefault().Value == request.NewValue);
        }
        [TestMethod]
        public async Task UpdateNonExisting_Test()
        {
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
