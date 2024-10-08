﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReversiRestApi.DAL;

#nullable disable

namespace ReversiRestApi.Migrations
{
    [DbContext(typeof(SpelContext))]
    [Migration("20240902090732_AddIsPointsUpdatedField")]
    partial class AddIsPointsUpdatedField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReversiRestApi.Model.Spel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AandeBeurt")
                        .HasColumnType("int");

                    b.Property<string>("BoardString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Bord");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PuntenGegeven")
                        .HasColumnType("bit");

                    b.Property<string>("Speler1Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speler2Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Winnaar")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Spellen");
                });
#pragma warning restore 612, 618
        }
    }
}
