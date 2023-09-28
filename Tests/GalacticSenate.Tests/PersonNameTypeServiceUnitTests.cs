using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library;
using GalacticSenate.Library.PersonNameType;
using GalacticSenate.Library.PersonNameType.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Tests
{
    [TestClass]
   public class PersonNameTypeServiceUnitTests : DatabaseFixture
    {
        private readonly IPersonNameTypeService personNameTypeService;
        private readonly UnitOfWork unitOfWork;

        public PersonNameTypeServiceUnitTests() : base("DataContext")
        {
            unitOfWork = new UnitOfWork(dataContext);
            var personNameTypeRepository = unitOfWork.GetPersonNameTypeRepository();
            personNameTypeService = new PersonNameTypeService(unitOfWork);
        }
        [TestMethod]
        public async Task Add_Test()
        {
            var addResponse = await personNameTypeService.AddAsync(new AddPersonNameTypeRequest
            {
                Value = "Fourth"
            });

            Assert.IsTrue(addResponse.Status == StatusEnum.Successful);
        }
        [TestMethod]
        public async Task AddMissingValue_Test()
        {
            var request = new AddPersonNameTypeRequest
            {
                Value = ""
            };

            var response = await personNameTypeService.AddAsync(request);

            Assert.IsTrue(response.Status == StatusEnum.Failed);
            Assert.IsTrue(response.Messages.Contains("Value cannot be null. (Parameter 'Value')"));
        }
        [TestMethod]
        public async Task AddDuplicate_Test()
        {
            var request = new AddPersonNameTypeRequest
            {
                Value = "Duplicate"
            };

            var addResponse1 = await personNameTypeService.AddAsync(request);

            var addResponse2 = await personNameTypeService.AddAsync(request);

            Assert.IsTrue(addResponse1.Status == StatusEnum.Successful);
            Assert.IsTrue(addResponse2.Messages.Contains($"PersonNameType with value {request.Value} already exists."));
        }
        [TestMethod]
        public async Task ReadPaged_Test()
        {
            var readResponse = await personNameTypeService.ReadAsync(new ReadPersonNameTypeMultiRequest
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
            var readResponse = await personNameTypeService.ReadAsync(new ReadPersonNameTypeMultiRequest
            {
                PageIndex = 0,
                PageSize = int.MaxValue
            });

            Assert.IsTrue(readResponse.Status == StatusEnum.Successful);
        }
        [TestMethod]
        public async Task UpdateExisting_Test()
        {
            var getResponse = await personNameTypeService.ReadAsync(new ReadPersonNameTypeMultiRequest
            {
                PageIndex = 0,
                PageSize = 1
            });

            var request = new UpdatePersonNameTypeRequest
            {
                Id = getResponse.Results.First().Id,
                NewValue = string.Concat("updated-", getResponse.Results.First().Value)
            };

            var updateResponse = await personNameTypeService.UpdateAsync(request);

            Assert.IsTrue(updateResponse.Status == StatusEnum.Successful);
            Assert.IsTrue(updateResponse.Results.FirstOrDefault().Value == request.NewValue);
        }
        [TestMethod]
        public async Task UpdateNonExisting_Test()
        {
            var request = new UpdatePersonNameTypeRequest
            {
                Id = -1,
                NewValue = "test"
            };

            var updateResponse = await personNameTypeService.UpdateAsync(request);

            Assert.IsTrue(updateResponse.Status == StatusEnum.Failed);
            Assert.IsTrue(updateResponse.Messages.Contains($"PersonNameType with id {request.Id} does not exist."));
        }
    }
}
