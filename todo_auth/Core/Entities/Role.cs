using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_auth.Core.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
    }
}
