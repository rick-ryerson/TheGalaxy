using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Model;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Requests;
using GalacticSenate.Library.Services.OrganizationNameValue;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services {
    public interface IPartyService {
        Task<ModelResponse<Party, AddPartyRequest>> AddAsync(AddPartyRequest request);
        Task<BasicResponse<DeletePartyRequest>> DeleteAsync(DeletePartyRequest request);
        Task<ModelResponse<Party, ReadPartyMultiRequest>> ReadAsync(ReadPartyMultiRequest request);
        Task<ModelResponse<Party, ReadPartyRequest>> ReadAsync(ReadPartyRequest request);
    }

    public class PartyService : BasicServiceBase, IPartyService {
        protected readonly IPartyRepository partyRepository;
        protected readonly IEventsFactory eventsFactory;

        public PartyService(IUnitOfWork<DataContext> unitOfWork,
           IPartyRepository partyRepository,
           IEventBus eventBus,
           IEventsFactory eventsFactory,
           ILogger logger) : base(unitOfWork, eventBus, logger) {
            this.partyRepository = partyRepository ?? throw new ArgumentNullException(nameof(partyRepository));
            this.eventsFactory = eventsFactory ?? throw new ArgumentNullException(nameof(eventsFactory));
        }

        public async Task<ModelResponse<Party, AddPartyRequest>> AddAsync(AddPartyRequest request) {
            var response = new ModelResponse<Party, AddPartyRequest>(DateTime.Now, request);

            try {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                var party = await partyRepository.GetAsync(request.Id);

                if (party is null) {
                    party = await partyRepository.AddAsync(new Party { Id = request.Id });

                    unitOfWork.Save();

                    eventBus.Publish(eventsFactory.Created(party));

                    response.Messages.Add($"Party with id {request.Id} added.");
                } else {
                    response.Messages.Add($"Party with id {request.Id} already exists.");
                }

                response.Results.Add(party);

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Status = StatusEnum.Failed;
                response.Messages.Add(ex.Message);
            }

            return response.Finalize();
        }
        public async Task<ModelResponse<Party, ReadPartyMultiRequest>> ReadAsync(ReadPartyMultiRequest request) {
            var response = new ModelResponse<Party, ReadPartyMultiRequest>(DateTime.Now, request);

            try {
                response.Results.AddRange(partyRepository.Get(request.PageIndex, request.PageSize));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }

            return await Task.Run(() => { return response.Finalize(); });
        }
        public async Task<ModelResponse<Party, ReadPartyRequest>> ReadAsync(ReadPartyRequest request) {
            var response = new ModelResponse<Party, ReadPartyRequest>(DateTime.Now, request);

            try {
                var party = await partyRepository.GetAsync(request.Id);

                if (party != null)
                    response.Results.Add(await partyRepository.GetAsync(request.Id));

                response.Status = StatusEnum.Successful;
            }
            catch (Exception ex) {
                response.Messages.Add(ex.Message);
                response.Status = StatusEnum.Failed;
            }
            return response.Finalize();
        }

        public async Task<BasicResponse<DeletePartyRequest>> DeleteAsync(DeletePartyRequest request) {
            var response = new BasicResponse<DeletePartyRequest>(DateTime.Now, request);

            try {
                var party = await partyRepository.GetAsync(request.Id);
                await partyRepository.DeleteAsync(party);
                unitOfWork.Save();

                eventBus.Publish(eventsFactory.Deleted(request.Id));
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
