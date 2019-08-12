using DtoModels.Enums;
using System;

namespace BusinessLayer.Models.Meals
{
    public class MealViewModel
    {
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public MealTypeEnum MealType { get; set; }
    }
}
