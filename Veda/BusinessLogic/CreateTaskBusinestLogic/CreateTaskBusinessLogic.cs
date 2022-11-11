using MyTask.Models.Entity;
using MyTask.Models.Request;
using MyTask.Models.Response;
using System;
using System.Collections.Generic;

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
        public TaskResponse MapCreateTaskResponse(TaskEntity createTaskResponse, List<TodolistEntity> createTodolistResponse, ColorsEntity color)
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
            TaskResponse taskResponse = new TaskResponse()
            {
                id = createTaskResponse.id,
                topic = createTaskResponse.topic,
                description = createTaskResponse.description,
                coverCodeColor = color,
                dueDate = createTaskResponse.dueDate,
                todolist = todolistResponse
            };
            return taskResponse;
        }
    }
}
