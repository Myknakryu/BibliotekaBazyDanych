using BibliotekaProject.Entities;
using BibliotekaProject.Entities.Configurations;

using Microsoft.EntityFrameworkCore;

namespace BibliotekaProject.Context;

public class DatabaseContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<StatusHistory> StatusHistories { get; set; }


    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new PositionConfiguration());
		builder.ApplyConfiguration(new BookConfiguration());
        builder.ApplyConfiguration(new StatusHistoryConfiguration());

		base.OnModelCreating(builder);

        builder.Entity<Author>().ToTable("Authors");
        builder.Entity<Genre>().ToTable("Genres");
        builder.Entity<Position>().ToTable("Positions");
        builder.Entity<Book>().ToTable("Books");
        builder.Entity<Client>().ToTable("Clients");
        builder.Entity<StatusHistory>().ToTable("StatusHistories");

    }
}