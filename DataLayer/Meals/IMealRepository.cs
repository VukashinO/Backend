using DtoModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Meals
{
    public interface IMealRepository : IRepository<Meal>
    {
        IEnumerable<Meal> GetAllMealsByUserId(Guid userId);
        IEnumerable<Meal> GetMealsByUserIdAndDate(Guid id, DateTime date);
        IEnumerable<UserMeal> GetMealsWithDapper(Guid userId);
        IQueryable<Calendar> GetMealsCalendar(Guid userId);
        double GetTotalCaloriesSum(Guid userId, int dayDate);
    }
}
