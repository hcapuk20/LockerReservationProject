﻿// <auto-generated />
using System;
using LRProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LRProject.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220805063924_AdminRelation")]
    partial class AdminRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.6.22329.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EmployeeSource", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.Property<int>("SourcesId")
                        .HasColumnType("int");

                    b.HasKey("EmployeesId", "SourcesId");

                    b.HasIndex("SourcesId");

                    b.ToTable("EmployeeSource");
                });

            modelBuilder.Entity("LRProject.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("LRProject.Models.Source", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("SourceGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SourceGroupId");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("LRProject.Models.SourceGroup", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("SourceGroups");
                });

            modelBuilder.Entity("EmployeeSource", b =>
                {
                    b.HasOne("LRProject.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LRProject.Models.Source", null)
                        .WithMany()
                        .HasForeignKey("SourcesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LRProject.Models.Source", b =>
                {
                    b.HasOne("LRProject.Models.SourceGroup", "SourceGroup")
                        .WithMany("Sources")
                        .HasForeignKey("SourceGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceGroup");
                });

            modelBuilder.Entity("LRProject.Models.SourceGroup", b =>
                {
                    b.HasOne("LRProject.Models.Employee", null)
                        .WithMany("SourceGroups")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("LRProject.Models.Employee", b =>
                {
                    b.Navigation("SourceGroups");
                });

            modelBuilder.Entity("LRProject.Models.SourceGroup", b =>
                {
                    b.Navigation("Sources");
                });
#pragma warning restore 612, 618
        }
    }
}
