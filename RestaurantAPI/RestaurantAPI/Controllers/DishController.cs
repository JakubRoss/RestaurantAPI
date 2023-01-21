using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IDishService _dishService;

        public DishController(IDishService dishServce) 
        {
            _dishService = dishServce;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDto createDishDto) 
        {
            var newDishId=_dishService.Create(restaurantId, createDishDto);
            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}",null);
        }

        [HttpGet]
        public ActionResult GetDishes([FromRoute]int restaurantId)
        {
            var dishes =_dishService.getAllDishes(restaurantId);
            return Ok(dishes);
        }
    }
}