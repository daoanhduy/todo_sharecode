using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_auth.Common;
using todo_auth.Core.Interfaces;
using todo_auth.Models.Input;

namespace todo_auth.Controllers
{
    public class AuthsController : BaseApiController
    {
        private readonly IUserServices userServices;
        private readonly IConfiguration configuration;
        public AuthsController(IUserServices userServices, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userServices = userServices;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(User_Auth user_Auth)
        {
            var user = userServices.Login(user_Auth);
            if(user.id == 0)
            {
                return Unauthorized();
            }
            if (user.token == null && user.exp == null)
            {
                (string token, DateTime exp) = Helper.GenerateToken(user.id, configuration);

                var rs = await userServices.AddTokenForUser(user.id, token, exp);
                if(rs== false)
                {
                    return Unauthorized();
                }
                return Ok(new { token = token });
            }
            return Ok(new { token = user.token });
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            string token = Helper.GetTokenFromRequest(HttpContext);
            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            await userServices.RemoveTokenForUser(token);
            return NoContent();
        }
    }
}
