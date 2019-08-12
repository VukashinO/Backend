using System;
using BusinessLayer.Models.Setup;
using BusinessLayer.Models.Users;
using DataLayer.Users;
using DtoModels.Enums;

namespace BusinessLayer.Setup
{
    public class HealthService : IHealthService
    {
        private readonly IUserRepository _userRepository;

        public HealthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BodyMassIndexFormulaViewModel CalculateBmi(Guid userId)
        {
            var user = _userRepository.GetById(userId);

            var bmi = (user.Weight / (user.Height * user.Height)) * 10000;
            string weightRangeInfo = CheckWeightRange(bmi);

            return new BodyMassIndexFormulaViewModel
            {
                Bmi = bmi,
                WeightRangeInfo = weightRangeInfo
            };
        }

        public TotalCaloriesUserModel TotalCalories(Guid userId)
        {
            var user = _userRepository.GetById(userId);
            UserEditModel mappedModel = new UserEditModel
            {
                Age = user.Age,
                Weight = user.Weight,
                Height = user.Height,
                Activity = user.Activity
            };

            var model = new TotalCaloriesUserModel
            {
                Bmr = Math.Floor(CalculateBmr(mappedModel))
            };

            model.TotalCalories = CalculateTotalCalories(user.Activity, model.Bmr);

            return model;
        }

        private double CalculateBmr(UserEditModel userEditModel)
        {
            var coef = userEditModel.Gender == GenderEnum.Male ? 5 : -161;
            return 10 * userEditModel.Weight + 6.25 * userEditModel.Height - 5 * userEditModel.Age + coef;
        }

        private double CalculateTotalCalories(ActivityEnum activity, double bmr)
        {
            switch (activity)
            {
                case ActivityEnum.Sedentary:
                    return bmr * 1.53;
                case ActivityEnum.Moderate:
                    return bmr * 1.76;
                case ActivityEnum.Vigorous:
                    return bmr * 2.25;
                default:
                    throw new Exception("Argument exception!");
            }
        }

        private string CheckWeightRange(double bmi)
        {

            if (bmi < 18.5)
                return "UnderWeight";
            else if (bmi < 25)
                return "NormalRange";
            else if (bmi < 30)
                return "OverWeight";
            else
                return "Obese";
        }
    }
}
