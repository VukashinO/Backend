using System;

namespace BusinessLayer.Models.Users
{
    public class AuthorizeModel
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
