using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("User")]public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] public int Id { get; set;}
        [Column("name")] public string Name { get; set;}
        [Column("hashPassword")] public string HashPassword { get; set;}
        [Column("administrator")] public bool Administrator { get; set; }
        [Column("createdAt")] public DateTime CreatedAt { get; set; }
        public Preference Preference { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<SavedArticle> SavedArticles { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}

