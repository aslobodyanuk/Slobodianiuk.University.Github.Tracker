﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Slobodianiuk.University.Github.Tracker.Database;

#nullable disable

namespace Slobodianiuk.University.Github.Tracker.Database.Migrations
{
    [DbContext(typeof(TrackerDbContext))]
    [Migration("20230301103457_Update-View-WIth-Alt-Name")]
    partial class UpdateViewWIthAltName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Slobodianiuk.University.Github.Tracker.Models.Database.Commit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int?>("CommitterId")
                        .HasColumnType("int");

                    b.Property<int>("FilesCount")
                        .HasColumnType("int");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RepositoryId")
                        .HasColumnType("int");

                    b.Property<string>("SHA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CommitterId");

                    b.HasIndex("RepositoryId");

                    b.HasIndex("StatsId");

                    b.ToTable("Commits");
                });

            modelBuilder.Entity("Slobodianiuk.University.Github.Tracker.Models.Database.GithubUserReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GithubUserReferences");
                });

            modelBuilder.Entity("Slobodianiuk.University.Github.Tracker.Models.Database.Procedure.GetRepositoryStatsResultItem", b =>
                {
                    b.Property<int>("Additions")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deletions")
                        .HasColumnType("int");

                    b.Property<int>("FilesCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepositoryId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);

                    b.ToView("View_RepositoryStats", (string)null);
                });

            modelBuilder.Entity("Slobodianiuk.University.Github.Tracker.Models.Database.Repository", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AltName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Repositories");
                });

            modelBuilder.Entity("Slobodianiuk.University.Github.Tracker.Models.Database.Stats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Additions")
                        .HasColumnType("int");

                    b.Property<int>("Deletions")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CommitStats");
                });

            modelBuilder.Entity("Slobodianiuk.University.Github.Tracker.Models.Database.Commit", b =>
                {
                    b.HasOne("Slobodianiuk.University.Github.Tracker.Models.Database.GithubUserReference", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Slobodianiuk.University.Github.Tracker.Models.Database.GithubUserReference", "Committer")
                        .WithMany()
                        .HasForeignKey("CommitterId");

                    b.HasOne("Slobodianiuk.University.Github.Tracker.Models.Database.Repository", null)
                        .WithMany("Commits")
                        .HasForeignKey("RepositoryId");

                    b.HasOne("Slobodianiuk.University.Github.Tracker.Models.Database.Stats", "Stats")
                        .WithMany()
                        .HasForeignKey("StatsId");

                    b.Navigation("Author");

                    b.Navigation("Committer");

                    b.Navigation("Stats");
                });

            modelBuilder.Entity("Slobodianiuk.University.Github.Tracker.Models.Database.Repository", b =>
                {
                    b.Navigation("Commits");
                });
#pragma warning restore 612, 618
        }
    }
}
