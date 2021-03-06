using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSCTest.DAL.EF;
using CSCTest.DAL.Exceptions;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCTest.DAL.Repositories
{
    public class CountryRepository : IRepository<Country>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<Country> dbSet;
        
        public CountryRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Countries;
        }

        public void Add(Country entity)
        {
            var country = Find(x => x.Code == entity.Code && x.OrganizationId == entity.Organization.Id);
            if (country != null)
            {
                throw new DALException($"Country with code {entity.Code} exist in organization {entity.Organization.Name}");
            }

            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Country> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(Country entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Country> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public Country Find(Func<Country, bool> predicate)
        {
            return dbSet
                .Include(c => c.Organization)
                .Include(c => c.CountryBusinesses)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Country> FindAll(Func<Country, bool> predicate)
        {
            return dbSet
                .Include(c => c.Organization)
                .Include(c => c.CountryBusinesses)
                .Where(predicate)
                .ToList();
        }

        public async Task<Country> FindAsync(Expression<Func<Country, bool>> predicate)
        {
            return await dbSet
                .Include(c => c.Organization)
                .Include(c => c.CountryBusinesses)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Country> GetAll()
        {
            return dbSet
                .Include(c => c.Organization)
                .Include(c => c.CountryBusinesses)
                .ToList();
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await dbSet
                .Include(c => c.Organization)
                .Include(c => c.CountryBusinesses)
                .ToListAsync();
        }

        public void Update(Country entity)
        {
            var country = Find(x => x.Id != entity.Id && x.Code == entity.Code && x.OrganizationId == entity.OrganizationId);
            if (country != null)
            {
                throw new DALException($"Country with code {entity.Code} exist in organization {entity.Organization.Name}");
            }
                
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}