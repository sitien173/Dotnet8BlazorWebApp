namespace BlazorWebApp.Entities;

public class Post : Entity<int>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorID { get; set; }
    public int CategoryID { get; set; }
    public DateTime PublishedDate { get; set; }
    public bool IsPublished { get; set; }
    
    // Navigation properties
    public User Author { get; set; }
    public Category Category { get; set; }
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}