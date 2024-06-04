using FinaFlow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaFlow.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(80);

        builder.Property(t => t.Type)
            .IsRequired()
            .HasColumnType("tinyint");
        builder.Property(t => t.Amount)
            .IsRequired()
            .HasColumnType("money");
        builder.Property(t => t.CreatedAt)
            .IsRequired();
        builder.Property(t => t.PaidOrReceivedAt)
            .IsRequired(false);
        builder.Property(t => t.UserdId)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(160);
    }
}
