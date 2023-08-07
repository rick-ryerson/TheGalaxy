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
   internal class MaritalStatusTypeRepository : IMaritalStatusTypeRepository {
      private readonly IUnitOfWork<DataContext> unitOfWork;

      public MaritalStatusTypeRepository(IUnitOfWork<DataContext> unitOfWork) {
         this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      }

      public async Task<MaritalStatusType> AddAsync(MaritalStatusType maritalStatusType) {
         await unitOfWork.Context.MaritalStatusTypes.AddAsync(maritalStatusType);

         return maritalStatusType;
      }

      public async Task DeleteAsync(int id) {
         var maritalStatusType = await GetAsync(id);

         if (maritalStatusType == null)
            throw new DeleteException($"MaritalStatusType with id {id} does not exist.");

         unitOfWork.Context.MaritalStatusTypes.Remove(maritalStatusType);
      }

      public IEnumerable<MaritalStatusType> Get(int pageIndex, int pageSize) {
         return unitOfWork.Context
             .MaritalStatusTypes
             .OrderBy(g => g.Id)
             .Skip(pageSize * pageIndex)
             .Take(pageSize);
      }

      public async Task<MaritalStatusType> GetAsync(int id) {
         return await unitOfWork.Context
             .MaritalStatusTypes
             .FindAsync(id);
      }

      public async Task<MaritalStatusType> GetExactAsync(string value) {
         return await unitOfWork.Context
             .MaritalStatusTypes
             .Where(g => g.Value == value)
             .FirstOrDefaultAsync();
      }
      public IEnumerable<MaritalStatusType> GetContains(string value) {
         return unitOfWork.Context
             .MaritalStatusTypes
             .Where(g => g.Value.Contains(value));
      }
      public void Update(MaritalStatusType maritalStatusType) {
         unitOfWork.Context
             .MaritalStatusTypes
             .Attach(maritalStatusType);

         unitOfWork.Context
             .Entry(maritalStatusType)
             .State = EntityState.Modified;
      }
   }
}
