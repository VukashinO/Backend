using DtoModels.Enums;
using System;


namespace DtoModels.Models
{
    public class Meal : BaseEntity
    {
        
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public MealTypeEnum MealType { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
       
    }
}
