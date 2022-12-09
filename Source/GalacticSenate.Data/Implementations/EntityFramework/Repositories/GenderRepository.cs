using Microsoft.EntityFrameworkCore;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Data.Interfaces.Repositories;

namespace GalacticSenate.Data.Implementations.EntityFramework.Repositories {
   internal class GenderRepository : IGenderRepository {
      private readonly IUnitOfWork<DataContext> unitOfWork;

      public GenderRepository(IUnitOfWork<DataContext> unitOfWork) {
         this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      }

      public async Task<Gender> AddAsync(Gender gender) {
         await unitOfWork.Context.Genders.AddAsync(gender);

         return gender;
      }

      public async Task DeleteAsync(int id) {
         var gender = await GetAsync(id);

         if (gender == null)
            throw new DeleteException($"Gender with id {id} does not exist.");

         unitOfWork
            .Context
            .Genders
            .Remove(gender);
      }

      public IEnumerable<Gender> Get(int pageIndex, int pageSize) {
         return unitOfWork.Context
             .Genders
             .OrderBy(g => g.Id)
             .Skip(pageSize * pageIndex)
             .Take(pageSize);
      }

      public async Task<Gender> GetAsync(int id) {
         return await unitOfWork.Context
             .Genders
             .FindAsync(id);
      }

      public async Task<Gender> GetExactAsync(string value) {
         return await unitOfWork.Context
             .Genders
             .Where(g => g.Value == value)
             .FirstOrDefaultAsync();
      }
      public IEnumerable<Gender> GetContains(string value) {
         return unitOfWork.Context
             .Genders
             .Where(g => g.Value.Contains(value));
      }
      public void Update(Gender gender) {
         unitOfWork.Context
             .Genders
             .Attach(gender);

         unitOfWork.Context
             .Entry(gender)
             .State = EntityState.Modified;
      }
   }
}
