using mytask.Repository;
using MyTask.Models.Entity;
using System.Collections.Generic;
using mytask.ExceptionBase;

namespace MyTask.Service.Task
{
    public class TaskService : ITaskService
    {
        private readonly IBaseRepository baseRepository;
        public TaskService(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public TaskEntity CreateTask(TaskEntity newTask)
        {
            TaskEntity createTaskResponse = baseRepository.Create(newTask);
            return createTaskResponse;
        }
        public List<TaskEntity> GetAllTasksByUserIdIncludeTodolist(long userId)
        {
            List<TaskEntity> tasks = baseRepository.GetInclude<TaskEntity>(null, filter: a => a.userId == userId, includeProperties: "todolist");
            return tasks;
        }

        public TaskEntity GetTasksByTaskIdIncludeTodolist(long userId, long taskId)
        {
            TaskEntity task = baseRepository.GetItemInclude<TaskEntity>(filter: a => a.userId == userId && a.id == taskId, includeProperties: "todolist");
            if (task == null)
            {
                throw new NotFoundException("This user doesn’t have taskId");
            }
            return task;
        }
    }
}
