using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUserEntity>(options)
{
    public virtual DbSet<BookingEntity> Bookings { get; set; }
    public virtual DbSet<CoolingRoomEntity> CoolingRooms { get; set; }
    public virtual DbSet<CoolingRoomPriceControlEntity> CoolingRoomPriceControls { get; set; }
    public virtual DbSet<CoolingRoomStatusEntity> CoolingRoomStatuses { get; set; }
    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<PaymentControlEntity> PaymentControls { get; set; }
    public virtual DbSet<PaymentStatusEntity> PaymentStatuses { get; set; }
    public virtual DbSet<PaymentMethodEntity> PaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Identity yapılarını korur

        // ApplicationUserEntity - BookingEntity (1-to-many)
        builder.Entity<BookingEntity>()
            .HasOne(b => b.ApplicationUser)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // CustomerEntity - BookingEntity (1-to-many)
        builder.Entity<BookingEntity>()
            .HasOne(b => b.Customer)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // CoolingRoomEntity - BookingEntity (1-to-many)
        builder.Entity<BookingEntity>()
            .HasOne(b => b.CoolingRoom)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.CoolingRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // PaymentControlEntity - BookingEntity (1-to-many)
        builder.Entity<BookingEntity>()
            .HasOne(b => b.PaymentControl)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PaymentControlId)
            .OnDelete(DeleteBehavior.Cascade);

        // PaymentMethodEntity - PaymentControlEntity (1-to-many)
        builder.Entity<PaymentControlEntity>()
            .HasOne(p => p.PaymentMethod)
            .WithMany(m => m.PaymentControls)
            .HasForeignKey(p => p.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        // PaymentStatusEntity - PaymentControlEntity (1-to-many)
        builder.Entity<PaymentControlEntity>()
            .HasOne(p => p.PaymentStatus)
            .WithMany(s => s.PaymentControls)
            .HasForeignKey(p => p.PaymentStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // CoolingRoomPriceControlEntity - CoolingRoomEntity (1-to-many)
        builder.Entity<CoolingRoomEntity>()
            .HasOne(r => r.CoolingRoomPriceControl)
            .WithMany(p => p.CoolingRooms)
            .HasForeignKey(r => r.CoolingRoomPriceControlId)
            .OnDelete(DeleteBehavior.Restrict);

        // CoolingRoomStatusEntity - CoolingRoomEntity (1-to-many)
        builder.Entity<CoolingRoomEntity>()
            .HasOne(r => r.CoolingRoomStatus)
            .WithMany(s => s.CoolingRooms)
            .HasForeignKey(r => r.CoolingRoomStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
