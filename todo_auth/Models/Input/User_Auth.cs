using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo_auth.Models.Input
{
    public class User_Auth
    {
        [StringLength(50,MinimumLength = 6)]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [StringLength(50, MinimumLength = 8)]
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
