using System;
using System.Collections.Generic;

namespace MyTask.Models.Request
{
    public class CreateTaskRequest
    {
        public string topic { get; set; }
        public string description { get; set; }
        public int coverColorId { get; set; }
        public DateTime dueDate { get; set; }
        public List<TodolistRequest> todolist { get; set;}
    }
}
