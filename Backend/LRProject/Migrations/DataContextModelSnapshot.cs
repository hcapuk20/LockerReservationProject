﻿// <auto-generated />
using LRProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LRProject.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.6.22329.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LRProject.Models.Source", b =>
                {
                    b.Property<int>("Source_Id")
                        .HasColumnType("int");

                    b.Property<int>("SourceGroupId")
                        .HasColumnType("int");

                    b.HasKey("Source_Id");

                    b.HasIndex("SourceGroupId");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("LRProject.Models.SourceGroup", b =>
                {
                    b.Property<int>("Source_Group_Id")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Source_Group_Id");

                    b.ToTable("SourceGroups");
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
                    b.Navigation("Sources");
                });
#pragma warning restore 612, 618
        }
    }
}
