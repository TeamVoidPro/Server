﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.DbContext;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230703043918_BookingReservationTables")]
    partial class BookingReservationTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Server.Models.BookingPlan", b =>
                {
                    b.Property<string>("BookingPlanId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParkingPlaceId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PlanDuration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BookingPlanId");

                    b.HasIndex("ParkingPlaceId");

                    b.ToTable("BookingPlans");
                });

            modelBuilder.Entity("Server.Models.BookingReservation", b =>
                {
                    b.Property<string>("BookingReservationId")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservationEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservationStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("TotalPayment")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<string>("ZonePlanId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("BookingReservationId");

                    b.HasIndex("VehicleNumber")
                        .IsUnique();

                    b.ToTable("BookingReservations");
                });

            modelBuilder.Entity("Server.Models.Driver", b =>
                {
                    b.Property<string>("DriverId")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("AccountCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DriverId");

                    b.HasIndex("DriverId", "Email");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Server.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("AccountCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Nic")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Server.Models.Parking", b =>
                {
                    b.Property<string>("BookingReservationId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("SlotId")
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("IsParkOnNextDay")
                        .HasColumnType("boolean");

                    b.Property<string>("ParkedDuration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ParkingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ParkingEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ParkingId")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("ParkingStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("BookingReservationId", "SlotId");

                    b.HasIndex("SlotId");

                    b.ToTable("BookingParkings");
                });

            modelBuilder.Entity("Server.Models.ParkingPlace", b =>
                {
                    b.Property<string>("ParkingPlaceId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ParkingPlaceOperatorId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ParkingPlaceOwnerOwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ParkingPlaceVerifierId")
                        .HasColumnType("varchar(20)");

                    b.HasKey("ParkingPlaceId");

                    b.HasIndex("ParkingPlaceOperatorId");

                    b.HasIndex("ParkingPlaceOwnerOwnerId");

                    b.HasIndex("ParkingPlaceVerifierId");

                    b.HasIndex("ParkingPlaceId", "Name", "Location", "ParkingPlaceOperatorId", "ParkingPlaceVerifierId")
                        .IsUnique();

                    b.ToTable("ParkingPlaces");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceImages", b =>
                {
                    b.Property<string>("ParkingPlaceId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.HasKey("ParkingPlaceId", "ImageUrl");

                    b.HasIndex("ParkingPlaceId", "ImageUrl")
                        .IsUnique();

                    b.ToTable("ParkingPlaceImages");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceOwner", b =>
                {
                    b.Property<string>("OwnerId")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("AccountCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("DeedCopy")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Nic")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("OwnerId");

                    b.ToTable("ParkingPlaceOwners");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceRatings", b =>
                {
                    b.Property<string>("RatingId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("DriverId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ParkingPlaceId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<DateTime>("RatingDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RatingId");

                    b.HasIndex("DriverId");

                    b.HasIndex("ParkingPlaceId");

                    b.ToTable("ParkingPlaceRatings");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceServices", b =>
                {
                    b.Property<string>("ParkingPlaceId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ServiceProvide")
                        .HasColumnType("text");

                    b.HasKey("ParkingPlaceId", "ServiceProvide");

                    b.HasIndex("ParkingPlaceId", "ServiceProvide")
                        .IsUnique();

                    b.ToTable("ParkingPlaceServices");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceSlotCapacities", b =>
                {
                    b.Property<string>("ParkingPlaceId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("SlotCategoryId")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("SlotCapacity")
                        .HasColumnType("integer");

                    b.HasKey("ParkingPlaceId", "SlotCategoryId");

                    b.HasIndex("SlotCategoryId");

                    b.ToTable("ParkingPlaceSlotCapacities");
                });

            modelBuilder.Entity("Server.Models.Reservation", b =>
                {
                    b.Property<string>("ReservationId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CancellationReason")
                        .HasColumnType("text");

                    b.Property<DateTime>("ParkingEndedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ParkingPlaceId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ParkingPlaceOperatorId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("ParkingStartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("ReservationCancelledAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservationCreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservationEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservationStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReservationStatus")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ReservationType")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("SlotId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("SpecialNotes")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ZonesZoneId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("ReservationId");

                    b.HasIndex("ParkingPlaceId");

                    b.HasIndex("ParkingPlaceOperatorId");

                    b.HasIndex("SlotId");

                    b.HasIndex("VehicleNumber");

                    b.HasIndex("ZonesZoneId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Server.Models.Slot", b =>
                {
                    b.Property<string>("SlotId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("ParkingPlaceId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("ReservedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservedUntil")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SlotCategoryId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("SlotCreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SlotNumber")
                        .HasColumnType("integer");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ZonesZoneId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("SlotId");

                    b.HasIndex("ParkingPlaceId");

                    b.HasIndex("SlotCategoryId");

                    b.HasIndex("ZonesZoneId");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("Server.Models.SlotCategories", b =>
                {
                    b.Property<string>("SlotCategoryId")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("CategoryCreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SlotCategoryDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SlotCategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("SlotCategoryId");

                    b.ToTable("SlotCategories");
                });

            modelBuilder.Entity("Server.Models.SlotReservationHistory", b =>
                {
                    b.Property<string>("SlotReservationHistoryId")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("ReservationEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReservationId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("ReservationStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SlotId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("SlotReservationHistoryId");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.HasIndex("SlotId");

                    b.ToTable("SlotReservationHistories");
                });

            modelBuilder.Entity("Server.Models.Ticket", b =>
                {
                    b.Property<string>("TicketId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("QrCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TicketCreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TicketExpiredDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Validity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("VerifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VerifiedBy")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("TicketId");

                    b.HasIndex("VerifiedBy");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Server.Models.Vehicle", b =>
                {
                    b.Property<string>("VehicleNumber")
                        .HasColumnType("varchar(8)");

                    b.Property<string>("AdditionalNotes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DriverId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("VehicleAddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("VehicleNumber");

                    b.HasIndex("DriverId");

                    b.HasIndex("VehicleNumber", "VehicleModel", "VehicleType");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Server.Models.ZonePlan", b =>
                {
                    b.Property<string>("ZonePlanId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("BookingPlanId")
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("ZonePlanId", "BookingPlanId");

                    b.HasIndex("BookingPlanId");

                    b.HasIndex("ZoneId");

                    b.ToTable("ZonePlans");
                });

            modelBuilder.Entity("Server.Models.Zones", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ParkingPlaceId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("ZoneCreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ZoneDescription")
                        .HasColumnType("text");

                    b.Property<string>("ZoneName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ZoneId");

                    b.HasIndex("ParkingPlaceId");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("Server.Models.BookingPlan", b =>
                {
                    b.HasOne("Server.Models.ParkingPlace", "ParkingPlace")
                        .WithMany("BookingPlans")
                        .HasForeignKey("ParkingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingPlace");
                });

            modelBuilder.Entity("Server.Models.BookingReservation", b =>
                {
                    b.HasOne("Server.Models.Vehicle", "Vehicle")
                        .WithOne("BookingReservation")
                        .HasForeignKey("Server.Models.BookingReservation", "VehicleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Server.Models.Parking", b =>
                {
                    b.HasOne("Server.Models.BookingReservation", "BookingReservation")
                        .WithMany("Parkings")
                        .HasForeignKey("BookingReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Slot", "Slot")
                        .WithMany("Parkings")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingReservation");

                    b.Navigation("Slot");
                });

            modelBuilder.Entity("Server.Models.ParkingPlace", b =>
                {
                    b.HasOne("Server.Models.Employee", "ParkingPlaceOperator")
                        .WithMany()
                        .HasForeignKey("ParkingPlaceOperatorId");

                    b.HasOne("Server.Models.ParkingPlaceOwner", "ParkingPlaceOwner")
                        .WithMany("ParkingPlaces")
                        .HasForeignKey("ParkingPlaceOwnerOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Employee", "ParkingPlaceVerifier")
                        .WithMany()
                        .HasForeignKey("ParkingPlaceVerifierId");

                    b.Navigation("ParkingPlaceOperator");

                    b.Navigation("ParkingPlaceOwner");

                    b.Navigation("ParkingPlaceVerifier");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceImages", b =>
                {
                    b.HasOne("Server.Models.ParkingPlace", "ParkingPlace")
                        .WithMany("ParkingPlaceImages")
                        .HasForeignKey("ParkingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingPlace");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceRatings", b =>
                {
                    b.HasOne("Server.Models.Driver", "Driver")
                        .WithMany("ParkingPlaceRatings")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.ParkingPlace", "ParkingPlace")
                        .WithMany("ParkingPlaceRatings")
                        .HasForeignKey("ParkingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("ParkingPlace");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceServices", b =>
                {
                    b.HasOne("Server.Models.ParkingPlace", "ParkingPlace")
                        .WithMany("ParkingPlaceServices")
                        .HasForeignKey("ParkingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingPlace");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceSlotCapacities", b =>
                {
                    b.HasOne("Server.Models.ParkingPlace", "ParkingPlace")
                        .WithMany("ParkingPlaceSlotCapacities")
                        .HasForeignKey("ParkingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.SlotCategories", "SlotCategories")
                        .WithMany("ParkingPlaceSlotCapacities")
                        .HasForeignKey("SlotCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingPlace");

                    b.Navigation("SlotCategories");
                });

            modelBuilder.Entity("Server.Models.Reservation", b =>
                {
                    b.HasOne("Server.Models.ParkingPlace", "ParkingPlace")
                        .WithMany("Reservations")
                        .HasForeignKey("ParkingPlaceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Models.Employee", "ParkingPlaceOperator")
                        .WithMany("Reservation")
                        .HasForeignKey("ParkingPlaceOperatorId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Server.Models.Slot", "Slot")
                        .WithMany("Reservations")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Server.Models.Vehicle", "Vehicle")
                        .WithMany("Reservations")
                        .HasForeignKey("VehicleNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Models.Zones", "Zones")
                        .WithMany()
                        .HasForeignKey("ZonesZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingPlace");

                    b.Navigation("ParkingPlaceOperator");

                    b.Navigation("Slot");

                    b.Navigation("Vehicle");

                    b.Navigation("Zones");
                });

            modelBuilder.Entity("Server.Models.Slot", b =>
                {
                    b.HasOne("Server.Models.ParkingPlace", "ParkingPlace")
                        .WithMany("Slots")
                        .HasForeignKey("ParkingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.SlotCategories", "SlotCategories")
                        .WithMany("Slots")
                        .HasForeignKey("SlotCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Zones", "Zones")
                        .WithMany()
                        .HasForeignKey("ZonesZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingPlace");

                    b.Navigation("SlotCategories");

                    b.Navigation("Zones");
                });

            modelBuilder.Entity("Server.Models.SlotReservationHistory", b =>
                {
                    b.HasOne("Server.Models.Reservation", "Reservation")
                        .WithOne("SlotReservationHistory")
                        .HasForeignKey("Server.Models.SlotReservationHistory", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Slot", "Slot")
                        .WithMany("SlotReservationHistories")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("Slot");
                });

            modelBuilder.Entity("Server.Models.Ticket", b =>
                {
                    b.HasOne("Server.Models.Employee", "ParkingPlaceOperator")
                        .WithMany("Ticket")
                        .HasForeignKey("VerifiedBy")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParkingPlaceOperator");
                });

            modelBuilder.Entity("Server.Models.Vehicle", b =>
                {
                    b.HasOne("Server.Models.Driver", "Driver")
                        .WithMany("Vehicles")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("Server.Models.ZonePlan", b =>
                {
                    b.HasOne("Server.Models.BookingPlan", "BookingPlan")
                        .WithMany("ZonePlans")
                        .HasForeignKey("BookingPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Zones", "Zone")
                        .WithMany("ZonePlans")
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BookingPlan");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("Server.Models.Zones", b =>
                {
                    b.HasOne("Server.Models.ParkingPlace", "ParkingPlace")
                        .WithMany("Zones")
                        .HasForeignKey("ParkingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingPlace");
                });

            modelBuilder.Entity("Server.Models.BookingPlan", b =>
                {
                    b.Navigation("ZonePlans");
                });

            modelBuilder.Entity("Server.Models.BookingReservation", b =>
                {
                    b.Navigation("Parkings");
                });

            modelBuilder.Entity("Server.Models.Driver", b =>
                {
                    b.Navigation("ParkingPlaceRatings");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Server.Models.Employee", b =>
                {
                    b.Navigation("Reservation");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Server.Models.ParkingPlace", b =>
                {
                    b.Navigation("BookingPlans");

                    b.Navigation("ParkingPlaceImages");

                    b.Navigation("ParkingPlaceRatings");

                    b.Navigation("ParkingPlaceServices");

                    b.Navigation("ParkingPlaceSlotCapacities");

                    b.Navigation("Reservations");

                    b.Navigation("Slots");

                    b.Navigation("Zones");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceOwner", b =>
                {
                    b.Navigation("ParkingPlaces");
                });

            modelBuilder.Entity("Server.Models.Reservation", b =>
                {
                    b.Navigation("SlotReservationHistory")
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.Slot", b =>
                {
                    b.Navigation("Parkings");

                    b.Navigation("Reservations");

                    b.Navigation("SlotReservationHistories");
                });

            modelBuilder.Entity("Server.Models.SlotCategories", b =>
                {
                    b.Navigation("ParkingPlaceSlotCapacities");

                    b.Navigation("Slots");
                });

            modelBuilder.Entity("Server.Models.Vehicle", b =>
                {
                    b.Navigation("BookingReservation")
                        .IsRequired();

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Server.Models.Zones", b =>
                {
                    b.Navigation("ZonePlans");
                });
#pragma warning restore 612, 618
        }
    }
}
