using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _service;

        public AccountController( IAccountService service)
        {
           _service = service;
        }

        [HttpPost("register")]
        public ActionResult RegisterUSer([FromBody]RegisterUserDto dto)
        {
            _service.Register(dto);
            return Ok();
        }
    }
}