using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public interface IAccountService
    {
        void Register(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private RestaurantDbContext _dbContext;
        private IPasswordHasher<User> _passwordHasher;

        public AccountService(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void Register(RegisterUserDto dto)
        {
            var user = new User
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashedPassword;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}