using GalacticSenate.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Data.Implementations.EntityFramework
{
    public static class RepositoryFactory
    {
        public static IGenderRepository GenderRepository(IUnitOfWork<DataContext> unitOfWork)
        {
            return new GenderRepository(unitOfWork);
        }
    }
}
