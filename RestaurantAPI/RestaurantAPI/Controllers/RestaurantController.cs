using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll() 
        {
            var restaurants = _dbContext
                .Restaurants
                .ToList();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Restaurant>>Get(int id)
        {
            var restaurnat = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if (restaurnat == null)
                return NotFound();
            return Ok(restaurnat);
        }
    }
}