using Microsoft.EntityFrameworkCore;

namespace GalacticSenate.Data.Interfaces {
   public interface IUnitOfWork<out TContext> where TContext : DbContext
    {
        TContext Context { get; }

        void Begin();
        void Commit();
        void Rollback();
        void Save();
    }
}
