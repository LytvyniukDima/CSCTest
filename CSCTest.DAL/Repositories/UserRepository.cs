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
    public class UserRepository : IRepository<User>
    {
        private readonly CSCDbContext dbContext;
        private readonly DbSet<User> dbSet;

        public UserRepository(CSCDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Users;
        }

        public void Add(User entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<User> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(User entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<User> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public User Find(Func<User, bool> predicate)
        {
            return dbSet
                .Include(u => u.Organizations)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<User> FindAll(Func<User, bool> predicate)
        {
            return dbSet
                .Include(u => u.Organizations)
                .Where(predicate)
                .ToList();
        }

        public async Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await dbSet
                .Include(u => u.Organizations)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<User> GetAll()
        {
            return dbSet
                .Include(u => u.Organizations)
                .ToList();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbSet
                .Include(u => u.Organizations)
                .ToListAsync();
        }

        public void Update(User entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}