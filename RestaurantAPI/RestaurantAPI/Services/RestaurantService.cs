using AutoMapper;
using RestaurantAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int CreateRestaurant(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll(string searchPhrase);
        RestaurantDto GetById(int id);
        IEnumerable<NamesDto> GetList();
        public void Delete(int id);
        public void Update(EditRestaurantDto editRestaurant, int id);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }


        public void Update(EditRestaurantDto editRestaurant, int id)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorization = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant.Id, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if(!authorization.Succeeded)
            {
                throw new ForbiddenException();
            }

            restaurant.Name = editRestaurant.Name;
            restaurant.Description= editRestaurant.Description;
            restaurant.HasDelivery = editRestaurant.HasDelivery;
            _dbContext.SaveChanges();
        }
        public IEnumerable<NamesDto> GetList()
        {
            var restaurants = _dbContext
                .Restaurants
                .ToList();
            var restaurantDtos = _mapper.Map<List<NamesDto>>(restaurants);
            return restaurantDtos;
        }
        public void Delete(int id)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public RestaurantDto GetById(int id)
        {
            var restaurnat = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);
            if (restaurnat == null)
                throw new NotFoundException("Restaurant not found");
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurnat);
            return restaurantDto;
        }
        public IEnumerable<RestaurantDto> GetAll(string searchPhrase)
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .Where(r=>searchPhrase ==null || (r.Name.ToLower().Contains(searchPhrase.ToLower()) || r.Description.ToLower().Contains(searchPhrase.ToLower())))
                .ToList();
            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantDtos;
        }

        public int CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = _userContextService.GetUserId;
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }
    }
}