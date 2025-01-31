

namespace Model
{
    public class UserDto
    {
        public string Name { get; set;}
        public string HashPassword { get; set;}
        public bool Administrator { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

