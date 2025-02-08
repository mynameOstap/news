using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;


namespace Model
{
    [Table("NewsArticles")]public class NewsArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]public int Id { get; set; }  
        [Column("title")]public string Title { get; set; }
        [Column("author")]public string Author { get; set; }
        [Column("content")]public string Content { get; set; } 
        [Column("source")]public Source Source { get; set; }
        [Column("description")]public string Description { get; set; }
        [Column("url")]public string Url { get; set; } 
        [Column("publishedAt")]public DateTime? PublishedAt { get; set; } 
   
        [Column("UrlToImage")]public string UrlToImage { get; set; }
        [JsonIgnore] 
        
        public ICollection<Comment> Comments { get; set; }
        public ICollection<SavedArticle> SavedArticles { get; set; } 

    }
}
[Owned]
public class Source
{
    
    public string Name { get; set; }
}
