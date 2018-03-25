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
    public class FamilyRepository : IRepository<Family>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<Family> dbSet;

        public FamilyRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Families;
        }

        public void Add(Family entity)
        {
            var family = Find(x => x.Name == entity.Name && x.BusinessId == entity.Business.Id);
            if (family != null)
            {
                throw new DALException($"Type of family with the name {entity.Name} and depend on business {entity.Business.Name} exist");
            }

            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Family> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(Family entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Family> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public Family Find(Func<Family, bool> predicate)
        {
            return dbSet
                .Include(f => f.Business)
                .Include(f => f.BusinessFamilies)
                .Include(f => f.Offerings)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Family> FindAll(Func<Family, bool> predicate)
        {
            return dbSet
                .Include(f => f.Business)
                .Include(f => f.BusinessFamilies)
                .Include(f => f.Offerings)
                .Where(predicate)
                .ToList();
        }

        public async Task<Family> FindAsync(Expression<Func<Family, bool>> predicate)
        {
            return await dbSet
                .Include(f => f.Business)
                .Include(f => f.BusinessFamilies)
                .Include(f => f.Offerings)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Family> GetAll()
        {
            return dbSet
                .Include(f => f.Business)
                .Include(f => f.BusinessFamilies)
                .Include(f => f.Offerings)
                .ToList();
        }

        public async Task<IEnumerable<Family>> GetAllAsync()
        {
            return await dbSet
                .Include(f => f.Business)
                .Include(f => f.BusinessFamilies)
                .Include(f => f.Offerings)
                .ToListAsync();
        }

        public void Update(Family entity)
        {
            var family = Find(x => x.Name == entity.Name && x.BusinessId == entity.Business.Id && x.Id != entity.Id);
            if (family != null)
            {
                throw new DALException($"Type of family with the name {entity.Name} and depend on business {entity.Business.Name} exist");
            }

            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}