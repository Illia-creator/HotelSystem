using HotelSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.Persistence.DbContexts;
public class HotelSystemDbContext : DbContext
{
    public HotelSystemDbContext(DbContextOptions<HotelSystemDbContext> options) : base(options)
    {

    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Reservation> Resevations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(r =>
        {
            r.HasKey(r => r.Id);

            r
            .HasMany(a => a.Reservations)
            .WithOne(r => r.Room)
            .HasForeignKey(r => r.RoomId);
        });

        modelBuilder.Entity<Hotel>(h =>
        {
            h.HasKey(h => h.Id);

            h
            .HasMany(r => r.Rooms)
            .WithOne(h => h.Hotel)
            .HasForeignKey(r => r.HotelId);
        });

        modelBuilder.Entity<RoomType>(r =>
        {
            r.HasKey(r => r.Id);

            r
            .HasMany(r => r.Rooms)
            .WithOne(h => h.RoomType)
            .HasForeignKey(h => h.RoomTypeId);
        });

        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(u => u.Id);

            u
           .HasMany(u => u.Reservations)
           .WithOne(p => p.User)
           .HasForeignKey(p => p.UserId);
        });

        modelBuilder.Entity<Payment>(p =>
        {
            p.HasKey(p => p.ReservationId);

            p.HasOne(p => p.Reservation);

        });

    }
}
