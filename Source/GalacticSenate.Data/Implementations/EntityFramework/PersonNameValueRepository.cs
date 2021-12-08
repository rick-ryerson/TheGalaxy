using Microsoft.EntityFrameworkCore;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticSenate.Domain.Exceptions;

namespace GalacticSenate.Data.Implementations.EntityFramework
{
    internal class PersonNameValueRepository : IPersonNameValueRepository
    {
        private readonly IUnitOfWork<DataContext> unitOfWork;

        public PersonNameValueRepository(IUnitOfWork<DataContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PersonNameValue> AddAsync(PersonNameValue personNameValue)
        {
            await unitOfWork.Context.PersonNameValues.AddAsync(personNameValue);

            return personNameValue;
        }

        public async Task DeleteAsync(int id)
        {
            var personNameValue = await GetAsync(id);

            if (personNameValue == null)
                throw new DeleteException($"PersonNameValue with id {id} does not exist.");

            unitOfWork.Context.PersonNameValues.Remove(personNameValue);
        }

        public IEnumerable<PersonNameValue> Get(int pageIndex, int pageSize)
        {
            return unitOfWork.Context
                .PersonNameValues
                .OrderBy(g => g.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
        }

        public async Task<PersonNameValue> GetAsync(int id)
        {
            return await unitOfWork.Context
                .PersonNameValues
                .FindAsync(id);
        }

        public async Task<PersonNameValue> GetExactAsync(string value)
        {
            return await unitOfWork.Context
                .PersonNameValues
                .Where(g => g.Value == value)
                .FirstOrDefaultAsync();
        }
        public IEnumerable<PersonNameValue> GetContains(string value)
        {
            return unitOfWork.Context
                .PersonNameValues
                .Where(g => g.Value.Contains(value));
        }
        public void Update(PersonNameValue personNameValue)
        {
            unitOfWork.Context
                .PersonNameValues
                .Attach(personNameValue);

            unitOfWork.Context
                .Entry(personNameValue)
                .State = EntityState.Modified;
        }
    }
}
