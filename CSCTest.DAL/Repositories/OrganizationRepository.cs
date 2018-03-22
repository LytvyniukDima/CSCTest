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
    public class OrganizationRepository : IRepository<Organization>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<Organization> dbSet;

        public OrganizationRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Organizations;
        }

        public void Add(Organization entity)
        {
            var organization = Find(x => x.Id != entity.Id && x.Code == entity.Code);
            if (organization != null)
                return;
                
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Organization> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(Organization entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Organization> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public Organization Find(Func<Organization, bool> predicate)
        {
            return dbSet
                .Include(o => o.User)
                .Include(o => o.Countries)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Organization> FindAll(Func<Organization, bool> predicate)
        {
            return dbSet
                .Include(o => o.User)
                .Include(o => o.Countries)
                .Where(predicate)
                .ToList();
        }

        public async Task<Organization> FindAsync(Expression<Func<Organization, bool>> predicate)
        {
            return await dbSet
                .Include(o => o.User)
                .Include(o => o.Countries)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Organization> GetAll()
        {
            return dbSet
                .Include(o => o.User)
                .Include(o => o.Countries)
                .ToList();
        }
        
        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            return await dbSet
                .Include(o => o.User)
                .Include(o => o.Countries)
                .ToListAsync();
        }

        public void Update(Organization entity)
        {
            var organization = Find(x => x.Id != entity.Id && x.Code == entity.Code);
            if (organization != null)
                return;

            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}