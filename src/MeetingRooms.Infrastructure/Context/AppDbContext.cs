using MeetingRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingRooms.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(user => user.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Room>()
            .Property(room => room.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Reserve>()
            .Property(room => room.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<Reserve> Reserves { get; set; }
}