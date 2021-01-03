using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo_auth.Models.Input
{
    public class Task_Input
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(1000,MinimumLength = 1)]
        public string Title { get; set; }
        [Required]
        public DateTime End_At { get; set; }
    }
}
