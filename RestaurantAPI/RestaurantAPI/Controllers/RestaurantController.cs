using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return Created($"api/restaurant/{restaurant.Id}",null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll() 
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r=>r.Address)
                .Include(r => r.Dishes)
                .ToList();
            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return Ok(restaurantDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RestaurantDto>>Get(int id)
        {
            var restaurnat = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);
            if (restaurnat == null)
                return NotFound();
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurnat);
            return Ok(restaurantDto);
        }
    }
}