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

        public AccountService(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
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
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}