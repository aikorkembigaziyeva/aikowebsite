﻿// <auto-generated />
using Aiko_Hastanesi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aiko_Hastanesi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241207101312_AsistanTabloSeed")]
    partial class AsistanTabloSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aiko_Hastanesi.Models.Asistan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Asistans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdSoyad = "Ali Yılmaz",
                            Adres = "İstanbul, Türkiye",
                            Email = "ali.yilmaz@example.com",
                            Telefon = "1234567890"
                        },
                        new
                        {
                            Id = 2,
                            AdSoyad = "Ayşe Demir",
                            Adres = "Ankara, Türkiye",
                            Email = "ayse.demir@example.com",
                            Telefon = "0987654321"
                        },
                        new
                        {
                            Id = 3,
                            AdSoyad = "Mehmet Can",
                            Adres = "İzmir, Türkiye",
                            Email = "mehmet.can@example.com",
                            Telefon = "5551234567"
                        },
                        new
                        {
                            Id = 4,
                            AdSoyad = "Fatma Korkmaz",
                            Adres = "Bursa, Türkiye",
                            Email = "fatma.korkmaz@example.com",
                            Telefon = "4445678901"
                        });
                });

            modelBuilder.Entity("Aiko_Hastanesi.Models.Hoca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Hocas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdSoyad = "Ali Yılmaz",
                            Adres = "İstanbul, Türkiye",
                            Email = "ali.yilmaz@example.com",
                            Telefon = "1234567890"
                        },
                        new
                        {
                            Id = 2,
                            AdSoyad = "Ayşe Demir",
                            Adres = "Ankara, Türkiye",
                            Email = "ayse.demir@example.com",
                            Telefon = "0987654321"
                        },
                        new
                        {
                            Id = 3,
                            AdSoyad = "Mehmet Can",
                            Adres = "İzmir, Türkiye",
                            Email = "mehmet.can@example.com",
                            Telefon = "5551234567"
                        },
                        new
                        {
                            Id = 4,
                            AdSoyad = "Fatma Korkmaz",
                            Adres = "Bursa, Türkiye",
                            Email = "fatma.korkmaz@example.com",
                            Telefon = "4445678901"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
