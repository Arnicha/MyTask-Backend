using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTask.Models.Entity
{
    [Table("tasks", Schema = "mytask")]
    public class TaskEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]
        [Column("id")]
        public long id { get; set; }
        [Column("user_id")]
        public long userId { get; set; }
        [Column("topic")]
        public string topic { get; set; }
        [Column("description")]
        public string description { get; set; }
        [Column("progress")]
        public decimal progress { get; set; }
        [Column("is_completed")]
        public bool isCompleted { get; set; }
        [Column("cover_color_id")]
        public int coverColorId { get; set; }
        [Column("is_public")]
        public bool isPublic { get; set; }
        [Column("due_date")]
        public DateTime dueDate { get; set; }
        [Column("created_at")]
        public DateTime createdAt { get; set; }
        [Column("updated_at")]
        public DateTime updatedAt { get; set; }
        [ForeignKey("taskId")]
        public List<TodolistEntity> todolist { get; set; }
    }
}
