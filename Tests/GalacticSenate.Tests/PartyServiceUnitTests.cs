using GalacticSenate.Library;
using GalacticSenate.Library.Requests;
using GalacticSenate.Library.Services;
using GalacticSenate.Tests.Properties;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Tests {
    [TestClass]
    public class PartyServiceUnitTests : PartyServiceFixture {
        private readonly PartyService partyService;

        public PartyServiceUnitTests() : base("DataContext") {
            partyService = new PartyService(
                unitOfWork,
                partyRepository,
                eventBusMock.Object,
                eventsFactory,
                NullLogger<PartyService>.Instance);
        }

        [TestMethod]
        public async Task Add_Test() {
            var addResponse = await partyService.AddAsync(new AddPartyRequest
            {
                Id = Guid.NewGuid(),
            });
            Assert.IsTrue(addResponse.Status == StatusEnum.Successful);
        }
    }
}
