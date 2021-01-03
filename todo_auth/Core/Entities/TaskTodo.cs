using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class TaskTodo : BaseEntity
    {
        public string Title { get; set; }
        public DateTime End_At { get; set; }
        public int UserId { get; set; }
        public bool isDone { get; set; }
        public virtual User User { get; set; }
    }
}
