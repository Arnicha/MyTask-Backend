using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTask.Models.Entity
{
    [Table("colors", Schema = "mytask")]
    public class ColorsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]
        [Column("id")]
        public int id { get; set; }
        
        [Column("code_color")]
        public string codeColor { get; set; }
    }
}
