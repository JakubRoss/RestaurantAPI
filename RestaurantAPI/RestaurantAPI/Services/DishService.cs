using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Services;
using System.Collections;

namespace RestaurantAPI
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto createDishDto);
        IEnumerable getAllDishes(int restaurantId);
        int deleteDish(int dishID,int restaurantID);
    }

    public class DishService : IDishService
    {
        private RestaurantDbContext _dbContext;
        private IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
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
        public IEnumerable getAllDishes(int restaurantId)
        {
            var dishes = _dbContext.Dishes
                .Where(i=>i.RestaurantId==restaurantId);
            return dishes;
        }

        public int deleteDish(int dishID, int restaurantID)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(id => id.Id == restaurantID);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var dish = _dbContext.Dishes
                .FirstOrDefault(i => i.Id == dishID);
            _dbContext.Dishes.Remove(dish);
            _dbContext.SaveChanges();
            return dish.Id;
        }
    }
}