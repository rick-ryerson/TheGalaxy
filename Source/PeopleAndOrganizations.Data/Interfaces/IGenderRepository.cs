using PeopleAndOrganizations.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Data.Interfaces
{
    public interface IGenderRepository
    {
        IEnumerable<Gender> Get(int pageIndex, int pageSize);
        Gender Get(int id);
        Gender GetExact(string value);
        IEnumerable<Gender> GetContains(string value);
        Gender Add(Gender gender);
        void Update(Gender gender);
        void Delete(int id);
    }
}
