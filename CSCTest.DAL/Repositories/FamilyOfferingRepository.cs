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
    public class FamilyOfferingRepository : IRepository<FamilyOffering>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<FamilyOffering> dbSet;

        public FamilyOfferingRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.FamilyOfferings;
        }
        
        public void Add(FamilyOffering entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<FamilyOffering> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Delete(FamilyOffering entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<FamilyOffering> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public FamilyOffering Find(Func<FamilyOffering, bool> predicate)
        {
            return dbSet
                .Include(f => f.BusinessFamily)
                .Include(f => f.Offering)
                .Include(f => f.Departments)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<FamilyOffering> FindAll(Func<FamilyOffering, bool> predicate)
        {
            return dbSet
                .Include(f => f.BusinessFamily)
                .Include(f => f.Offering)
                .Include(f => f.Departments)
                .Where(predicate)
                .ToList();
        }

        public async Task<FamilyOffering> FindAsync(Expression<Func<FamilyOffering, bool>> predicate)
        {
            return await dbSet
                .Include(f => f.BusinessFamily)
                .Include(f => f.Offering)
                .Include(f => f.Departments)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<FamilyOffering> GetAll()
        {
            return dbSet
                .Include(f => f.BusinessFamily)
                .Include(f => f.Offering)
                .Include(f => f.Departments)
                .ToList();
        }

        public async Task<IEnumerable<FamilyOffering>> GetAllAsync()
        {
            return await dbSet
                .Include(f => f.BusinessFamily)
                .Include(f => f.Offering)
                .Include(f => f.Departments)
                .ToListAsync();
        }

        public void Update(FamilyOffering entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}