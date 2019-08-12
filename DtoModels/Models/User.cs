using DtoModels.Enums;
using System.Collections.Generic;

namespace DtoModels.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public GenderEnum Gender { get; set; }
        public ActivityEnum Activity { get; set; }
        public RoleEnum Role { get; set; }
        public ICollection<Meal> Meals { get; set; }
    }
}
