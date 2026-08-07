using Microsoft.EntityFrameworkCore;
using MiniHittegods.Domain.Entities;

namespace MiniHittegods.Api.Data;

public class MiniHittegodsDbContext : DbContext
{
    public MiniHittegodsDbContext(
        DbContextOptions<MiniHittegodsDbContext> options)
        : base(options)
    {
    }

    public DbSet<FoundItem> FoundItems { get; set; }
}