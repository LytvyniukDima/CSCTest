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
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<Department> dbSet;

        public DepartmentRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Departments;
        }

        public void Add(Department entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Department> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(Department entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Department> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public Department Find(Func<Department, bool> predicate)
        {
            return dbSet
                .Include(d => d.FamilyOffering)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Department> FindAll(Func<Department, bool> predicate)
        {
            return dbSet
                .Include(d => d.FamilyOffering)
                .Where(predicate)
                .ToList();
        }

        public async Task<Department> FindAsync(Expression<Func<Department, bool>> predicate)
        {
            return await dbSet
                .Include(d => d.FamilyOffering)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Department> GetAll()
        {
            return dbSet
                .Include(d => d.FamilyOffering)
                .ToList();
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await dbSet
                .Include(d => d.FamilyOffering)
                .ToListAsync();
        }

        public void Update(Department entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}