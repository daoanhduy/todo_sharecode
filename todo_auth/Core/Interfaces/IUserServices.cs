using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using todo_auth.Models.Input;
using todo_auth.Models.Output;

namespace todo_auth.Core.Interfaces
{
    public interface IUserServices
    {
        Task<User_Info> GetUser(int id);
        User_Info GetUser(string token);
        Task<bool> AddTokenForUser(int id, string token, DateTime exp);
        Task<bool> RemoveTokenForUser(string token);
        Task<int> AddUser(User_Auth user_Auth);
        Task<bool> UpdateUser(string token, string firstname, string lastname);
        (int id, string token, DateTime? exp) Login(User_Auth user_Auth);
    }
}
