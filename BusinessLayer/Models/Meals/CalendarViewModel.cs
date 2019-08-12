using DtoModels.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models.Meals
{
    public class CalendarViewModel
    {
        public DateTime? Date { get; set; }
        public IEnumerable<Meal> Meals { get; set; }
        public double TotalCalories { get; set; }

        public class Meal
        {
            public double Calories { get; set; }
            public string Name { get; set; }
            public MealTypeEnum MealType { get; set; }
            [JsonIgnore]
            public DateTime DateCreated { get; set; }
        }
    }
}
