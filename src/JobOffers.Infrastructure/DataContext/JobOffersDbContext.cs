using JobOffers.Infrastructure.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobOffers.Infrastructure.DataContext;

public class JobOffersDbContext(DbContextOptions<JobOffersDbContext> options) : DbContext(options)
{
    public const string CONNECTION_STRING_NAME = "JobOffersDb";

    public DbSet<JobOffer> JobOffers { get; set; } = null!;

    public static void SetDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
    {
        optionsBuilder.UseSqlite(configuration.GetConnectionString(CONNECTION_STRING_NAME));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.ConfigureWarnings(w => w.Log(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.MultipleCollectionIncludeWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobOffer>(entity =>
        {
            entity.ToTable("job_offers");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Source);

            entity.Property(e => e.ExternalId);

            entity.Property(e => e.Title);

            entity.Property(e => e.Description);

            entity.Property(e => e.CompanyName);

            entity.Property(e => e.Location);

            entity.Property(e => e.PostedAt)
                .HasColumnType("datetime");

            entity.HasIndex(e => new { e.Source, e.ExternalId })
                .IsUnique();
        });
    }
}