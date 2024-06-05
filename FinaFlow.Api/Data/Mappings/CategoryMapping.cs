using FinaFlow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaFlow.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(80);

        builder.Property(c => c.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(255);

        builder.Property(c => c.UserId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(160);
    }
}
