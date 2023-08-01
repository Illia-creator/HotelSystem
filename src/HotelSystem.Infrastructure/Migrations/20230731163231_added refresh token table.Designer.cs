﻿// <auto-generated />
using System;
using HotelSystem.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelSystem.Infrastructure.Migrations
{
    [DbContext(typeof(HotelSystemDbContext))]
    [Migration("20230731163231_added refresh token table")]
    partial class addedrefreshtokentable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Hotel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Site")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Payment", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("boolean");

                    b.Property<double>("PaymentAmount")
                        .HasColumnType("double precision");

                    b.Property<DateOnly>("PaymentDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ReservationId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("RentEnd")
                        .HasColumnType("date");

                    b.Property<DateOnly>("RentStart")
                        .HasColumnType("date");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uuid");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<Guid>("RoomTypeId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoomsNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Shower")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Toilet")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.RoomType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<double>("MoneyBonuses")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Payment", b =>
                {
                    b.HasOne("HotelSystem.Domain.Entities.DbEntities.Reservation", "Reservation")
                        .WithOne("Payment")
                        .HasForeignKey("HotelSystem.Domain.Entities.DbEntities.Payment", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelSystem.Domain.Entities.DbEntities.User", null)
                        .WithMany("Payments")
                        .HasForeignKey("UserId");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.RefreshToken", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Reservation", b =>
                {
                    b.HasOne("HotelSystem.Domain.Entities.DbEntities.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelSystem.Domain.Entities.DbEntities.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Room", b =>
                {
                    b.HasOne("HotelSystem.Domain.Entities.DbEntities.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelSystem.Domain.Entities.DbEntities.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Reservation", b =>
                {
                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.Room", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.RoomType", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelSystem.Domain.Entities.DbEntities.User", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
