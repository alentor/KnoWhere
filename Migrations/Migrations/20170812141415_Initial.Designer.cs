using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Database;

namespace Migrations.Migrations
{
    [DbContext(typeof(KnowWhereContext))]
    [Migration("20170812141415_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Database.Entities.Range", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.HasKey("Id");

                    b.ToTable("Ranges");
                });

            modelBuilder.Entity("Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<int>("UserSettingsId");

                    b.HasKey("Id");

                    b.HasIndex("UserSettingsId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.Entities.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RangeId");

                    b.HasKey("Id");

                    b.HasIndex("RangeId");

                    b.ToTable("UsersSettings");
                });

            modelBuilder.Entity("Database.Entities.User", b =>
                {
                    b.HasOne("Database.Entities.UserSettings", "Settings")
                        .WithMany()
                        .HasForeignKey("UserSettingsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Entities.UserSettings", b =>
                {
                    b.HasOne("Database.Entities.Range", "RangeNav")
                        .WithMany()
                        .HasForeignKey("RangeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
