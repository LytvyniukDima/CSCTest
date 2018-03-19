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
    public class BusinessFamilyRepository : IRepository<BusinessFamily>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<BusinessFamily> dbSet;

        public BusinessFamilyRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.BusinessFamilies;
        }
        
        public void Add(BusinessFamily entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<BusinessFamily> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Delete(BusinessFamily entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<BusinessFamily> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public BusinessFamily Find(Func<BusinessFamily, bool> predicate)
        {
            return dbSet
                .Include(b => b.CountryBusiness)
                .Include(b => b.Family)
                .Include(b => b.FamilyOfferings)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<BusinessFamily> FindAll(Func<BusinessFamily, bool> predicate)
        {
            return dbSet
                .Include(b => b.CountryBusiness)
                .Include(b => b.Family)
                .Include(b => b.FamilyOfferings)
                .Where(predicate)
                .ToList();
        }

        public async Task<BusinessFamily> FindAsync(Expression<Func<BusinessFamily, bool>> predicate)
        {
            return await dbSet
                .Include(b => b.CountryBusiness)
                .Include(b => b.Family)
                .Include(b => b.FamilyOfferings)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<BusinessFamily> GetAll()
        {
            return dbSet
                .Include(b => b.CountryBusiness)
                .Include(b => b.Family)
                .Include(b => b.FamilyOfferings)
                .ToList();
        }

        public async Task<IEnumerable<BusinessFamily>> GetAllAsync()
        {
            return await dbSet
                .Include(b => b.CountryBusiness)
                .Include(b => b.Family)
                .Include(b => b.FamilyOfferings)
                .ToListAsync();
        }

        public void Update(BusinessFamily entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}