using DtoModels.Models;
using System;

namespace DataLayer.Users
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(Guid id);
        User GetUserByEmail(string email);
    }
}
