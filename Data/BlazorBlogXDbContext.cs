using BlazorWebApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApp.Data;
public class BlazorBlogXDbContext : DbContext
{
    public BlazorBlogXDbContext(DbContextOptions<BlazorBlogXDbContext> options) : base(options)
    {
    }

    // Define DbSet properties for each entity
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<Comment> Comments { get; set; }

    // Override OnModelCreating to apply entity configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This ensures that EF Core accesses enum properties using their underlying fields.
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
        modelBuilder.ApplyEnumToStringConversion();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}