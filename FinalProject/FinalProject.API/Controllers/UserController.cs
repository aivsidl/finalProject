using FinalProject.BusinessLayer.Models;
using FinalProject.BusinessLayer.Services;
using FinalProject.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAsync(RegisterUser registerUser)
        {
            await userService.AddAsync(registerUser);
            return Ok();

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> LoginAsync(UserDto userdto)
        {            
            return Ok(await userService.LoginAsync(userdto));
        }


        [Authorize]
        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> GetAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
               var a = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
              
            }

            return Ok("Labas");
        }


        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetUserByIdAsync(int id)
        {
            return Ok(await userService.GetUserByIdAsync(id));
        }
    }
}
    