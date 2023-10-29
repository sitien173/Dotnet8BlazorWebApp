namespace BlazorWebApp.Entities;

public class Category : Entity<int>
{
    public string Name { get; set; }
    
    // Navigation properties
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}