using MyTask.Models.Entity;
using System.Collections.Generic;

namespace MyTask.Service.Task
{
    public interface ITaskService
    {
        TaskEntity CreateTask(TaskEntity newTask);
        List<TaskEntity> GetAllTasksByUserIdIncludeTodolist(long userId);
        TaskEntity GetTasksByTaskIdIncludeTodolist(long userId, long taskId);
    }
}
