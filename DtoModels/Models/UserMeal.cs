using DtoModels.Enums;

namespace DtoModels.Models
{
    public class UserMeal
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public MealTypeEnum MealType { get; set; }
    }
}
