﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sneakers.Models;

namespace Sneakers.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220611040035_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Sneakers.Models.EMPLOYEE", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SURNAME");

                    b.Property<DateTime>("WorkEnter")
                        .HasColumnType("datetime2")
                        .HasColumnName("WORK_ENTER");

                    b.HasKey("Id");

                    b.ToTable("EMPLOYEE");
                });

            modelBuilder.Entity("Sneakers.Models.SIZE", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SIZE");

                    b.HasKey("Id");

                    b.ToTable("SIZE");
                });

            modelBuilder.Entity("Sneakers.Models.SIZE_SNEAKERS_CONNECTION", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("QUANTITY");

                    b.Property<int>("SizeId")
                        .HasColumnType("int")
                        .HasColumnName("SIZE_ID");

                    b.Property<int>("SneakersId")
                        .HasColumnType("int")
                        .HasColumnName("SNEAKERS_ID");

                    b.HasKey("Id");

                    b.HasIndex("SizeId");

                    b.HasIndex("SneakersId");

                    b.ToTable("SIZE_SNEAKERS_CONNECTION");
                });

            modelBuilder.Entity("Sneakers.Models.SNEAKERS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int>("BrandId")
                        .HasColumnType("int")
                        .HasColumnName("BRAND_ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("CREATED_BY");

                    b.Property<int>("ModelId")
                        .HasColumnType("int")
                        .HasColumnName("MODEL_ID");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("PRICE");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("TYPE_ID");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("UPDATED_BY");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int")
                        .HasColumnName("WAREHOUSE_ID");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TypeId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("SNEAKERS");
                });

            modelBuilder.Entity("Sneakers.Models.SNEAKERS_BRAND", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BRAND");

                    b.HasKey("Id");

                    b.ToTable("SNEAKERS_BRAND");
                });

            modelBuilder.Entity("Sneakers.Models.SNEAKERS_MODEL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODEL");

                    b.HasKey("Id");

                    b.ToTable("SNEAKERS_MODEL");
                });

            modelBuilder.Entity("Sneakers.Models.SNEAKERS_TYPE", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TYPE");

                    b.HasKey("Id");

                    b.ToTable("SNEAKERS_TYPE");
                });

            modelBuilder.Entity("Sneakers.Models.WAREHOUSE", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Capacity")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CAPACITY");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LOCATION");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ZIP");

                    b.HasKey("Id");

                    b.ToTable("WAREHOUSE");
                });

            modelBuilder.Entity("Sneakers.Models.SIZE_SNEAKERS_CONNECTION", b =>
                {
                    b.HasOne("Sneakers.Models.SIZE", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sneakers.Models.SNEAKERS", "Sneakers")
                        .WithMany()
                        .HasForeignKey("SneakersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Size");

                    b.Navigation("Sneakers");
                });

            modelBuilder.Entity("Sneakers.Models.SNEAKERS", b =>
                {
                    b.HasOne("Sneakers.Models.SNEAKERS_BRAND", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sneakers.Models.SNEAKERS_MODEL", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sneakers.Models.SNEAKERS_TYPE", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sneakers.Models.WAREHOUSE", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Model");

                    b.Navigation("Type");

                    b.Navigation("Warehouse");
                });
#pragma warning restore 612, 618
        }
    }
}
