using DtoModels.Enums;
using System;

namespace BusinessLayer.Helpers
{
    public interface ITokenHelper
    {
        string GenerateToken(string email, Guid userId, RoleEnum role);
    }
}
