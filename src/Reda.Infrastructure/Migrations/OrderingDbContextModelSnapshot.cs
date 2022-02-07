﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reda.Infrastructure.Repositories;

#nullable disable

namespace Reda.Infrastructure.Migrations
{
    [DbContext(typeof(OrderingDbContext))]
    partial class OrderingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Reda.Infrastructure.Repositories.Models.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Reda.Infrastructure.Repositories.Models.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .IsFixedLength();

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Reda.Infrastructure.Repositories.Models.ProductTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StackLimit")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ProductTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("cbffff55-25c1-4e39-abac-20dfda442c27"),
                            Name = "photoBook",
                            StackLimit = 1,
                            Width = 19.0
                        },
                        new
                        {
                            Id = new Guid("a2b6fb96-7e3c-44e6-905c-72ce8bebc71c"),
                            Name = "calendar",
                            StackLimit = 1,
                            Width = 10.0
                        },
                        new
                        {
                            Id = new Guid("a9bf52be-3bf1-46f2-b0bb-a8f8ff02090e"),
                            Name = "canvas",
                            StackLimit = 1,
                            Width = 16.0
                        },
                        new
                        {
                            Id = new Guid("557ade17-e9a9-48e8-b5f9-7010c7db6514"),
                            Name = "cards",
                            StackLimit = 1,
                            Width = 4.7000000000000002
                        },
                        new
                        {
                            Id = new Guid("4d6f3b85-cf39-4aae-8a3d-3b0337e37fa9"),
                            Name = "mug",
                            StackLimit = 4,
                            Width = 94.0
                        });
                });

            modelBuilder.Entity("Reda.Infrastructure.Repositories.Models.ProductEntity", b =>
                {
                    b.HasOne("Reda.Infrastructure.Repositories.Models.OrderEntity", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Reda.Infrastructure.Repositories.Models.ProductTypeEntity", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("Reda.Infrastructure.Repositories.Models.OrderEntity", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
