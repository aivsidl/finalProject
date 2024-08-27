using FinalProject.BusinessLayer.Models;
using FinalProject.BusinessLayer.Services;
using FinalProject.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAsync(RegisterUser registerUser)
        {
            await _userService.AddAsync(registerUser);
            return Ok();

        }
    }
}
