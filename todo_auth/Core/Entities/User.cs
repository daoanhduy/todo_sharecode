using Entities.Base;
using System;
using todo_auth.Core.Entities;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
