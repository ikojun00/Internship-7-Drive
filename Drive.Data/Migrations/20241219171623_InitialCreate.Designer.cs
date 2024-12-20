﻿// <auto-generated />
using System;
using Drive.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Drive.Data.Migrations
{
    [DbContext(typeof(DriveDbContext))]
    [Migration("20241219171623_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Drive.Data.Entities.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FileId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Great report, but needs more details in section 3",
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(9159),
                            FileId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            Content = "Updated the timeline section",
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(9173),
                            FileId = 3,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Content = "Please review the changes",
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(9174),
                            FileId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FolderId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Files");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "This is a report content",
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(8161),
                            FolderId = 1,
                            Name = "report.txt",
                            OwnerId = 1
                        },
                        new
                        {
                            Id = 2,
                            Content = "base64_encoded_image_content",
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(8174),
                            FolderId = 2,
                            Name = "profile.jpg",
                            OwnerId = 1
                        },
                        new
                        {
                            Id = 3,
                            Content = "Project timeline and milestones",
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(8175),
                            FolderId = 3,
                            Name = "project_plan.txt",
                            OwnerId = 2
                        },
                        new
                        {
                            Id = 4,
                            Content = "Notes from team meeting",
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(8177),
                            FolderId = 4,
                            Name = "meeting_notes.txt",
                            OwnerId = 2
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int?>("ParentFolderId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("Folders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(6779),
                            Name = "Documents",
                            OwnerId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(7018),
                            Name = "Images",
                            OwnerId = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(7019),
                            Name = "Projects",
                            OwnerId = 2
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(7021),
                            Name = "Work Documents",
                            OwnerId = 2,
                            ParentFolderId = 3
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(5734),
                            Email = "john.doe@dump.hr",
                            PasswordHash = "hashed_password_1"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(5998),
                            Email = "jane.smith@dump.hr",
                            PasswordHash = "hashed_password_2"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 12, 19, 17, 16, 22, 778, DateTimeKind.Utc).AddTicks(6000),
                            Email = "bob.wilson@dump.hr",
                            PasswordHash = "hashed_password_3"
                        });
                });

            modelBuilder.Entity("FileUser", b =>
                {
                    b.Property<int>("SharedFilesId")
                        .HasColumnType("integer");

                    b.Property<int>("SharedWithId")
                        .HasColumnType("integer");

                    b.HasKey("SharedFilesId", "SharedWithId");

                    b.HasIndex("SharedWithId");

                    b.ToTable("FileShares", (string)null);

                    b.HasData(
                        new
                        {
                            SharedFilesId = 1,
                            SharedWithId = 2
                        },
                        new
                        {
                            SharedFilesId = 3,
                            SharedWithId = 1
                        });
                });

            modelBuilder.Entity("FolderUser", b =>
                {
                    b.Property<int>("SharedFoldersId")
                        .HasColumnType("integer");

                    b.Property<int>("SharedWithId")
                        .HasColumnType("integer");

                    b.HasKey("SharedFoldersId", "SharedWithId");

                    b.HasIndex("SharedWithId");

                    b.ToTable("FolderShares", (string)null);

                    b.HasData(
                        new
                        {
                            SharedFoldersId = 1,
                            SharedWithId = 2
                        },
                        new
                        {
                            SharedFoldersId = 3,
                            SharedWithId = 1
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Comment", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.File", "File")
                        .WithMany("Comments")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.File", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Folder", "Folder")
                        .WithMany("Files")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.User", "Owner")
                        .WithMany("OwnedFiles")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folder", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.User", "Owner")
                        .WithMany("OwnedFolders")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.Folder", "ParentFolder")
                        .WithMany("SubFolders")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Owner");

                    b.Navigation("ParentFolder");
                });

            modelBuilder.Entity("FileUser", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.File", null)
                        .WithMany()
                        .HasForeignKey("SharedFilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("SharedWithId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FolderUser", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Folder", null)
                        .WithMany()
                        .HasForeignKey("SharedFoldersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("SharedWithId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.File", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folder", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("SubFolders");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.User", b =>
                {
                    b.Navigation("OwnedFiles");

                    b.Navigation("OwnedFolders");
                });
#pragma warning restore 612, 618
        }
    }
}
