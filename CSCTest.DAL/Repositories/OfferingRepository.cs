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
    public class OfferingRepository : IRepository<Offering>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<Offering> dbSet;

        public OfferingRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Offerings;
        }

        public void Add(Offering entity)
        {
            var offering = Find(x => x.Name == entity.Name && x.Family == entity.Family);
            if (offering != null)
                return;

            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Offering> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(Offering entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Offering> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public Offering Find(Func<Offering, bool> predicate)
        {
            return dbSet
                .Include(o => o.Family)
                .Include(o => o.FamilyOfferings)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Offering> FindAll(Func<Offering, bool> predicate)
        {
            return dbSet
                .Include(o => o.Family)
                .Include(o => o.FamilyOfferings)
                .Where(predicate)
                .ToList();
        }

        public async Task<Offering> FindAsync(Expression<Func<Offering, bool>> predicate)
        {
            return await dbSet
                .Include(o => o.Family)
                .Include(o => o.FamilyOfferings)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Offering> GetAll()
        {
            return dbSet
                .Include(o => o.Family)
                .Include(o => o.FamilyOfferings)
                .ToList();
        }

        public async Task<IEnumerable<Offering>> GetAllAsync()
        {
            return await dbSet
                .Include(o => o.Family)
                .Include(o => o.FamilyOfferings)
                .ToListAsync();
        }

        public void Update(Offering entity)
        {
            var offering = Find(x => x.Name == entity.Name && x.Family == entity.Family && x.Id != entity.Id);
            if (offering != null)
                return;

             dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}