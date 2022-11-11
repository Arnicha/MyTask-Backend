using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTask.Models.Entity
{
    [Table("healthcheck", Schema = "mytask")]
    public class HealthCheckEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]

        [Column("id")]
        public int id { get; set; }

        [Column("message")]
        public string statusMessage { get; set; }
    }
}
