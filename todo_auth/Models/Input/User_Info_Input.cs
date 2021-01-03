using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo_auth.Models.Input
{
    public class User_Info_Input
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
    }
}
