using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BusinessLayer.Helpers;
using BusinessLayer.Models.Users;
using DataLayer.Users;
using DtoModels.Enums;
using DtoModels.Models;

namespace BusinessLayer.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IHashHelper _hashHelper;
        private readonly IPasswordHelper _passwordRegex;

        public UserService(
            IUserRepository userRepository,
            ITokenHelper tokenHelper,
            IHashHelper hashHelper,
            IPasswordHelper passwordRegex
            )
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _hashHelper = hashHelper;
            _passwordRegex = passwordRegex;
        }

        public AuthorizeModel Login(UserLoginModel userLogin)
        {
            var user = _userRepository.GetUserByEmail(userLogin.Email);

            if (user == null) throw new Exception("Invalid Credentials");

            (_, string checkedPassword) = _hashHelper.Hash(userLogin.Password, user.Salt);

            if (user.Password != checkedPassword) throw new Exception("Invalid Credentials");

            var mappedUser = new AuthorizeModel
            {
                Id = user.Id,
                Email = user.Email,
            };
            mappedUser.Token = _tokenHelper.GenerateToken(user.Email, user.Id, user.Role);

            return mappedUser;
        }

        public AuthorizeModel Register(UserRegisterModel userRegister)
        {
            if (!new EmailAddressAttribute().IsValid(userRegister.Email))
                throw new Exception("Please enter valid Email format");

            if (!_passwordRegex.GetPasswordRegex(userRegister.Password))
                throw new Exception("You need to add at least one lower case," +
                    " at least one upper case,  at least one number, at least one special character, minimum 8 characters");

            if (userRegister.Password != userRegister.ConfirmPassword)
                throw new Exception("Invalid Credentials");

            var userExists = _userRepository.GetUserByEmail(userRegister.Email);

            if (userExists != null)
                throw new Exception("There is already User with that E-mail");

            (string salt, string hashedPassword) = _hashHelper.Hash(userRegister.Password);

            var user = new User
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                Password = hashedPassword,
                Salt = salt,
                Role = RoleEnum.Customer
            };

            _userRepository.Create(user);

            var mappedUser = new AuthorizeModel { Id = user.Id, Email = user.Email };

            mappedUser.Token = _tokenHelper.GenerateToken(user.Email, user.Id, user.Role);

            return mappedUser;

        }

        public void ChangePassword(ChangePasswordModel changePassword)
        {
            var user = _userRepository.GetUserByEmail(changePassword.Email);

            if (user == null) throw new Exception("Invalid Credentials");

            if (user.Password != changePassword.OldPassword) throw new Exception("Invalid Credentials");

            if (user.Password == changePassword.NewPassword) throw new Exception("You have entered same password");

            user.Password = changePassword.NewPassword;

            _userRepository.Update(user);
        }

        public UserEditModel GetProfile(Guid userId)
        {
            var user = _userRepository.GetById(userId);

            return new UserEditModel
            {
                Age = user.Age,
                Weight = user.Weight,
                Height = user.Height,
                Gender = user.Gender,
                Activity = user.Activity
            };
        }

        public void Edit(Guid id, UserEditModel userEditModel)
        {
            bool isValid = IsValid(userEditModel);
            if (!isValid) throw new Exception("Please input valid parametars!");

            var user = _userRepository.GetById(id);

            user.Age = userEditModel.Age;
            user.Height = userEditModel.Height;
            user.Weight = userEditModel.Weight;
            user.Activity = userEditModel.Activity;
            user.Gender = userEditModel.Gender;

            _userRepository.Update(user);
        }

        public UserViewModel GetUserById(Guid id)
        {
            var user = _userRepository.GetById(id);

            return new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Weight = user.Weight,
                Height = user.Height,
                Activity = user.Activity,
                Gender = user.Gender
            };
        }
        private bool IsValid(UserEditModel userEditModel)
        {
            if (userEditModel.Age < 12 || userEditModel.Age > 75) return false;

            if (userEditModel.Weight < 50 || userEditModel.Weight > 140) return false;

            if (userEditModel.Height < 120 || userEditModel.Height > 210) return false;

            return true;
        }

        public void DeleteUser(string email)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null) throw new Exception("There is no user with that Email!");

            _userRepository.Delete(user);
        }

        public List<UserViewModel> GetAllUsers()
        {
            return _userRepository.GetAll()
                .Select(u => new UserViewModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    Height = u.Height,
                    Weight = u.Weight,
                    Activity = u.Activity,
                    Gender = u.Gender
                }).ToList();
        }
    }
}
