﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using deal.data.Models;

namespace deal.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220519215155_Changes")]
    partial class Changes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("deal.data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("category")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("deal.data.Models.DeliveryRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("text");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("deliveryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("deliverymanId")
                        .HasColumnType("integer");

                    b.Property<string>("status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("deliverymanId");

                    b.ToTable("DeliveryRecords");
                });

            modelBuilder.Entity("deal.data.Models.Deliveryman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<int>("phone")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Deliverymens");
                });

            modelBuilder.Entity("deal.data.Models.Good", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Img")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("available")
                        .HasColumnType("boolean");

                    b.Property<int?>("categoryId")
                        .HasColumnType("integer");

                    b.Property<int>("cost")
                        .HasColumnType("integer");

                    b.Property<int>("count")
                        .HasColumnType("integer");

                    b.Property<string>("maxdesc")
                        .HasColumnType("text");

                    b.Property<string>("mindesc")
                        .HasColumnType("text");

                    b.Property<int?>("sellerNameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("categoryId");

                    b.HasIndex("sellerNameId");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("deal.data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("GoodId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("GoodId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("deal.data.Models.ShopCart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("GoodId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("GoodId");

                    b.HasIndex("UserId");

                    b.ToTable("ShopCarts");
                });

            modelBuilder.Entity("deal.data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsOrganization")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("balance")
                        .HasColumnType("integer");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("deal.data.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<int>("phone")
                        .HasColumnType("integer");

                    b.Property<string>("role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("deal.data.Models.DeliveryRecord", b =>
                {
                    b.HasOne("deal.data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("deal.data.Models.Deliveryman", "deliveryman")
                        .WithMany()
                        .HasForeignKey("deliverymanId");
                });

            modelBuilder.Entity("deal.data.Models.Good", b =>
                {
                    b.HasOne("deal.data.Models.Category", "category")
                        .WithMany()
                        .HasForeignKey("categoryId");

                    b.HasOne("deal.data.Models.User", "sellerName")
                        .WithMany()
                        .HasForeignKey("sellerNameId");
                });

            modelBuilder.Entity("deal.data.Models.Order", b =>
                {
                    b.HasOne("deal.data.Models.Good", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId");

                    b.HasOne("deal.data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("deal.data.Models.ShopCart", b =>
                {
                    b.HasOne("deal.data.Models.Good", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId");

                    b.HasOne("deal.data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
