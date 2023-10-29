namespace BlazorWebApp.Entities;

public class PostTag : Entity<int>
{
    public int PostID { get; set; }
    public int TagID { get; set; }
    
    public Post Post { get; set; }
    public Tag Tag { get; set; }
}