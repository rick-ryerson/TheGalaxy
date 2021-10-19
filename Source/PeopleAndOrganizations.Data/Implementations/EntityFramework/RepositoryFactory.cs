using PeopleAndOrganizations.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Data.Implementations.EntityFramework
{
    public static class RepositoryFactory
    {
        public static IGenderRepository GenderRepository(UnitOfWork<DataContext> unitOfWork)
        {
            return new GenderRepository(unitOfWork);
        }
    }
}
