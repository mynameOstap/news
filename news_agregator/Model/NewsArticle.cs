using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    [Table("NewsArticles")]public class NewsArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]public int Id { get; set; }  
        [Column("title")]public string Title { get; set; } 
        [Column("content")]public string Content { get; set; } 
        [Column("source")]public string Source { get; set; } 
        [Column("url")]public string Url { get; set; } 
        [Column("publishedAt")]public DateTime PublishedAt { get; set; } 
        [Column("category")]public List<string> Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

