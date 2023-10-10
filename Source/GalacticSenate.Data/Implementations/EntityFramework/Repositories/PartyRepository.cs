using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Implementations.EntityFramework.Repositories {
   internal class PartyRepository : IPartyRepository {
      private readonly IUnitOfWork<DataContext> unitOfWork;

      public PartyRepository(IUnitOfWork<DataContext> unitOfWork) {
         this.unitOfWork = unitOfWork;
      }

      public async Task<Party> AddAsync(Party party) {
         await unitOfWork.Context.Parties.AddAsync(party);

         return party;
      }
      public async Task DeleteAsync(Guid id) {
         var party = await GetAsync(id);

         if (party != null)
            throw new DeleteException($"Party with id {id} does not exist.");

         unitOfWork.Context.Parties.Remove(party);
      }

      public IEnumerable<Party> Get(int pageIndex, int pageSize) {
         return unitOfWork
            .Context
            .Parties
            .OrderBy(g => g.Id)
            .Skip(pageSize * pageIndex)
            .Take(pageSize);
      }

      public async Task<Party> GetAsync(Guid id) {
         return await unitOfWork
            .Context
            .Parties
            .FindAsync(id);
      }
   }
}
