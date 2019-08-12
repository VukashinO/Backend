using BusinessLayer.Models.Meals;
using DataLayer.Meals;
using DtoModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Meals
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public Guid CreateMeal(MealViewModel mealViewModel, Guid userId)
        {

            Meal meal = new Meal
            {
                Name = mealViewModel.Name,
                Calories = mealViewModel.Calories,
                MealType = mealViewModel.MealType,
                DateCreated = mealViewModel.DateCreated,
                UserId = userId
            };

            _mealRepository.Create(meal);

            return meal.Id;
        }

        public CalendarViewModel GetMealByUserIdAndDate(Guid userId, DateTime date)
        {
            var meals = _mealRepository.GetMealsByUserIdAndDate(userId, date)
                .Select(meal => new CalendarViewModel.Meal
                {
                    Calories = meal.Calories,
                    Name = meal.Name,
                    DateCreated = meal.DateCreated,
                    MealType = meal.MealType
                });

            return new CalendarViewModel
            {
                Date = meals.FirstOrDefault()?.DateCreated,
                TotalCalories = meals.Sum(x => x.Calories),
                Meals = meals,
            };
        }

        public IEnumerable<UserMeal> GetMeals(Guid userId)
        {
            return _mealRepository.GetMealsWithDapper(userId);
        }

        public IEnumerable<DateCreatedViewModel> GetCalendar(Guid userId)
        {
            return _mealRepository
                .GetMealsCalendar(userId)
                .Select(x => new DateCreatedViewModel
                {
                    DateCreated = x.CreatedDate
                });
        }

        public void RemoveMeal(Guid mealId)
        {
            var meal = _mealRepository.GetById(mealId);
            _mealRepository.Delete(meal);
        }

        public TotalCalorieViewModel GetTotalCalorieSum(Guid userId, int dayDate)
        {
            var sum = _mealRepository.GetTotalCaloriesSum(userId, dayDate);

            return new TotalCalorieViewModel
            {
                TotalCaloriesSum = sum
            };
        }
    }
}
