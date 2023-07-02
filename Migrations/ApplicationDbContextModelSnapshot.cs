﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.DbContext;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

            modelBuilder.Entity("Server.Models.Vehicle", b =>
                {
                    b.HasOne("Server.Models.Driver", "Driver")
                        .WithMany("Vehicles")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("Server.Models.Driver", b =>
                {
                    b.Navigation("ParkingPlaceRatings");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Server.Models.ParkingPlace", b =>
                {
                    b.Navigation("ParkingPlaceImages");

                    b.Navigation("ParkingPlaceRatings");

                    b.Navigation("ParkingPlaceServices");

                    b.Navigation("ParkingPlaceSlotCapacities");
                });

            modelBuilder.Entity("Server.Models.ParkingPlaceOwner", b =>
                {
                    b.Navigation("ParkingPlaces");
                });

            modelBuilder.Entity("Server.Models.SlotCategories", b =>
                {
                    b.Navigation("ParkingPlaceSlotCapacities");
                });
#pragma warning restore 612, 618
        }
    }
}
