﻿// <auto-generated />
using System;
using CurveFitter.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CurveFitter.Server.Migrations
{
    [DbContext(typeof(ArchiveContext))]
    [Migration("20240124160343_testMigration")]
    partial class testMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32");

            modelBuilder.Entity("CurveFitter.Server.Archive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Equation")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FitType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Archives");
                });

            modelBuilder.Entity("CurveFitter.Server.DataPoint", b =>
                {
                    b.Property<double>("X")
                        .HasColumnType("REAL");

                    b.Property<int?>("ArchiveId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ArchiveId1")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Y")
                        .HasColumnType("REAL");

                    b.HasKey("X");

                    b.HasIndex("ArchiveId");

                    b.HasIndex("ArchiveId1");

                    b.ToTable("DataPoint");
                });

            modelBuilder.Entity("CurveFitter.Server.DataPoint", b =>
                {
                    b.HasOne("CurveFitter.Server.Archive", null)
                        .WithMany("FitDataPoints")
                        .HasForeignKey("ArchiveId");

                    b.HasOne("CurveFitter.Server.Archive", null)
                        .WithMany("UserDataPoints")
                        .HasForeignKey("ArchiveId1");
                });
#pragma warning restore 612, 618
        }
    }
}
