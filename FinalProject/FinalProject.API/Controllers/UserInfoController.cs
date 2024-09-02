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
    [Route("api/info")]

    public class UserInfoController : ControllerBase
    {
        private readonly IUserService userService;
        public UserInfoController(IUserService userService)
        {
            this.userService = userService;
        }


        [Authorize]
        [HttpPut]
        [Route("{userId:int}")]

        public async Task<ActionResult> UpdateUserInfoAsync(int userId, UserInfoRequest userInfoRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUserId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (currentUserId != userId)
                {
                    throw new StatusCodeException(HttpStatusCode.Unauthorized, $"Unauthorized action");
                }

            }

            await userService.UpdateUserInfoDtoAsync(userId, new UserInfoDto
            {
                FirstName = userInfoRequest.FirstName,
                LastName = userInfoRequest.LastName,
                PersonalCode = userInfoRequest.PersonalCode,
                Phone = userInfoRequest.Phone,
                Email = userInfoRequest.Email,
            });

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{userId:int}")]

        public async Task<ActionResult> AddUserInfoAsync(int userId, UserInfoRequest userInfoRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var currentUserId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (currentUserId != userId)
                {
                    throw new StatusCodeException(HttpStatusCode.Unauthorized, $"Unauthorized action");
                }

            }

            await userService.AddUserInfoDtoAsync(userId, new UserInfoDto
            {
                FirstName = userInfoRequest.FirstName,
                LastName = userInfoRequest.LastName,
                PersonalCode = userInfoRequest.PersonalCode,
                Phone = userInfoRequest.Phone,
                Email = userInfoRequest.Email,
            });

            return Ok();
        }


    }
}