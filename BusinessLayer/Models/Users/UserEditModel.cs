using DtoModels.Enums;

namespace BusinessLayer.Models.Users
{
    public class UserEditModel
    {
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public ActivityEnum Activity { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
