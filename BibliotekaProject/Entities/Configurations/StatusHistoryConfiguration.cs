using Microsoft.EntityFrameworkCore;
using BibliotekaProject.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BibliotekaProject.Entities.Configurations;

class StatusHistoryConfiguration : IEntityTypeConfiguration<StatusHistory>
{
    public void Configure(EntityTypeBuilder<StatusHistory> builder)
    {
        builder.HasOne(x => x.Client)
            .WithMany(x => x.StatusHistories)
            .HasForeignKey(x => x.IdClient)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Book)
            .WithMany(x => x.StatusHistories)
            .HasForeignKey(x => x.IdBook)
            .OnDelete(DeleteBehavior.Restrict);
    }
}