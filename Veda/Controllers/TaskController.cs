using Microsoft.AspNetCore.Mvc;
using mytask.Controllers;
using MyTask.BusinessFlow;
using MyTask.Models.Entity;
using MyTask.Models.Request;
using MyTask.Models.Response;
using System.Collections.Generic;

namespace MyTask.Controllers
{
    public class TaskController : BaseController
    {

        private readonly TaskBusinessFlow taskBusinessFlow;
        public TaskController(TaskBusinessFlow taskBusinessFlow)
        {
            this.taskBusinessFlow = taskBusinessFlow;
        }

        [HttpPost("api/v1/task")]
        public TaskResponse CreateTask([FromBody] CreateTaskRequest createTaskRequest)
        {
            long userId = GetUserId();
            return taskBusinessFlow.CreateTask(userId, createTaskRequest);
        }

        [HttpGet("api/v1/tasks")]
        public List<TaskResponse> GetAllTasksByUserId()
        {
            long userId = GetUserId();
            return taskBusinessFlow.GetAllTasksByUserId(userId);
        }

        [HttpGet("api/v1/tasks/{taskId}")]
        public TaskResponse GetTaskByTaskId(long taskId)
        {
            long userId = GetUserId();
            return taskBusinessFlow.GetTaskByTaskId(userId, taskId);
        }

        [HttpGet("api/v1/tasks/publish")]
        public List<TaskResponse> GetAllTasksPublishByUserId()
        {
            long userId = GetUserId();
            return taskBusinessFlow.GetAllTasksPublishByUserId(userId);
        }

    }
}
