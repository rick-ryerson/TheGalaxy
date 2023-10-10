using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Implementations.EntityFramework.Repositories {
   internal class PersonNameTypeRepository : IPersonNameTypeRepository {
      private readonly IUnitOfWork<DataContext> unitOfWork;

      public PersonNameTypeRepository(IUnitOfWork<DataContext> unitOfWork) {
         this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      }

      public async Task<PersonNameType> AddAsync(PersonNameType gender) {
         await unitOfWork.Context.PersonNameTypes.AddAsync(gender);

         return gender;
      }

      public async Task DeleteAsync(int id) {
         var gender = await GetAsync(id);

         if (gender == null)
            throw new DeleteException($"PersonNameType with id {id} does not exist.");

         unitOfWork.Context.PersonNameTypes.Remove(gender);
      }

      public IEnumerable<PersonNameType> Get(int pageIndex, int pageSize) {
         return unitOfWork.Context
             .PersonNameTypes
             .OrderBy(g => g.Id)
             .Skip(pageSize * pageIndex)
             .Take(pageSize);
      }

      public async Task<PersonNameType> GetAsync(int id) {
         return await unitOfWork.Context
             .PersonNameTypes
             .FindAsync(id);
      }

      public async Task<PersonNameType> GetExactAsync(string value) {
         return await unitOfWork.Context
             .PersonNameTypes
             .Where(g => g.Value == value)
             .FirstOrDefaultAsync();
      }
      public IEnumerable<PersonNameType> GetContains(string value) {
         return unitOfWork.Context
             .PersonNameTypes
             .Where(g => g.Value.Contains(value));
      }
      public void Update(PersonNameType gender) {
         unitOfWork.Context
             .PersonNameTypes
             .Attach(gender);

         unitOfWork.Context
             .Entry(gender)
             .State = EntityState.Modified;
      }

   }
}
