using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Driver>? Drivers { get; set; }
    
    public DbSet<Vehicle>? Vehicles { get; set; }
    
    public DbSet<ParkingPlaceOwner>? ParkingPlaceOwners { get; set; }
    
    public DbSet<Employee>? Employees { get; set; }
    
    public DbSet<ParkingPlace>? ParkingPlaces { get; set; }
    
    public DbSet<ParkingPlaceImages>? ParkingPlaceImages { get; set; }
    
    public DbSet<ParkingPlaceServices>? ParkingPlaceServices { get; set; }
    
    public DbSet<ParkingPlaceRatings>? ParkingPlaceRatings { get; set; }
    
    public DbSet<SlotCategories>? SlotCategories { get; set; }
    
    public DbSet<ParkingPlaceSlotCapacities>? ParkingPlaceSlotCapacities { get; set; }
    
    public DbSet<Zones>? Zones { get; set; }
    
    public DbSet<Slot>? Slots { get; set; }
    
    public DbSet<SlotReservationHistory>? SlotReservationHistories { get; set; }
    
    public DbSet<Reservation> Reservations { get; set; } = null!;
    
    public DbSet<Ticket>? Tickets { get; set; }
    
    public DbSet<BookingPlan>? BookingPlans { get; set; }
    
    public DbSet<ZonePlan>? ZonePlans { get; set; }
    
    public DbSet<BookingReservation>? BookingReservations { get; set; }
    
    public DbSet<Parking>? BookingParkings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.Driver)
            .WithMany(d => d.Vehicles)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParkingPlaceImages>()
            .HasKey(p => new {p.ParkingPlaceId, p.ImageUrl});

        modelBuilder.Entity<ParkingPlaceServices>()
            .HasKey(p => new {p.ParkingPlaceId, p.ServiceProvide});
        
        modelBuilder.Entity<ParkingPlaceOwner>()
            .HasMany(p=>p.ParkingPlaces)
            .WithOne(p=>p.ParkingPlaceOwner)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParkingPlaceSlotCapacities>()
            .HasKey(k => new {k.ParkingPlaceId, k.SlotCategoryId});

        modelBuilder.Entity<ParkingPlaceRatings>()
            .HasOne(p => p.ParkingPlace)
            .WithMany(p => p.ParkingPlaceRatings)
            .HasForeignKey(pr => pr.ParkingPlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ParkingPlaceRatings>()
            .HasOne(d => d.Driver)
            .WithMany(p => p.ParkingPlaceRatings)
            .HasForeignKey(pr => pr.DriverId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ParkingPlaceSlotCapacities>()
            .HasOne(s => s.SlotCategories)
            .WithMany(p => p.ParkingPlaceSlotCapacities)
            .HasForeignKey(psc => psc.SlotCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ParkingPlaceSlotCapacities>()
            .HasOne( p => p.ParkingPlace)
            .WithMany(p => p.ParkingPlaceSlotCapacities)
            .HasForeignKey(psc => psc.ParkingPlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Zones>()
            .HasOne(p => p.ParkingPlace)
            .WithMany(p => p.Zones)
            .HasForeignKey(z => z.ParkingPlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Slot>()
            .HasOne(p => p.ParkingPlace)
            .WithMany(p => p.Slots)
            .HasForeignKey(z => z.ParkingPlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Slot>()
            .HasOne(p => p.SlotCategories)
            .WithMany(p => p.Slots)
            .HasForeignKey(z => z.SlotCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<SlotReservationHistory>()
            .HasOne(s => s.Slot)
            .WithMany(s => s.SlotReservationHistories)
            .HasForeignKey(s => s.SlotId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Reservation>()
            .HasOne(s => s.Slot)
            .WithMany(s => s.Reservations)
            .HasForeignKey(s => s.SlotId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Reservation>()
            .HasOne(p => p.ParkingPlaceOperator)
            .WithMany(p => p.Reservation)
            .HasForeignKey(e => e.ParkingPlaceOperatorId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Reservation>()
            .HasOne(P => P.ParkingPlace)
            .WithMany(p => p.Reservations)
            .HasForeignKey(e => e.ParkingPlaceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Reservation>()
            .HasOne(v => v.Vehicle)
            .WithMany(v => v.Reservations)
            .HasForeignKey(e => e.VehicleNumber)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<SlotReservationHistory>()
            .HasOne(r => r.Reservation)
            .WithOne(srh => srh.SlotReservationHistory)
            .HasForeignKey<SlotReservationHistory>(srh => srh.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Ticket>()
            .HasOne(op => op.ParkingPlaceOperator)
            .WithMany(t => t.Ticket)
            .HasForeignKey(op => op.VerifiedBy)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookingPlan>()
            .HasOne(p => p.ParkingPlace)
            .WithMany(b => b.BookingPlans)
            .HasForeignKey(p => p.ParkingPlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ZonePlan>()
            .HasKey(k => new {k.ZonePlanId, k.BookingPlanId});
        
        modelBuilder.Entity<ZonePlan>()
            .HasOne(z => z.Zone)
            .WithMany(z => z.ZonePlans)
            .HasForeignKey(z => z.ZoneId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ZonePlan>()
            .HasOne(b => b.BookingPlan)
            .WithMany(z => z.ZonePlans)
            .HasForeignKey(b => b.BookingPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookingReservation>()
            .HasOne(v => v.Vehicle)
            .WithOne(b => b.BookingReservation)
            .HasForeignKey<BookingReservation>(v => v.VehicleNumber)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Parking>()
            .HasKey(k => new {k.BookingReservationId, k.SlotId});
        
        modelBuilder.Entity<Parking>()
            .HasOne(p => p.BookingReservation)
            .WithMany(b => b.Parkings)
            .HasForeignKey(p => p.BookingReservationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Parking>()
            .HasOne(p => p.Slot)
            .WithMany(s => s.Parkings)
            .HasForeignKey(p => p.SlotId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<BookingReservation>()
            .HasOne(z => z.ZonePlan)
            .WithMany(b => b.BookingReservations)
            .HasForeignKey(z => new {z.BookingPlanId, z.ZonePlanId})
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Vehicle>()
            .HasOne(z => z.ZonePlan)
            .WithOne(v => v.Vehicle)
            .HasForeignKey<Vehicle>(z => new {z.BookingPlanId, z.ZonePlanId})
            .OnDelete(DeleteBehavior.Restrict);
    }
}

