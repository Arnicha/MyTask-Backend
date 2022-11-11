using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTask.Models.Entity
{
    [Table("todolist", Schema = "mytask")]
    public class TodolistEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]
        [Column("id")]
        public long id { get; set; }
        [Column("task_id")]
        public long taskId { get; set; }
        [Column("description")]
        public string description { get; set; }
        [Column("is_completed")]
        public bool isCompleted { get; set; }
    }
}
