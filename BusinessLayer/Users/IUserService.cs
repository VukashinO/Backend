using BusinessLayer.Models.Users;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Users
{
    public interface IUserService
    {
        UserViewModel GetUserById(Guid id);
        AuthorizeModel Login(UserLoginModel userLogin);
        AuthorizeModel Register(UserRegisterModel userRegister);
        void ChangePassword(ChangePasswordModel changePassword);
        void Edit(Guid id, UserEditModel userEditModel);
        UserEditModel GetProfile(Guid userId);
        void DeleteUser(string email);
        List<UserViewModel> GetAllUsers();
    }
}
