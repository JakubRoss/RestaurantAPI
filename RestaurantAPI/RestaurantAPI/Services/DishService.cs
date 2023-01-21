using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Services;

namespace RestaurantAPI
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto createDishDto);
    }

    public class DishService : IDishService
    {
        private RestaurantDbContext _dbContext;
        private IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto createDishDto)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(id => id.Id == restaurantId);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");
            var DishEntity = _mapper.Map<Dish>(createDishDto);
            DishEntity.RestaurantId= restaurantId;
            _dbContext.Add(DishEntity);
            _dbContext.SaveChanges();

            return DishEntity.Id;
        }
    }
}