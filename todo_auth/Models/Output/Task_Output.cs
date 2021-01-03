using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_auth.Models.Output
{
    public class Task_Output
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime End_At { get; set; }
        public bool IsDone { get; set; }
    }
}
