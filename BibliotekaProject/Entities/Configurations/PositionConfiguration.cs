using Microsoft.EntityFrameworkCore;
using BibliotekaProject.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BibliotekaProject.Entities.Configurations;

class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasOne(x => x.Genre)
            .WithMany(x => x.Positions)
            .HasForeignKey(x => x.IdGenre)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.Authors)
            .WithMany(x => x.Positions);
    }
}