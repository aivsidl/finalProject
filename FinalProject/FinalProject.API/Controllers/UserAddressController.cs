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
        [Route("api/address")]

        public class UserAddressController : ControllerBase
        {
            private readonly IUserService userService;
            public UserAddressController(IUserService userService)
            {
                this.userService = userService;
            }

            [Authorize]
            [HttpPut]
            [Route("{userId:int}")]

            public async Task<ActionResult> UpdateUserAddressAsync(int userId, UserAddressRequest userAddressRequest)
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
            await userService.UpdateUserAddressDtoAsync(userId, new UserAddressDto
            {

                City = userAddressRequest.City,
                Street = userAddressRequest.Street,
                HouseNumber = userAddressRequest.HouseNumber,
                ApartamentNumber = userAddressRequest.ApartamentNumber,

            });

                return Ok();
            }

        [Authorize]
        [HttpPost]
        [Route("{userId:int}")]

        public async Task<ActionResult> AddUserAddressAsync(int userId, UserAddressRequest userAddressRequest)
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
            await userService.AddUserAddressDtoAsync(userId, new UserAddressDto
            {

                City = userAddressRequest.City,
                Street = userAddressRequest.Street,
                HouseNumber = userAddressRequest.HouseNumber,
                ApartamentNumber = userAddressRequest.ApartamentNumber,

            });

            return Ok();
        }



    }
    
}
