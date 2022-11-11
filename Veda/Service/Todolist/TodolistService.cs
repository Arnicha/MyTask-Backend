using mytask.Repository;
using MyTask.Models.Entity;
using System.Collections.Generic;

namespace MyTask.Service.Todolist
{
    public class TodolistService : ITodolistService
    {
        private readonly IBaseRepository baseRepository;
        public TodolistService(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public List<TodolistEntity> CreateTodolist(List<TodolistEntity> todoList)
        {
            List<TodolistEntity> createTodolistResponse = baseRepository.CreateList(todoList);
            return createTodolistResponse;
        }
    }
}
