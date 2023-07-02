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
    }
}

