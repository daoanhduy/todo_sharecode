using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using todo_auth.Common;
using todo_auth.Core.Interfaces;
using todo_auth.Models.Input;

namespace todo_auth.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserServices userServices;
        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [Authorize]
        [ValidateToken]
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if(id<=0 || id > Int32.MaxValue)
            {
                return UnprocessableEntity();
            }
            var result = await userServices.GetUser(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(new { data = result });
        }

        [Authorize]
        [ValidateToken]
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            string token = Helper.GetTokenFromRequest(HttpContext);
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            var result =  userServices.GetUser(token);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new { data = result });
        }

        [Authorize]
        [ValidateToken]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User_Info_Input user_Info)
        {
            string token = Helper.GetTokenFromRequest(HttpContext);
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            var result = await userServices.UpdateUser(token, user_Info.FirstName, user_Info.LastName);
            if (result == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> AddUser(User_Auth user_Auth)
        {
            var result = await userServices.AddUser(user_Auth);
            return result == 0 ? StatusCode(StatusCodes.Status422UnprocessableEntity, new { error = StatusCodes.Status422UnprocessableEntity.ToString() })
                : StatusCode(StatusCodes.Status201Created, new { id = result });
        }
    }
}
