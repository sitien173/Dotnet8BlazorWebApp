using BlazorWebApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BlazorWebApp.EntityTypeConfiguration;

public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder.HasKey(pt => pt.Id);
        
        builder.Property(pt => pt.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
