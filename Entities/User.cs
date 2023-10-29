namespace BlazorWebApp.Entities;

public class User : Entity<int>
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    // Navigation properties
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}