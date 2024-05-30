﻿// <auto-generated />
using System;
using BackEndStructuer.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GazBackEnd.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231114105810_AddCartEntity")]
    partial class AddCartEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackEndStructuer.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int?>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BackEndStructuer.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("BackEndStructuer.Entities.Notifications", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("NotifyFor")
                        .HasColumnType("text");

                    b.Property<Guid>("NotifyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("DistrictId")
                        .HasColumnType("uuid");

                    b.Property<string>("FullAddress")
                        .HasColumnType("text");

                    b.Property<Guid?>("GovernorateId")
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<double?>("Latidute")
                        .HasColumnType("double precision");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("GovernorateId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.CartProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("DistrictId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.District", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("GovernorateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GovernorateId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("FileID")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProviderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FileID");

                    b.HasIndex("ProviderId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Governorate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Governorates");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("OrderStatus")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.OrderProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Otp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<int?>("OtpCode")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Otps");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("Price")
                        .HasColumnType("integer");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Weight")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LicenseNumber")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int?>("Role")
                        .HasColumnType("integer");

                    b.Property<Guid?>("stationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("stationId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Station", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("DistrictId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GovernorateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("ProductionRate")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("GovernorateId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Address", b =>
                {
                    b.HasOne("BackEndStructuer.Entities.AppUser", "AppUser")
                        .WithMany("Addresses")
                        .HasForeignKey("AppUserId");

                    b.HasOne("Gaz_BackEnd.Entities.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId");

                    b.HasOne("Gaz_BackEnd.Entities.District", "District")
                        .WithMany("Addresses")
                        .HasForeignKey("DistrictId");

                    b.HasOne("Gaz_BackEnd.Entities.Governorate", "Governorate")
                        .WithMany("Addresses")
                        .HasForeignKey("GovernorateId");

                    b.Navigation("AppUser");

                    b.Navigation("City");

                    b.Navigation("District");

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Cart", b =>
                {
                    b.HasOne("BackEndStructuer.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.CartProduct", b =>
                {
                    b.HasOne("Gaz_BackEnd.Entities.Cart", "Cart")
                        .WithMany("CartProducts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gaz_BackEnd.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.City", b =>
                {
                    b.HasOne("Gaz_BackEnd.Entities.District", "District")
                        .WithMany("Cities")
                        .HasForeignKey("DistrictId");

                    b.Navigation("District");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.District", b =>
                {
                    b.HasOne("Gaz_BackEnd.Entities.Governorate", "Governorate")
                        .WithMany("Districts")
                        .HasForeignKey("GovernorateId");

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Document", b =>
                {
                    b.HasOne("Gaz_BackEnd.Entities.File", "File")
                        .WithMany("Documents")
                        .HasForeignKey("FileID");

                    b.HasOne("Gaz_BackEnd.Entities.Provider", "Provider")
                        .WithMany("Documents")
                        .HasForeignKey("ProviderId");

                    b.Navigation("File");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Order", b =>
                {
                    b.HasOne("Gaz_BackEnd.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Gaz_BackEnd.Entities.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");

                    b.HasOne("BackEndStructuer.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Address");

                    b.Navigation("Provider");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.OrderProduct", b =>
                {
                    b.HasOne("Gaz_BackEnd.Entities.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gaz_BackEnd.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Product", b =>
                {
                    b.HasOne("Gaz_BackEnd.Entities.File", "File")
                        .WithOne("Product")
                        .HasForeignKey("Gaz_BackEnd.Entities.Product", "FileId");

                    b.Navigation("File");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Provider", b =>
                {
                    b.HasOne("Gaz_BackEnd.Entities.Station", "Station")
                        .WithMany("Providers")
                        .HasForeignKey("stationId");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Report", b =>
                {
                    b.HasOne("BackEndStructuer.Entities.AppUser", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Station", b =>
                {
                    b.HasOne("BackEndStructuer.Entities.AppUser", "AppUser")
                        .WithMany("Stations")
                        .HasForeignKey("AppUserId");

                    b.HasOne("Gaz_BackEnd.Entities.City", "City")
                        .WithMany("Stations")
                        .HasForeignKey("CityId");

                    b.HasOne("Gaz_BackEnd.Entities.District", "District")
                        .WithMany("Stations")
                        .HasForeignKey("DistrictId");

                    b.HasOne("Gaz_BackEnd.Entities.Governorate", "Governorate")
                        .WithMany("Stations")
                        .HasForeignKey("GovernorateId");

                    b.Navigation("AppUser");

                    b.Navigation("City");

                    b.Navigation("District");

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("BackEndStructuer.Entities.AppUser", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Reports");

                    b.Navigation("Stations");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Cart", b =>
                {
                    b.Navigation("CartProducts");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.City", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Stations");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.District", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Cities");

                    b.Navigation("Stations");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.File", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Governorate", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Districts");

                    b.Navigation("Stations");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Provider", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("Gaz_BackEnd.Entities.Station", b =>
                {
                    b.Navigation("Providers");
                });
#pragma warning restore 612, 618
        }
    }
}
