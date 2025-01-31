using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("SavedArticle")]public class SavedArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]public int Id { get; set; } 
        [Column("userId")]public int UserId { get; set; } 
        [Column("articleId")]public int ArticleId { get; set; } 
        [Column("savedAt")]public DateTime SavedAt { get; set; } 
        public User User { get; set; }
    }
}

