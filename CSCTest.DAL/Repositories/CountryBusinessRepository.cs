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
    public class CountryBusinessRepository : IRepository<CountryBusiness>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<CountryBusiness> dbSet;

        public CountryBusinessRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.CountryBusinesses;
        }

        public void Add(CountryBusiness entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<CountryBusiness> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(CountryBusiness entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<CountryBusiness> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public CountryBusiness Find(Func<CountryBusiness, bool> predicate)
        {
            return dbSet
                .Include(c => c.Country)
                .Include(c => c.Business)
                .Include(c => c.BusinessFamilies)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<CountryBusiness> FindAll(Func<CountryBusiness, bool> predicate)
        {
            return dbSet
                .Include(c => c.Country)
                .Include(c => c.Business)
                .Include(c => c.BusinessFamilies)
                .Where(predicate)
                .ToList();
        }

        public async Task<CountryBusiness> FindAsync(Expression<Func<CountryBusiness, bool>> predicate)
        {
            return await dbSet
                .Include(c => c.Country)
                .Include(c => c.Business)
                .Include(c => c.BusinessFamilies)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<CountryBusiness> GetAll()
        {
            return dbSet
                .Include(c => c.Country)
                .Include(c => c.Business)
                .Include(c => c.BusinessFamilies)
                .ToList();
        }

        public async Task<IEnumerable<CountryBusiness>> GetAllAsync()
        {
            return await dbSet
                .Include(c => c.Country)
                .Include(c => c.Business)
                .Include(c => c.BusinessFamilies)
                .ToListAsync();
        }

        public void Update(CountryBusiness entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}