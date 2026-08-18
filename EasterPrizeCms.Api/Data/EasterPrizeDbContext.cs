using EasterPrizeCms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasterPrizeCms.Api.Data;

public class EasterPrizeDbContext : DbContext
{
    public EasterPrizeDbContext(DbContextOptions<EasterPrizeDbContext> options)
        : base(options) { }

    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<Prize> Prizes => Set<Prize>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(participant => participant.Id);

            entity.Property(participant => participant.FullName).IsRequired().HasMaxLength(80);

            entity.Property(participant => participant.City).IsRequired().HasMaxLength(80);
        });

        modelBuilder.Entity<Prize>(entity =>
        {
            entity.HasKey(prize => prize.Id);

            entity.Property(prize => prize.Name).IsRequired().HasMaxLength(80);

            entity.Property(prize => prize.Value).HasPrecision(18, 2);

            entity.Property(prize => prize.Status).HasConversion<string>().IsRequired();

            entity
                .HasOne<Participant>()
                .WithMany()
                .HasForeignKey(prize => prize.ParticipantId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
