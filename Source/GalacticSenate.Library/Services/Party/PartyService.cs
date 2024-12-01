using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Model;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Requests;
using GalacticSenate.Library.Services.OrganizationNameValue;
using GalacticSenate.Library.Services.Party.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.Party {
    public interface IPartyService {
        Task<ModelResponse<Model.Party, AddPartyRequest>> AddAsync(AddPartyRequest request);
        Task<BasicResponse<DeletePartyRequest>> DeleteAsync(DeletePartyRequest request);
        Task<ModelResponse<Model.Party, ReadPartyMultiRequest>> ReadAsync(ReadPartyMultiRequest request);
        Task<ModelResponse<Model.Party, ReadPartyRequest>> ReadAsync(ReadPartyRequest request);
    }

    public class PartyService : BasicServiceBase, IPartyService {
        private readonly IPartyRepository partyRepository;
        private readonly IEventsFactory<Model.Party, Guid> partyEventsFactory;

        public PartyService(IUnitOfWork<DataContext> unitOfWork,
           IPartyRepository partyRepository,
           IEventBus eventBus,
           IEventsFactory<Model.Party, Guid> partyEventsFactory,
           ILogger logger) : base(unitOfWork, eventBus, logger) {
            this.partyRepository = partyRepository ?? throw new ArgumentNullException(nameof(partyRepository));
            this.partyEventsFactory = partyEventsFactory ?? throw new ArgumentNullException(nameof(partyEventsFactory));
        }

        public async Task<ModelResponse<Model.Party, AddPartyRequest>> AddAsync(AddPartyRequest request) {
            var response = new ModelResponse<Model.Party, AddPartyRequest>(DateTime.Now, request);

            try {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                var party = await partyRepository.GetAsync(request.Id);

                if (party is null) {
                    party = await partyRepository.AddAsync(new Model.Party { Id = request.Id });

                    unitOfWork.Save();

                    eventBus.Publish(partyEventsFactory.Created(party));

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
        public async Task<ModelResponse<Model.Party, ReadPartyMultiRequest>> ReadAsync(ReadPartyMultiRequest request) {
            var response = new ModelResponse<Model.Party, ReadPartyMultiRequest>(DateTime.Now, request);

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
        public async Task<ModelResponse<Model.Party, ReadPartyRequest>> ReadAsync(ReadPartyRequest request) {
            var response = new ModelResponse<Model.Party, ReadPartyRequest>(DateTime.Now, request);

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
                await partyRepository.DeleteAsync(request.Id);
                unitOfWork.Save();

                eventBus.Publish(partyEventsFactory.Deleted(request.Id));
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
