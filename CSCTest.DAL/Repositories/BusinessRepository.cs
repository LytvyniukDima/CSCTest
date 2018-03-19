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
    public class BusinessRepository : IRepository<Business>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<Business> dbSet;

        public BusinessRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Businesses;
        }

        public void Add(Business entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Business> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Delete(Business entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Business> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public Business Find(Func<Business, bool> predicate)
        {
            return dbSet
                .Include(b => b.CountryBusinesses)
                .Include(b => b.Families)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Business> FindAll(Func<Business, bool> predicate)
        {
            return dbSet
                .Include(b => b.CountryBusinesses)
                .Include(b => b.Families)
                .Where(predicate)
                .ToList();
        }

        public async Task<Business> FindAsync(Expression<Func<Business, bool>> predicate)
        {
            return await dbSet
                .Include(b => b.CountryBusinesses)
                .Include(b => b.Families)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Business> GetAll()
        {
            return dbSet
                .Include(b => b.CountryBusinesses)
                .Include(b => b.Families)
                .ToList();
        }

        public async Task<IEnumerable<Business>> GetAllAsync()
        {
            return await dbSet
                .Include(b => b.CountryBusinesses)
                .Include(b => b.Families)
                .ToListAsync();
        }

        public void Update(Business entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}