using MyTask.Models.Entity;
using System.Collections.Generic;

namespace MyTask.Service.Todolist
{
    public interface ITodolistService
    {
        List<TodolistEntity> CreateTodolist(List<TodolistEntity> todoList);
    }
}
