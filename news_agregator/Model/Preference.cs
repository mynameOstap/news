using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Preference")]public class Preference
    {
        [Column("id")]public int Id { get; set; } 
        [Column("userId")]public int UserId { get; set; } 
        [Column("category")]public string Category { get; set; } 
        [Column("Language")]public string Language { get; set; } 
        [Column("Country")]public string Country { get; set; } 
        public User User { get; set; }
    }
}

