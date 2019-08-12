using Microsoft.EntityFrameworkCore;

namespace DtoModels.Models
{
    public class CalorieCounterDbContext : DbContext
    {
        public CalorieCounterDbContext(DbContextOptions<CalorieCounterDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(user => user.Meals)
                .WithOne(user => user.User)
                .HasForeignKey(meal => meal.UserId);

      



            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    Id = new Guid("9896080F-D25B-49A6-A3E0-F8CB1B9C5B1F"),
            //    UserName = "Vukashin",
            //    Email = "Vuks@gmail.com",
            //    Password="123456",
            //    Age = 22,
            //    Height = 172,
            //    Weight = 75,
            //    Activity = Enums.ActivityEnum.Moderate,
            //    Gender = Enums.GenderEnum.Male
            //},
            //new User
            //{
            //    Id = new Guid("5E4799FF-473A-4591-A11C-1BEC2CA85F14"),
            //    UserName ="Ana",
            //    Email ="Ana@gmail.com",
            //    Password = "123456",
            //    Age =20,
            //    Height =160,
            //    Weight =55,
            //    Gender =Enums.GenderEnum.Female,
            //    Activity =Enums.ActivityEnum.Sedentary
            //});
                
            //modelBuilder.Entity<Meal>().HasData(new Meal
            //{
            //    Id = new Guid("8A5EEB32-BE88-4FAB-928C-38B25B333A44"),
            //    Name="Oats",
            //    Calories=350,
            //    MealType=Enums.MealTypeEnum.Breakfast,
            //    UserId = new Guid("9896080F-D25B-49A6-A3E0-F8CB1B9C5B1F"),
            //    DateCreated = DateTime.Now
            //});


        }

    }
}
