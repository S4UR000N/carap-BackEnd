﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_storm.Data;

namespace api_storm.Migrations
{
    [DbContext(typeof(api_stormContext))]
    partial class api_stormContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api_storm.Models.BrandModel", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("BrandModel");
                });

            modelBuilder.Entity("api_storm.Models.VehicleModel", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandNameId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.ToTable("VehicleModel");
                });

            modelBuilder.Entity("api_storm.Models.VehicleTypeModel", b =>
                {
                    b.Property<int>("VehicleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("VehicleTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleTypeId");

                    b.ToTable("VehicleTypeModel");
                });
#pragma warning restore 612, 618
        }
    }
}
