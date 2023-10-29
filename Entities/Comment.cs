namespace BlazorWebApp.Entities;

public class Comment : Entity<int>
{
    public int PostID { get; set; }
    public int UserID { get; set; }
    public string Content { get; set; }
    public DateTime CommentDate { get; set; }
    
    // Navigation properties
    public Post Post { get; set; }
    public User User { get; set; }
}