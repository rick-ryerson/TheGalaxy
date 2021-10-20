using Microsoft.EntityFrameworkCore;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticSenate.Data.Implementations.EntityFramework
{
    internal class GenderRepository : IGenderRepository
    {
        private readonly UnitOfWork<DataContext> unitOfWork;

        public GenderRepository(UnitOfWork<DataContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Gender Add(Gender gender)
        {
            unitOfWork.Context.Genders.Add(gender);

            return gender;
        }

        public void Delete(int id)
        {
            var gender = Get(id);

            unitOfWork.Context.Genders.Remove(gender);
        }

        public IEnumerable<Gender> Get(int pageIndex, int pageSize)
        {
            return unitOfWork.Context
                .Genders
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
        }

        public Gender Get(int id)
        {
            return unitOfWork.Context
                .Genders
                .Find(id);
        }

        public Gender GetExact(string value)
        {
            return unitOfWork.Context
                .Genders
                .Where(g => g.Value == value)
                .FirstOrDefault();
        }
        public IEnumerable<Gender> GetContains(string value)
        {
            return unitOfWork.Context
                .Genders
                .Where(g => g.Value.Contains(value));
        }
        public void Update(Gender gender)
        {
            unitOfWork.Context
                .Genders
                .Attach(gender);

            unitOfWork.Context
                .Entry(gender)
                .State = EntityState.Modified;
        }
    }
}
