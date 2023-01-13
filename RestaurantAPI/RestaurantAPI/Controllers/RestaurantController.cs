using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {

        private readonly IRestaurantService _service;
        public RestaurantController(IRestaurantService restaurantService)
        {

            _service= restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult EditRestaurant([FromBody] EditRestaurantDto dto ,[FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var value = _service.Edit(dto,id);
            if (value)
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var value = _service.Delete(id);
            if(value)
                return NoContent();
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _service.CreateRestaurant(dto);

            return Created($"api/restaurant/{id}",id);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll() 
        {
            var restaurantDtos =_service.GetAll();
            return Ok(restaurantDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RestaurantDto>>Get([FromRoute]int id) 
        {
            var restaurnat = _service.GetById(id);
            if (restaurnat == null)
                return NotFound();
            return Ok(restaurnat);
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<NamesDto>> GetNames() 
        {
            var restaurantDtos = _service.GetList();
            return Ok(restaurantDtos);
        }
    }
}