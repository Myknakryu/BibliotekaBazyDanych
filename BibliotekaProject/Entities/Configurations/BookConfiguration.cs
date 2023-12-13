using Microsoft.EntityFrameworkCore;
using BibliotekaProject.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BibliotekaProject.Entities.Configurations;

class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasOne(x => x.Position)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.IdPosition)
            .OnDelete(DeleteBehavior.Restrict);
    }
}