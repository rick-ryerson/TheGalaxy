using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces
{
    public interface IGenderRepository
    {
        IEnumerable<Gender> Get(int pageIndex, int pageSize);
        Task<Gender> GetAsync(int id);
        Task<Gender> GetExactAsync(string value);
        IEnumerable<Gender> GetContains(string value);
        Gender Add(Gender gender);
        void Update(Gender gender);
        Task DeleteAsync(int id);
    }
}
