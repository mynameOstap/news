using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]public int Id { get; set; } 
        [Column("userId")]public int UserId { get; set; } 
        [Column("type")]public string Type { get; set; } 
        [Column("setting")]public string Settings { get; set; }
        public User User { get; set; }
    }
}

