using System;
using System.Collections.Generic;
using System.Linq;
using DtoModels.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly CalorieCounterDbContext _dbContext;
        public UserRepository(CalorieCounterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users
                .FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(Guid id)
        {
            return _dbContext.Users
                .Include(x => x.Meals)
                .Single(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _dbContext.Set<User>().ToList();
        }

        public User GetById(Guid id)
        {
            return _dbContext.Set<User>().Find(id);
        }

        public void Create(User obj)
        {
            obj.Id = Guid.NewGuid();
            _dbContext.Set<User>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Update(User obj) // prasaj ova use ednas
        {
            if (obj.Id == Guid.Empty)
            {
                Create(obj);
            }
            else
            {
                _dbContext.Set<User>().Update(obj);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(User obj)
        {
            _dbContext.Set<User>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
