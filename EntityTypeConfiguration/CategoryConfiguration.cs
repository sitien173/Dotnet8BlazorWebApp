﻿using BlazorWebApp.Entities;

namespace BlazorWebApp.EntityTypeConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
