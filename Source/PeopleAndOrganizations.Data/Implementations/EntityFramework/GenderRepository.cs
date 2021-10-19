using Microsoft.EntityFrameworkCore;
using PeopleAndOrganizations.Data.Interfaces;
using PeopleAndOrganizations.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleAndOrganizations.Data.Implementations.EntityFramework
{
    public class GenderRepository : IGenderRepository
    {
        private readonly DataContext dataContext;

        public GenderRepository(DataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public Gender Add(Gender gender)
        {
            dataContext.Genders.Add(gender);

            return gender;
        }

        public void Delete(int id)
        {
            var gender = Get(id);

            dataContext.Genders.Remove(gender);
        }

        public IEnumerable<Gender> Get(int pageIndex, int pageSize)
        {
            return dataContext
                .Genders
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
        }

        public Gender Get(int id)
        {
            return dataContext
                .Genders
                .Find(id);
        }

        public Gender Get(string value)
        {
            return dataContext
                .Genders
                .Where(g => g.Value == value)
                .FirstOrDefault();
        }

        public void Update(Gender gender)
        {
            dataContext
                .Entry(gender)
                .State = EntityState.Modified;
        }
    }
}
