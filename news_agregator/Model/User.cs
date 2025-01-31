using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Model
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Ім'я користувача обов'язкове")]
        [Column("name")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий")]
        [Column("hashPassword")] 
        [JsonPropertyName("password")]
        public string HashPassword { get; set; }

        [Column("administrator")] 
        public bool Administrator { get; set; }

        [Column("createdAt")] 
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual ICollection<Comment> Comments { get; set; }
        
        [JsonIgnore]
        [ValidateNever]
        public virtual ICollection<SavedArticle> SavedArticles { get; set; }
        
        [JsonIgnore]
        [ValidateNever]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}

