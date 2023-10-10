using GalacticSenate.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
   public interface IPersonNameValueRepository {
      IEnumerable<PersonNameValue> Get(int pageIndex, int pageSize);
      Task<PersonNameValue> GetAsync(int id);
      Task<PersonNameValue> GetExactAsync(string value);
      IEnumerable<PersonNameValue> GetContains(string value);
      Task<PersonNameValue> AddAsync(PersonNameValue gender);
      void Update(PersonNameValue gender);
      Task DeleteAsync(int id);
   }
}
