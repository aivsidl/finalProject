using FinalProject.API.Models;
using FinalProject.BusinessLayer.Infrastructure.Exeptions;
using FinalProject.BusinessLayer.Models;
using FinalProject.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/user")]
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
        public async Task<ActionResult> LoginAsync(LoginDto loginDto)
        {            
            return Ok(await userService.LoginAsync(loginDto));
        }


        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetUserByIdAsync(int id)
        {
            return Ok(await userService.GetUserByIdAsync(id));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            await userService.DeleteUserAsync(id);

            return Ok("User deleted successfully.");
        }




    }
}
    