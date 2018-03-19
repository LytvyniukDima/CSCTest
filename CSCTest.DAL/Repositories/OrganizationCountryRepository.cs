using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSCTest.DAL.EF;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCTest.DAL.Repositories
{
    public class OrganizationCountryRepository : IRepository<OrganizationCountry>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<OrganizationCountry> dbSet;

        public OrganizationCountryRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.OrganizationCountries;
        }

        public void Add(OrganizationCountry entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<OrganizationCountry> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Delete(OrganizationCountry entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<OrganizationCountry> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public OrganizationCountry Find(Func<OrganizationCountry, bool> predicate)
        {
            return dbSet
                .Include(o => o.Organization)
                .Include(o => o.Country)
                .Include(o => o.CountryBusinesses)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<OrganizationCountry> FindAll(Func<OrganizationCountry, bool> predicate)
        {
            return dbSet
                .Include(o => o.Organization)
                .Include(o => o.Country)
                .Include(o => o.CountryBusinesses)
                .Where(predicate)
                .ToList();
        }

        public async Task<OrganizationCountry> FindAsync(Expression<Func<OrganizationCountry, bool>> predicate)
        {
            return await dbSet
                .Include(o => o.Organization)
                .Include(o => o.Country)
                .Include(o => o.CountryBusinesses)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<OrganizationCountry> GetAll()
        {
            return dbSet
                .Include(o => o.Organization)
                .Include(o => o.Country)
                .Include(o => o.CountryBusinesses)
                .ToList();
        }

        public async Task<IEnumerable<OrganizationCountry>> GetAllAsync()
        {
            return await dbSet
                .Include(o => o.Organization)
                .Include(o => o.Country)
                .Include(o => o.CountryBusinesses)
                .ToListAsync();
        }

        public void Update(OrganizationCountry entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}