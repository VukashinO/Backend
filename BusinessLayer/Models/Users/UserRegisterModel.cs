using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Users
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
       // [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
