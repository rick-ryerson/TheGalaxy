using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
    public interface IRepository<TModel, TKey> {
        IEnumerable<TModel> Get(int pageIndex, int pageSize);
        Task<TModel> GetAsync(TKey id);
        Task<TModel> AddAsync(TModel model);
        Task DeleteAsync(TModel model);
    }
}
