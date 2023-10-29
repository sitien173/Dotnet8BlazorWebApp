namespace BlazorWebApp.Entities;

public class Tag : Entity<int>
{
    public string Name { get; set; }
    // Navigation properties
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}