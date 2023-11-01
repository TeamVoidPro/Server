using Microsoft.EntityFrameworkCore;
using server.Helpers;
using Server.Models;

namespace Server.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
    
    public DbSet<AwaitedParkingPlaces> AwaitedParkingPlaces { get; set; } = null!;
    
    public DbSet<ComplianceMonitoring> ComplianceMonitoring { get; set; } = null!;
    
    public DbSet<Issues> Issues { get; set; } = null!;
    
    public DbSet<IssueImages>? IssueImages { get; set; }
    
    public DbSet<SlotRatings>? SlotRatings { get; set; }

    public DbSet<RefreshToken>? RefreshTokens { get; set; }
    
    public DbSet<VerificationCodes>? VerificationCodes { get; set; }
    
    public DbSet<OnsiteReservations>? OnsiteReservations { get; set; }

    public DbSet<OnsiteReservations>? OnlineReservations { get; set; }

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
        
        modelBuilder.Entity<ParkingPlace>()
            .HasOne(p => p.ParkingPlaceOwner)
            .WithMany(p => p.ParkingPlaces)
            .HasForeignKey(p => p.ParkingPlaceOwnerId)
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

        // modelBuilder.Entity<Reservation>()
        //     .HasOne(p => p.ParkingPlaceOperator)
        //     .WithMany(p => p.Reservation)
        //     .HasForeignKey(e => e.ParkingPlaceOperatorId)
        //     .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Reservation>()
            .HasOne(P => P.ParkingPlace)
            .WithMany(p => p.Reservations)
            .HasForeignKey(e => e.ParkingPlaceId)
            .OnDelete(DeleteBehavior.Restrict);

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
        
        modelBuilder.Entity<AwaitedParkingPlaces>()
            .HasOne(o => o.ParkingPlaceOwner)
            .WithMany(a => a.AwaitedParkingPlaces)
            .HasForeignKey(o => o.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<AwaitedParkingPlaces>()
            .HasOne(pv => pv.ParkingPlaceVerifier)
            .WithMany(a => a.AwaitedParkingPlaces)
            .HasForeignKey(pv => pv.ParkingPlaceVerifierId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<ComplianceMonitoring>()
            .HasOne(v => v.ParkingPlace)
            .WithMany(c => c.ComplianceMonitoring)
            .HasForeignKey(v => v.ParkingPlaceId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<ComplianceMonitoring>()
            .HasOne(v => v.ParkingPlaceVerifier)
            .WithMany(c => c.ComplianceMonitoring)
            .HasForeignKey(v => v.ParkingPlaceVerifierId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Issues>()
            .HasOne(p => p.ParkingPlace)
            .WithMany(i => i.Issues)
            .HasForeignKey(p => p.ParkingPlaceId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Issues>()
            .HasOne(d => d.Driver)
            .WithMany(i => i.Issues)
            .HasForeignKey(d => d.ReportedBy)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Issues>()
            .HasOne(op => op.ParkingPlaceVerifier)
            .WithMany(i => i.Issues)
            .HasForeignKey(op => op.ParkingPlaceVerifierId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<IssueImages>()
            .HasKey(k => new { k.IssueId, k.Image });
        
        modelBuilder.Entity<IssueImages>()
            .HasOne(i => i.Issues)
            .WithMany(i => i.IssueImages)
            .HasForeignKey(i => i.IssueId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<SlotRatings>()
            .HasOne(d => d.Driver)
            .WithMany(s => s.SlotRatings)
            .HasForeignKey(d => d.DriverId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<SlotRatings>()
            .HasOne(s => s.Slot)
            .WithMany(s => s.SlotRatings)
            .HasForeignKey(s => s.SlotId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<SlotRatings>()
            .HasKey(k => new {k.DriverId, k.SlotId});
        
        modelBuilder.Entity<Employee>()
            .HasMany(r => r.RefreshToken)
            .WithOne(e => e.Employee)
            .HasForeignKey(r => r.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Driver>()
            .HasMany(d => d.RefreshTokens)
            .WithOne(d => d.Driver)
            .HasForeignKey(r => r.DriverId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<OnsiteReservations>()
            .HasOne(p => p.ParkingPlaceOperator)
            .WithMany(o => o.OnsiteReservations)
            .HasForeignKey(p => p.ParkingPlaceOperatorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Slot>()
            .HasOne(z => z.Zones)
            .WithMany(s => s.Slots)
            .HasForeignKey(s => s.ZoneId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<OnsiteReservations>()
            .HasOne(r => r.Reservation)
            .WithOne(o => o.OnsiteReservations)
            .HasForeignKey<OnsiteReservations>(r => r.OnsiteReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookingReservation>()
            .HasOne(r => r.Reservation)
            .WithOne(b => b.BookingReservation)
            .HasForeignKey<BookingReservation>(r => r.BookingReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OnlineReservations>()
            .HasOne(r => r.Vehicle)
            .WithMany(o => o.OnlineReservations)
            .HasForeignKey(r => r.VehicleNumber)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<OnlineReservations>()
            .HasOne(r => r.Reservation)
            .WithOne(o => o.OnlineReservations)
            .HasForeignKey<OnlineReservations>(r => r.OnlineReservationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        

        modelBuilder.Entity<Employee>()
            .HasData(
                // Administrator
                new Employee
                {
                    EmployeeId = "EMP_0023_4590",
                    FirstName = "Viharsha",
                    LastName = "Pramodi",
                    Email = "viharshapramodi@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Viharsha@123"),
                    Role = "Administrator",
                    ContactNumber = "0711234567",
                    AddressLine1 = "108/5 A",
                    Street = "Weragama Road",
                    City = "Wadduwa",
                    Nic = "199914212942",
                    ProfilePicture = "https://i.imgur.com/1qk4XKn.jpg",
                    AccountCreatedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    Token = ""
                },
                // Parking Place Operator
                new Employee
                {
                    EmployeeId = "EMP_0023_4590",
                    FirstName = "Prasad",
                    LastName = "Udara",
                    Email = "parkingopeator@parkease.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Prasad@123"),
                    Role = "ParkingPlaceOperator",
                    ContactNumber = "0711234567",
                    AddressLine1 = "108/5 A",
                    Street = "Weragama Road",
                    City = "Wadduwa",
                    Nic = "199914212942",
                    ProfilePicture = "https://i.imgur.com/1qk4XKn.jpg",
                    AccountCreatedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    Token = ""
                },
                // Parking Place Verifier
                new Employee
                {
                    EmployeeId = "EMP_0025_4591",
                    FirstName = "Dinethi",
                    LastName = "Wickramasinghe",
                    Email = "dinethi@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Dinethi@123"),
                    Role = "ParkingPlaceVerifier",
                    ContactNumber = "0711234567",
                    AddressLine1 = "108/5 A",
                    Street = "Weragama Road",
                    City = "Wadduwa",
                    Nic = "199914212953",
                    ProfilePicture = "https://i.imgur.com/1qk4XKn.jpg",
                    AccountCreatedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    Token = ""
                }
            );


        modelBuilder.Entity<Employee>()
            .HasData(
                new Employee
                {
                    EmployeeId = "EMP_0022_4588",
                    FirstName = "Akila",
                    LastName = "Santhush",
                    Email = "akilasanthush@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Akila@123"),
                    Role = "Verifier",
                    ContactNumber = "0766123645",
                    AddressLine1 = "108/5 A",
                    Street = "Weragama Road",
                    City = "Wadduwa",
                    Nic = "199914212942",
                    ProfilePicture = "https://i.imgur.com/1qk4XKn.jpg",
                    AccountCreatedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    Token = ""
                }
            );

        modelBuilder.Entity<ParkingPlaceOwner>()
            .HasData(
                new ParkingPlaceOwner
                {
                    OwnerId = "OWNER_0001_0001",
                    FirstName = "Isurika ",
                    LastName = " Arunodi",
                    Email = "parkingowner@test.com",
                    Password = "12345678",
                    AddressLine1 = "No 12",
                    City = "Colombo",
                    ContactNumber = "0711234567",
                    NIC = "199914212942",
                    NICFront = "f",
                    NICBack = "b",
                    Token = "token"
                }
            );

        modelBuilder.Entity<ParkingPlaceOwner>()
            .HasData(
                new ParkingPlaceOwner
                {
                    OwnerId = "OWNER_0001_0002",
                    FirstName = "Isurika ",
                    LastName = " Arunodi",
                    Email = "isu@test.com",
                    Password = "12345678",
                    AddressLine1 = "No 12",
                    City = "Colombo",
                    ContactNumber = "0711234567",
                    NIC = "199914212942",
                    NICFront = "a",
                    NICBack = "b",
                    Token = "w"
                },
                new ParkingPlaceOwner
                {
                    OwnerId = "OWN_0024_4591",
                    FirstName = "Isurika ",
                    LastName = " Kandasamy",
                    Email = "isu2@test.com",
                    Password = "12345678",
                    AddressLine1 = "No 12",
                    City = "Colombo",
                    ContactNumber = "0711234567",
                    NIC = "199914212944",
                    NICFront = "a",
                    Token = "b",
                    NICBack = "w"
                },
                new ParkingPlaceOwner
                {
                    OwnerId = "OWN_0025_45819",
                    FirstName = "Isurika ",
                    LastName = " Pramodi",
                    Email = "isu3@test.com",
                    Password = "12345678",
                    AddressLine1 = "No 12",
                    City = "Colombo",
                    ContactNumber = "0711234567",
                    NIC = "199914212942",
                    NICFront = "a",
                    NICBack = "b",
                    Token = "w"
                },
                new ParkingPlaceOwner
                {
                    OwnerId = "OWN_0023_4589",
                    FirstName = "Isurika ",
                    LastName = " Arunodi",
                    Email = "isu4@test.com",
                    Password = "12345678",
                    AddressLine1 = "No 12",
                    City = "Colombo",
                    ContactNumber = "0711234567",
                    NIC = "199914212943",
                    NICFront = "a",
                    NICBack = "b",
                    Token = "w"
                }
            );

        // Parking Places
        modelBuilder.Entity<ParkingPlace>().HasData
        (
            new ParkingPlace
            {
                ParkingPlaceId = "Park_001",
                Description = "This is a parking place for UCSC.",
                Name = "UCSC Parking Place",
                Latitude = 6.9037046,
                Longitude = 79.8597409,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_002",
                Name = "Thirsten College Car Park",
                Description = "This is a parking place for Thirsten College.",
                Latitude = 6.903758,
                Longitude = 79.858857,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_003",
                Description = "This is a parking place for Alfred's car.",
                Name = "Alfred Parking Place",
                Latitude = 6.9028807,
                Longitude = 79.8574223,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_004",
                Description = "This is a parking place for Elite's car.",
                Name = "Elite Parking Place",
                Latitude =6.896073,
                Longitude = 79.858103,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_005",
                Description = "This is a parking place for Colombo's car.",
                Name = "Colombo Parking Spot 1",
                Latitude = 6.927062,
                Longitude = 9.861774,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_006",
                Description = "This is a parking place for City Center.",
                Name = "City Center Parking Lot",
                Latitude = 6.934814,
                Longitude = 79.845247,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_007",
                Description = "This is a parking place for Colombo's car.",
                Name = "Colombo Parking Spot 2",
                Latitude = 6.938403,
                Longitude = 79.863279,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_008",
                Description = "This is a parking place for Downtown.",
                Name = "Downtown Parking Zone",
                Latitude = 6.936579,
                Longitude = 79.853734,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_009",
                Description = "This is a parking place for Central Colombo.",
                Name = "Central Colombo Car Park",
                Latitude = 6.942156,
                Longitude = 79.853205,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_010",
                Name = "Colombo Parking Spot 29",
                Description = "This is a parking place for Colombo's car.",
                Latitude = 6.926507,
                Longitude = 79.859743,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            },
            new ParkingPlace
            {
                ParkingPlaceId = "Park_011",
                Description = "This is a parking place for Colombo's car.",
                Name = "Colombo Parking Spot 30",
                Latitude = 6.920345,
                Longitude = 79.962573,
                ParkingPlaceOwnerId = "OWN_0023_4589",
                ParkingPlaceOperatorId = "EMP_0022_4588",
                ParkingPlaceVerifierId = "EMP_0022_4588",
            }
        );


        modelBuilder.Entity<ParkingPlace>()
            .HasData(
                new ParkingPlace
                {
                    ParkingPlaceId = "PARK_0001_0001",
                    ParkingPlaceOwnerId = "OWNER_0001_0001",
                    ParkingPlaceOperatorId = "EMP_0022_4588",
                    Name = "UCSC Parking Area",
                    ParkingPlaceVerifierId = "EMP_0022_4588",
                    Latitude = 6.920345,
                    Description = "This is a parking place in Wadduwa",
                    Longitude = 79.962573,
                });

        modelBuilder.Entity<SlotCategories>()
            .HasData(
                new SlotCategories
                {
                    SlotCategoryId = "CATEG_0001_0001",
                    SlotCategoryName = "Car",
                    SlotCategoryDescription = "Car parkings only"
                });


        modelBuilder.Entity<ParkingPlaceSlotCapacities>()
            .HasData(
                new ParkingPlaceSlotCapacities
                {
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotCategoryId = "CATEG_0001_0001",
                    SlotCapacity = 20,
                });

        modelBuilder.Entity<Zones>()
            .HasData(
                new Zones
                {
                    ZoneId = "ZONE_0001_0001",
                    ZoneName = "Zone A",
                    ZoneDescription = "This Zone is near to the exit of the parking place",
                    ZoneCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ZonePrice = 150,
                    ParkingPlaceId = "PARK_0001_0001"
                },
                new Zones
                {
                    ZoneId = "ZONE_0001_0002",
                    ZoneName = "Zone B",
                    ZoneDescription = "This Zone is far from the exit of the parking place",
                    ZoneCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ZonePrice = 100,
                    ParkingPlaceId = "PARK_0001_0001"
                });

        modelBuilder.Entity<Slot>()
            .HasData(
                new Slot
                {
                    SlotId = "SLOT_0001_0001",
                    SlotNumber = 1,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0002",
                    SlotNumber = 2,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0003",
                    SlotNumber = 3,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0004",
                    SlotNumber = 4,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0005",
                    SlotNumber = 5,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0006",
                    SlotNumber = 6,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0007",
                    SlotNumber = 7,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0008",
                    SlotNumber = 8,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0009",
                    SlotNumber = 9,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0001",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0010",
                    SlotNumber = 10,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0011",
                    SlotNumber = 11,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Parked",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0012",
                    SlotNumber = 12,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0013",
                    SlotNumber = 13,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Reserved",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0014",
                    SlotNumber = 14,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0015",
                    SlotNumber = 15,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Emergency",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0016",
                    SlotNumber = 16,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0017",
                    SlotNumber = 17,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0018",
                    SlotNumber = 18,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0019",
                    SlotNumber = 19,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                },
                new Slot
                {
                    SlotId = "SLOT_0001_0020",
                    SlotNumber = 20,
                    SlotCategoryId = "CATEG_0001_0001",
                    ZoneId = "ZONE_0001_0002",
                    ParkingPlaceId = "PARK_0001_0001",
                    SlotStatus = "Available",
                    Description = "Slot",
                    SlotCreatedDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ReservedUntil = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                }
            );
    }
}