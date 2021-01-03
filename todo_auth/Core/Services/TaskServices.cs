using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_auth.Core.Interfaces;
using todo_auth.Models.Input;
using todo_auth.Models.Output;

namespace todo_auth.Core.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly IUnitOfWork unitOfWork;
        public TaskServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Task_Output> AddTodo(Task_Input task_Input)
        {
            var task = new TaskTodo
            {
                Title = task_Input.Title.Trim(),
                isDone = false,
                End_At = task_Input.End_At,
                UserId = task_Input.UserId
            };
            unitOfWork.GetRepository<TaskTodo>().Add(task);

            var rs = await unitOfWork.SaveChangesAsync();
            if(rs <= 0)
            {
                return null;
            }
            return new Task_Output
            {
                Id= task.Id,
                Title = task_Input.Title,
                IsDone = false,
                End_At = task_Input.End_At
            };
        }

        public List<Task_Output> GetAllOfUser(int userid)
        {
            var list = unitOfWork.GetRepository<TaskTodo>().AsQueryable().Where(x => x.UserId == userid).ToList();
            if(list.Count <= 0)
            {
                return null;
            }
            var newListTodo = new List<Task_Output>();
            list.ForEach(x => newListTodo.Add(new Task_Output
            {
                Id=x.Id,
                Title = x.Title,
                End_At = x.End_At,
                IsDone = x.isDone
            }));
            return newListTodo;
        }

        public async Task<bool> RemoveTask(int taskId)
        {
            var task = await unitOfWork.GetRepository<TaskTodo>().GetAsync(taskId);
            if (task == null)
            {
                return false;
            }
            unitOfWork.GetRepository<TaskTodo>().Delete(task);
            return await unitOfWork.SaveChangesAsync() != 0 ? true : false;
        }

        public async Task<bool> TaskIsCompleted(int taskId)
        {
            var task = await unitOfWork.GetRepository<TaskTodo>().GetAsync(taskId);
            if(task == null)
            {
                return false;
            }
            task.isDone = !task.isDone;
            return await unitOfWork.SaveChangesAsync() != 0 ? true : false;
        }
    }
}
