using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
   public interface IPartyRepository {
      IEnumerable<Party> Get(int pageIndex, int pageSize);
      Task<Party> GetAsync(Guid id);
      Task<Party> AddAsync(Party party);
      Task DeleteAsync(Guid id);
   }
}
