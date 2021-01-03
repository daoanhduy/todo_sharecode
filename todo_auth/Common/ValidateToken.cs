using Core.Entities;
using Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_auth.Core.Interfaces;
using todo_auth.Repositories;

namespace todo_auth.Common
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateToken :AuthorizeAttribute, IAuthorizationFilter
    {
        DbContextOptionsBuilder<TodoDataContext> optionsBuilder = new DbContextOptionsBuilder<TodoDataContext>();
        public ValidateToken()
        {
            optionsBuilder.UseSqlServer(Values.CONNECTION_STRING);
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            token = token.Replace("Bearer ", "");
            using (TodoDataContext db =new TodoDataContext(optionsBuilder.Options))
            {
                var result = db.Users.FirstOrDefault(x => x.Token == token);
                if (result == null)
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                    return;
                }
            }
                
        }
    }
}
