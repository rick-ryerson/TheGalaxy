using GalacticSenate.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
    public interface IGenderRepository : IRepository<Gender, int> {
        Task<Gender> GetExactAsync(string value);
        IEnumerable<Gender> GetContains(string value);
    }
}
