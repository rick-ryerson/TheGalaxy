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
    internal class OrganizationNameValueRepository : IOrganizationNameValueRepository
    {
        private readonly IUnitOfWork<DataContext> unitOfWork;

        public OrganizationNameValueRepository(IUnitOfWork<DataContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<OrganizationNameValue> AddAsync(OrganizationNameValue organizationNameValue)
        {
            await unitOfWork.Context.OrganizationNameValues.AddAsync(organizationNameValue);

            return organizationNameValue;
        }

        public async Task DeleteAsync(int id)
        {
            var organizationNameValue = await GetAsync(id);

            if (organizationNameValue == null)
                throw new DeleteException($"OrganizationNameValue with id {id} does not exist.");

            unitOfWork.Context.OrganizationNameValues.Remove(organizationNameValue);
        }

        public IEnumerable<OrganizationNameValue> Get(int pageIndex, int pageSize)
        {
            return unitOfWork.Context
                .OrganizationNameValues
                .OrderBy(g => g.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
        }

        public async Task<OrganizationNameValue> GetAsync(int id)
        {
            return await unitOfWork.Context
                .OrganizationNameValues
                .FindAsync(id);
        }

        public async Task<OrganizationNameValue> GetExactAsync(string value)
        {
            return await unitOfWork.Context
                .OrganizationNameValues
                .Where(g => g.Value == value)
                .FirstOrDefaultAsync();
        }
        public IEnumerable<OrganizationNameValue> GetContains(string value)
        {
            return unitOfWork.Context
                .OrganizationNameValues
                .Where(g => g.Value.Contains(value));
        }
        public void Update(OrganizationNameValue organizationNameValue)
        {
            unitOfWork.Context
                .OrganizationNameValues
                .Attach(organizationNameValue);

            unitOfWork.Context
                .Entry(organizationNameValue)
                .State = EntityState.Modified;
        }
    }
}
