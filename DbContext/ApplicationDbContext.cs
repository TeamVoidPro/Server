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
    }
}

