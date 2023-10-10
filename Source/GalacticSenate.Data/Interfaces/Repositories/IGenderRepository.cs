using GalacticSenate.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
   public interface IGenderRepository {
      IEnumerable<Gender> Get(int pageIndex, int pageSize);
      Task<Gender> GetAsync(int id);
      Task<Gender> GetExactAsync(string value);
      IEnumerable<Gender> GetContains(string value);
      Task<Gender> AddAsync(Gender gender);
      void Update(Gender gender);
      Task DeleteAsync(int id);
   }
}
