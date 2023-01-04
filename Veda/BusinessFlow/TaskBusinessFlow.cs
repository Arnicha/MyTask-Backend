using Microsoft.EntityFrameworkCore.Storage;
using mytask.ExceptionBase;
using mytask.Repository;
using MyTask.BusinessLogic.CreateTaskBusinestLogic;
using MyTask.BusinessLogic.CreateTodolistBusinessLogic;
using MyTask.Models.Entity;
using MyTask.Models.Request;
using MyTask.Models.Response;
using MyTask.Service.Color;
using MyTask.Service.Task;
using MyTask.Service.Todolist;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTask.BusinessFlow
{
    public class TaskBusinessFlow
    {
        private readonly ITaskService taskService;
        private readonly ITodolistService todolistService;
        private readonly IBaseRepository baseRepository;
        private readonly IColorService colorService;
        private readonly CreateTaskBusinessLogic createTaskBusinessLogic;
        private readonly CreateTodolistBusinessLogic createTodolistBusinessLogic;
        public TaskBusinessFlow(ITaskService taskService, ITodolistService todolistService, IBaseRepository baseRepository, IColorService colorService, CreateTaskBusinessLogic createTaskBusinessLogic, CreateTodolistBusinessLogic createTodolistBusinessLogic)
        {
            this.taskService = taskService;
            this.todolistService = todolistService;
            this.baseRepository = baseRepository;
            this.colorService = colorService;
            this.createTaskBusinessLogic = createTaskBusinessLogic;
            this.createTodolistBusinessLogic = createTodolistBusinessLogic;
        }
        public TaskResponse CreateTask(long userId, CreateTaskRequest createTaskRequest)
        {
            IDbContextTransaction transaction = baseRepository.GetBeginTransaction();
            try
            {
                List<TodolistEntity> createTodolistResponse = new List<TodolistEntity>();
                TaskEntity newTask = createTaskBusinessLogic.MapNewTask(userId, createTaskRequest);
                TaskEntity createTaskResponse = taskService.CreateTask(newTask);
                if (createTaskRequest.todolist != null)
                {
                    List<TodolistEntity> newTodolist = createTodolistBusinessLogic.MapNewTodolist(createTaskResponse.id, createTaskRequest.todolist);
                    createTodolistResponse = todolistService.CreateTodolist(newTodolist);
                }
                ColorsEntity color = colorService.GetColorByCoverColorId(createTaskResponse.coverColorId);
                decimal progressPercent = createTaskBusinessLogic.CalculateProress(createTaskResponse);
                TaskResponse taskResponse = createTaskBusinessLogic.MapCreateTaskResponse(createTaskResponse, createTodolistResponse, color, progressPercent);
                baseRepository.Commit(transaction);
                return taskResponse;
            }
            catch
            {
                baseRepository.RollBack(transaction);
                throw;
            }
        }
        public List<TaskResponse> GetAllTasksByUserId(long userId)
        {
            List<TaskEntity> tasks = new List<TaskEntity>();
            List<TaskResponse> tasksResponse = new List<TaskResponse>();
            try
            {
                tasks = taskService.GetAllTasksByUserIdIncludeTodolist(userId);
                foreach (TaskEntity item in tasks)
                {
                    ColorsEntity color = colorService.GetColorByCoverColorId(item.coverColorId);
                    decimal progressPercent = createTaskBusinessLogic.CalculateProress(item);
                    TaskResponse taskResponse = createTaskBusinessLogic.MapCreateTaskResponse(item, item.todolist, color, progressPercent);
                    tasksResponse.Add(taskResponse);
                }
            }
            catch
            {
                throw;
            }
            return tasksResponse;
        }
        public TaskResponse GetTaskByTaskId(long userId, long taskId)
        {
            TaskResponse taskResponse = new TaskResponse();
            TaskEntity task = new TaskEntity();
            try
            {
                task = taskService.GetTasksByTaskIdIncludeTodolist(userId, taskId);
                ColorsEntity color = colorService.GetColorByCoverColorId(task.coverColorId);
                decimal progressPercent = createTaskBusinessLogic.CalculateProress(task);
                taskResponse = createTaskBusinessLogic.MapCreateTaskResponse(task, task.todolist, color, progressPercent);
            }
            catch
            {
                throw;
            }
            return taskResponse;
        }
        public List<TaskResponse> GetAllTasksPublishByUserId(long userId)
        {
            List<TaskEntity> tasks = new List<TaskEntity>();
            List<TaskResponse> tasksResponse = new List<TaskResponse>();
            try
            {
                tasks = taskService.GetAllTasksPublishByUserIdIncludeTodolist(userId);
                foreach (TaskEntity item in tasks)
                {
                    ColorsEntity color = colorService.GetColorByCoverColorId(item.coverColorId);
                    TaskResponse taskResponse = createTaskBusinessLogic.MapCreateTaskResponse(item, item.todolist, color);
                    tasksResponse.Add(taskResponse);
                }
            }
            catch
            {
                throw;
            }
            return tasksResponse;
        }
    }
}