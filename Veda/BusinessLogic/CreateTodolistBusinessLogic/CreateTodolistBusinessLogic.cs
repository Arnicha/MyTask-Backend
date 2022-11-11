using MyTask.Models.Entity;
using MyTask.Models.Request;
using System.Collections.Generic;

namespace MyTask.BusinessLogic.CreateTodolistBusinessLogic
{
    public class CreateTodolistBusinessLogic
    {
        public List<TodolistEntity> MapNewTodolist(long taskId, List<TodolistRequest> newTodolist)
        {
            List<TodolistEntity> todoList = new List<TodolistEntity>();
            foreach (TodolistRequest todo in newTodolist)
            {
                TodolistEntity newTodo = new TodolistEntity()
                {
                    taskId = taskId,
                    description = todo.description,
                    isCompleted = todo.isCompleted
                };
                todoList.Add(newTodo);
            }
            return todoList;
        }
    }
}
