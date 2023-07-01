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

                    b.Property<int>("ContactNumber")
                        .HasMaxLength(10)
                        .HasColumnType("integer");

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

                    b.Property<int>("ContactNumber")
                        .HasMaxLength(10)
                        .HasColumnType("integer");

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

                    b.Property<int>("ContactNumber")
                        .HasMaxLength(10)
                        .HasColumnType("integer");

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
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
