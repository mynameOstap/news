using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Comments")]public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]public int Id { get; set; }
        [Column("articleId")]public int ArticleId { get; set; } 
        [Column("userId")]public int UserId { get; set; } 
        [Column("text")]public string Text { get; set; } 
        [Column("createdAt")]public DateTime CreatedAt { get; set; }
    
        public User User { get; set; }
        public NewsArticle NewsArticle { get; set; }
    }
}

