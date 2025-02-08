using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model
{
    [Table("SavedArticle")]public class SavedArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]public int Id { get; set; } 
        [Column("userId")]public int UserId { get; set; } 
        [ForeignKey("NewsArticle")]
        public int ArticleId { get; set; }
        [Column("savedAt")]public DateTime SavedAt { get; set; } 
        public User User { get; set; }
        [JsonIgnore] 
        public NewsArticle NewsArticle { get; set; }
    }
    
    public class SavedArticleResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public DateTime SavedAt { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleAuthor { get; set; }
        public string ArticleUrl { get; set; }
    }
}

