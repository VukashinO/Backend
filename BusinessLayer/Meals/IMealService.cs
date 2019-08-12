using BusinessLayer.Models.Meals;
using System;
using System.Collections.Generic;
using DtoModels.Models;
using System.Linq;

namespace BusinessLayer.Meals
{
    public interface IMealService
    {
        Guid CreateMeal(MealViewModel mealViewModel, Guid userId);
        CalendarViewModel GetMealByUserIdAndDate(Guid userId, DateTime date);
        IEnumerable<UserMeal> GetMeals(Guid userId);
        IEnumerable<DateCreatedViewModel> GetCalendar(Guid userId);
        void RemoveMeal(Guid mealId);
        TotalCalorieViewModel GetTotalCalorieSum(Guid userId, int dayDate);
    }
}
