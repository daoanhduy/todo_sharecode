using Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using todo_auth.Common;
using todo_auth.Core.Interfaces;
using todo_auth.Models.Input;
using todo_auth.Models.Output;

namespace todo_auth.Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Regex regexRoleMember = new Regex("^[a-zA-Z0-9_]{6,52}$");
        private readonly Regex regexRoleAdmin = new Regex("^[a-zA-Z0-9_]{6,52} -> admin");
        public UserServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddTokenForUser(int id, string token, DateTime exp)
        {
            var user = await unitOfWork.GetRepository<User>().GetAsync(id);
            if (user == null)
            {
                return false;
            }
            user.Token = token;
            user.ExpirationDate = exp;
            var rs = await unitOfWork.SaveChangesAsync();
            return rs != 0 ? true : false;
        }

        public async Task<int> AddUser(User_Auth user_Auth)
        {
            var checkUsername = unitOfWork.GetRepository<User>().AsQueryable().FirstOrDefault(x => x.UserName == user_Auth.Username);

            if(checkUsername != null)
            {
                return 0;
            }

            int roleid = 0;
            if (regexRoleMember.Match(user_Auth.Username).Success)
            {
                roleid = 1;
            }
            else if (regexRoleAdmin.Match(user_Auth.Username).Success)
            {
                roleid = 2;
            }

            if(roleid <= 0 || roleid > 2)
            {
                return 0;
            }

            var user = unitOfWork.GetRepository<User>().Add(new User {
                RoleId =roleid,
                UserName = user_Auth.Username.Trim().Split(" ")[0], 
                Password = Helper.CreateMD5Hash(user_Auth.Password) 
            });

            return (await unitOfWork.SaveChangesAsync()) != 0 ? user.Id : 0;
        }

        public async Task<User_Info> GetUser(int id)
        {
            var user = await unitOfWork.GetRepository<User>().GetAsync(id);
            if(user == null)
            {
                return null;
            }
            return new User_Info
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public User_Info GetUser(string token)
        {
            var user =  unitOfWork.GetRepository<User>().AsQueryable().FirstOrDefault(x => x.Token == token);
            if (user == null)
            {
                return null;
            }
            return new User_Info
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public (int id, string token, DateTime? exp) Login(User_Auth user_Auth)
        {
            var user = unitOfWork.GetRepository<User>().AsQueryable().FirstOrDefault(x => x.UserName == user_Auth.Username && x.Password == Helper.CreateMD5Hash(user_Auth.Password));
            if(user == null)
            {
                return (0,null,null);
            }
            else if(user.ExpirationDate == null && string.IsNullOrEmpty(user.Token))
            {
                return (user.Id, null, null);
            }
            else if (user.ExpirationDate != null && !string.IsNullOrEmpty(user.Token) && user.ExpirationDate.GetValueOrDefault() > DateTime.UtcNow)
            {
                return (user.Id, user.Token, user.ExpirationDate);
            }
            return (user.Id, null, null);
        }

        public async Task<bool> RemoveTokenForUser(string token)
        {
            var user = unitOfWork.GetRepository<User>().AsQueryable().FirstOrDefault(x => x.Token == token);
            if (user == null)
            {
                return false;
            }
            user.Token = null;
            user.ExpirationDate = null;
            return (await unitOfWork.SaveChangesAsync()) != 0 ? true : false;
        }

        public async Task<bool> UpdateUser(string token,string firstname, string lastname)
        {
            var user = unitOfWork.GetRepository<User>().AsQueryable().FirstOrDefault(x => x.Token == token);
            if (user == null)
            {
                return false;
            }
            user.FirstName = firstname;
            user.LastName = lastname;
            return (await unitOfWork.SaveChangesAsync()) != 0 ? true : false;
        }
    }
}
