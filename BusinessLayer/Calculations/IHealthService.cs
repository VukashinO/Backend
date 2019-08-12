using BusinessLayer.Models.Setup;
using BusinessLayer.Models.Users;
using System;

namespace BusinessLayer.Setup
{
    public interface IHealthService
    {
        TotalCaloriesUserModel TotalCalories(Guid userId);
        BodyMassIndexFormulaViewModel CalculateBmi(Guid userId);
    }
}
