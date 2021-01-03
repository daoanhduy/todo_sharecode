using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_auth.Models.Input;
using todo_auth.Models.Output;

namespace todo_auth.Core.Interfaces
{
    public interface ITaskServices
    {
        Task<Task_Output> AddTodo(Task_Input task_Input);
        List<Task_Output> GetAllOfUser(int userid);
        Task<bool> TaskIsCompleted(int taskId);
        Task<bool> RemoveTask(int taskId);
    }
}
