using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;

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
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll() 
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r=>r.Adress)
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
                .Include(r => r.Adress)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);
            if (restaurnat == null)
                return NotFound();
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurnat);
            return Ok(restaurantDto);
        }
    }
}