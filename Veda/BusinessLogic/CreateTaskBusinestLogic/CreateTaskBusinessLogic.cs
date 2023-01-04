using MyTask.Models.Entity;
using MyTask.Models.Request;
using MyTask.Models.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTask.BusinessLogic.CreateTaskBusinestLogic
{
    public class CreateTaskBusinessLogic
    {
        public TaskEntity MapNewTask(long userId, CreateTaskRequest createTaskRequest)
        {
            TaskEntity newTask = new TaskEntity()
            {
                userId = userId,
                topic = createTaskRequest.topic,
                description = createTaskRequest.description,
                coverColorId = createTaskRequest.coverColorId,
                progress = 0,
                dueDate = createTaskRequest.dueDate,
                isCompleted = false,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            };
            return newTask;
        }
        public TaskResponse MapCreateTaskResponse(TaskEntity createTaskResponse, List<TodolistEntity> createTodolistResponse, ColorsEntity color, decimal progress)
        {
            List<TodolistResponse> todolistResponse = new List<TodolistResponse>();
            foreach (TodolistEntity item in createTodolistResponse)
            {
                TodolistResponse mapTodo = new TodolistResponse()
                {
                    id = item.id,
                    description = item.description,
                    isCompleted = item.isCompleted
                };
                todolistResponse.Add(mapTodo);
            }
            string scaleProgressText = CalculateScaleProgress(progress);
            TaskResponse taskResponse = new TaskResponse()
            {
                id = createTaskResponse.id,
                topic = createTaskResponse.topic,
                description = createTaskResponse.description,
                coverCodeColor = color,
                isPublic = createTaskResponse.isPublic,
                progress = progress,
                scaleProgress = scaleProgressText,
                dueDate = createTaskResponse.dueDate,
                todolist = todolistResponse
            };
            return taskResponse;
        }
        public decimal CalculateProress(TaskEntity item)
        {
            int countTodos = item.todolist.Count;
            int countTodoIsSuccess = item.todolist.Count(a => a.isCompleted == true);
            decimal process = (countTodoIsSuccess * 100) / countTodos;
            return process;
        }
        public string CalculateScaleProgress(decimal progress)
        {
            decimal scaleProgress = (progress * 145) / 100;
            if (scaleProgress > 145)
            {
                scaleProgress = 145;
            }
            string scaleProgressText = "w-[" + Math.Round(scaleProgress).ToString() + "px]";
            return scaleProgressText;
        }
    }
}
