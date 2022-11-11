using MyTask.Models.Entity;
using System;
using System.Collections.Generic;

namespace MyTask.Models.Response
{
    public class TaskResponse
    {
        public long id { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public ColorsEntity coverCodeColor { get; set; }
        public DateTime dueDate { get; set; }
        public List<TodolistResponse> todolist { get; set; }
    }
}
