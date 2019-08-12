using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DtoModels.Models;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Meals
{
    public class MealRepository : IMealRepository
    {
        private readonly CalorieCounterDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public MealRepository(CalorieCounterDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        private IDbConnection _connection
        {
            get
            {
                return new SqlConnection(_configuration.GetSection("ConnectionString").GetChildren().FirstOrDefault().Value);
            }
        }

        public IQueryable<Calendar> GetMealsCalendar(Guid userId)
        {
            var result =
                from m in _dbContext.Meals
                where m.UserId == userId
                group m by new
                {
                    m.DateCreated
                }
                into g
                select new Calendar
                {
                    CreatedDate = g.Key.DateCreated
                };

            return result;
        }

        public IEnumerable<Meal> GetAllMealsByUserId(Guid userId)
        {
            return _dbContext.Meals.Where(meal => meal.UserId == userId);
        }

        public IEnumerable<Meal> GetMealsByUserIdAndDate(Guid id, DateTime date)
        {
            return _dbContext.Meals.Where(meal => meal.UserId == id && meal.DateCreated == date);
        }

        public double GetTotalCaloriesSum(Guid userId, int dayDate)
        {
            return _dbContext.Meals
                .Where(meal => meal.UserId == userId && meal.DateCreated.Day == dayDate)
                .Sum(x => x.Calories);
        }

        public List<Meal> GetAll()
        {
            return _dbContext.Set<Meal>().ToList();
        }

        public Meal GetById(Guid id)
        {
            return _dbContext.Set<Meal>().Find(id);
        }

        public void Create(Meal obj)
        {
            obj.Id = Guid.NewGuid();
            _dbContext.Set<Meal>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Update(Meal obj) // prasaj ova use ednas
        {
            if (obj.Id == Guid.Empty)
            {
                Create(obj);
            }
            else
            {
                _dbContext.Set<Meal>().Update(obj);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(Meal obj)
        {
            _dbContext.Set<Meal>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<UserMeal> GetMealsWithDapper(Guid userId)
        {
            IEnumerable<UserMeal> meals;
            using (IDbConnection con = _connection)
            {
                con.Open();
                //var sql = "Select * From Meals where UserId = userId";
                var sql = $@"SELECT 
                                 FirstName 
                                ,LastName
                                ,Email
                                ,[Name]
                                ,Calories
                                ,MealType
                                ,DateCreated 
                            FROM Meals AS me
                            INNER join Users AS us
                                    ON me.UserId = us.Id
                                        WHERE UserId = '{userId}'
                            GROUP by FirstName
                                ,LastName
                                ,Email
                                ,[Name]
                                ,Calories
                                ,MealType
                                ,DateCreated";
                meals = con.Query<UserMeal>(sql);
            }

            return meals;

        }
    }
}
