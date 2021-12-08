using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces {
   public interface IPersonNameTypeRepository {
      IEnumerable<PersonNameType> Get(int pageIndex, int pageSize);
      Task<PersonNameType> GetAsync(int id);
      Task<PersonNameType> GetExactAsync(string value);
      IEnumerable<PersonNameType> GetContains(string value);
      Task<PersonNameType> AddAsync(PersonNameType gender);
      void Update(PersonNameType gender);
      Task DeleteAsync(int id);
   }
}
