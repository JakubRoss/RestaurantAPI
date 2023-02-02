using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Security.Claims;

namespace RestaurantAPI
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
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

            _service.Update(dto,id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _service.Delete(id);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles ="Admin, Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto) 
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _service.CreateRestaurant(dto);

            return Created($"api/restaurant/{id}",id);
        }

        [HttpGet]
        [AllowAnonymous]
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
                throw new NotFoundException("Restaurant not found");
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