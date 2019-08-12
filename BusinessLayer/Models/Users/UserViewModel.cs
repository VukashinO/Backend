using DtoModels.Enums;

namespace BusinessLayer.Models.Users
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public GenderEnum Gender { get; set; }
        public ActivityEnum Activity { get; set; }
    }
}
