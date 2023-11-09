﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarWatch.Database;

#nullable disable

namespace SolarWatch.Migrations
{
    [DbContext(typeof(SolarApiContext))]
    partial class SolarApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SolarWatch.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SunriseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SunriseId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "England",
                            Latitude = 51.509864999999998,
                            Longitude = -0.118092,
                            Name = "London"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Hungary",
                            Latitude = 47.497912999999997,
                            Longitude = 19.040236,
                            Name = "Budapest"
                        },
                        new
                        {
                            Id = 3,
                            Country = "France",
                            Latitude = 48.864716000000001,
                            Longitude = 2.3490139999999999,
                            Name = "Paris"
                        });
                });

            modelBuilder.Entity("SolarWatch.Model.Sunrises", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfSunrise")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("SunriseTimeDb")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Sunrises");
                });

            modelBuilder.Entity("SolarWatch.Model.City", b =>
                {
                    b.HasOne("SolarWatch.Model.Sunrises", "Sunrise")
                        .WithMany()
                        .HasForeignKey("SunriseId");

                    b.Navigation("Sunrise");
                });
#pragma warning restore 612, 618
        }
    }
}
