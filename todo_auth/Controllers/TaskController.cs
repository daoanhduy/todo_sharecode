using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_auth.Common;
using todo_auth.Core.Interfaces;
using todo_auth.Models.Input;
using todo_auth.Models.Output;

namespace todo_auth.Controllers
{
    public class TaskController : BaseApiController
    {
        private readonly ITaskServices taskServices;
        public TaskController(ITaskServices taskServices)
        {
            this.taskServices = taskServices;
        }

        [Authorize]
        [ValidateToken]
        [HttpGet]
        public IActionResult ListTasksOfUser(int userId)
        {
            var list = taskServices.GetAllOfUser(userId);
            if(list == null)
            {
                return Ok(new { data = new Task_Output[] { } });
            }
            return Ok(new { data = list });
        }

        [Authorize]
        [ValidateToken]
        [HttpPost]
        public async Task<IActionResult> Add(Task_Input task_Input)
        {
            var task = await taskServices.AddTodo(task_Input);
            if (task == null)
            {
                return UnprocessableEntity();
            }
            return Ok(new { data = task });
        }

        [Authorize]
        [ValidateToken]
        [HttpPut]
        public async Task<IActionResult> CompleteTask(int taskid)
        {
            var task = await taskServices.TaskIsCompleted(taskid);
            if (task == false)
            {
                return UnprocessableEntity();
            }
            
            return Ok();
        }

        [Authorize]
        [ValidateToken]
        [HttpDelete]
        public async Task<IActionResult> RemoveTask(int taskid)
        {
            var task = await taskServices.RemoveTask(taskid);
            if (task == false)
            {
                return UnprocessableEntity();
            }

            return Ok();
        }
    }
}
