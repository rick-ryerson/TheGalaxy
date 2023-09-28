using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library;
using GalacticSenate.Library.PersonNameValue;
using GalacticSenate.Library.PersonNameValue.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Tests
{
    [TestClass]
   public class PersonNameValueServiceUnitTests : DatabaseFixture
    {
        private readonly IPersonNameValueService personNameValueService;
        private readonly UnitOfWork unitOfWork;

        public PersonNameValueServiceUnitTests() : base("DataContext")
        {
            unitOfWork = new UnitOfWork(dataContext);
            var genderRepository = unitOfWork.GetPersonNameValueRepository();
            personNameValueService = new PersonNameValueService(unitOfWork);
        }
        [TestMethod]
        public async Task Add_Test()
        {
            var addResponse = await personNameValueService.AddAsync(new AddPersonNameValueRequest
            {
                Value = "Fourth"
            });

            Assert.IsTrue(addResponse.Status == StatusEnum.Successful);
        }
        [TestMethod]
        public async Task AddMissingValue_Test()
        {
            var request = new AddPersonNameValueRequest
            {
                Value = ""
            };

            var response = await personNameValueService.AddAsync(request);

            Assert.IsTrue(response.Status == StatusEnum.Failed);
            Assert.IsTrue(response.Messages.Contains("Value cannot be null. (Parameter 'Value')"));
        }
        [TestMethod]
        public async Task AddDuplicate_Test()
        {
            var request = new AddPersonNameValueRequest
            {
                Value = "Duplicate"
            };

            var addResponse1 = await personNameValueService.AddAsync(request);

            var addResponse2 = await personNameValueService.AddAsync(request);

            Assert.IsTrue(addResponse1.Status == StatusEnum.Successful);
            Assert.IsTrue(addResponse2.Messages.Contains($"PersonNameValue with value {request.Value} already exists."));
        }
        [TestMethod]
        public async Task ReadPaged_Test()
        {
            var readResponse = await personNameValueService.ReadAsync(new ReadPersonNameValueMultiRequest
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
            var readResponse = await personNameValueService.ReadAsync(new ReadPersonNameValueMultiRequest
            {
                PageIndex = 0,
                PageSize = int.MaxValue
            });

            Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
        }
        [TestMethod]
        public async Task UpdateExisting_Test()
        {
            var getResponse = await personNameValueService.ReadAsync(new ReadPersonNameValueMultiRequest
            {
                PageIndex = 0,
                PageSize = 1
            });

            var request = new UpdatePersonNameValueRequest
            {
                Id = getResponse.Results.First().Id,
                NewValue = string.Concat("updated-", getResponse.Results.First().Value)
            };

            var updateResponse = await personNameValueService.UpdateAsync(request);

            Assert.IsTrue(updateResponse.Status == StatusEnum.Successful);
            Assert.IsTrue(updateResponse.Results.FirstOrDefault().Value == request.NewValue);
        }
        [TestMethod]
        public async Task UpdateNonExisting_Test()
        {
            var request = new UpdatePersonNameValueRequest
            {
                Id = -1,
                NewValue = "test"
            };

            var updateResponse = await personNameValueService.UpdateAsync(request);

            Assert.IsTrue(updateResponse.Status == StatusEnum.Failed);
            Assert.IsTrue(updateResponse.Messages.Contains($"PersonNameValue with id {request.Id} does not exist."));
        }
    }
}
