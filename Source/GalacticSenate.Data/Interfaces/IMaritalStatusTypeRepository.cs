using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces
{
    public interface IMaritalStatusTypeRepository
    {
        IEnumerable<MaritalStatusType> Get(int pageIndex, int pageSize);
        Task<MaritalStatusType> GetAsync(int id);
        Task<MaritalStatusType> GetExactAsync(string value);
        IEnumerable<MaritalStatusType> GetContains(string value);
        Task<MaritalStatusType> AddAsync(MaritalStatusType gender);
        void Update(MaritalStatusType gender);
        Task DeleteAsync(int id);
    }
}
