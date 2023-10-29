using BlazorWebApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BlazorWebApp.EntityTypeConfiguration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(p => p.Content)
            .IsRequired();
        
        builder.Property(p => p.PublishedDate)
            .IsRequired();
        
        builder.Property(p => p.IsPublished)
            .IsRequired();

        builder.HasOne(p => p.Author)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.AuthorID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
