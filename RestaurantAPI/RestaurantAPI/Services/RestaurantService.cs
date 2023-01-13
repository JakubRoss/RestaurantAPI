using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int CreateRestaurant(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        IEnumerable<NamesDto> GetList();
        public bool Delete(int id);
        public bool Edit(EditRestaurantDto editRestaurant, int id);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }


        public bool Edit(EditRestaurantDto editRestaurant, int id)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
                return false;
            _logger.LogTrace($"Restaurant name with id:{restaurant.Id} was changed from {restaurant.Name} to {editRestaurant.Name}");
            restaurant.Name = editRestaurant.Name;
            restaurant.Description= editRestaurant.Description;
            restaurant.HasDelivery = editRestaurant.HasDelivery;
            _dbContext.SaveChanges();
            return true;
        }
        public IEnumerable<NamesDto> GetList()
        {
            var restaurants = _dbContext
                .Restaurants
                .ToList();
            var restaurantDtos = _mapper.Map<List<NamesDto>>(restaurants);
            return restaurantDtos;
        }
        public bool Delete(int id)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant == null) 
                return false;

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
            return true;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurnat = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);
            if (restaurnat == null)
                return null;
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurnat);
            return restaurantDto;
        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();
            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantDtos;
        }

        public int CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }
    }
}