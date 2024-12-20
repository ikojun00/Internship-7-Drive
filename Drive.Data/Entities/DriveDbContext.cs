using Drive.Data.Entities.Models;
using Drive.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Data.Entities
{
    public class DriveDbContext : DbContext
    {
        public DriveDbContext(DbContextOptions<DriveDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired();

                entity.HasMany(e => e.OwnedFolders)
                    .WithOne(e => e.Owner)
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.OwnedFiles)
                    .WithOne(e => e.Owner)
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(e => e.ParentFolder)
                    .WithMany(e => e.SubFolders)
                    .HasForeignKey(e => e.ParentFolderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.SharedWith)
                    .WithMany(e => e.SharedFolders)
                    .UsingEntity(j => j.ToTable("FolderShares"));
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Content).IsRequired();

                entity.HasMany(e => e.SharedWith)
                    .WithMany(e => e.SharedFiles)
                    .UsingEntity(j => j.ToTable("FileShares"));

                entity.HasOne(e => e.Folder)
                    .WithMany(e => e.Files)
                    .HasForeignKey(e => e.FolderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
            });
            DatabaseSeeder.Seed(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        public class DriveDbContextFactory : IDesignTimeDbContextFactory<DriveDbContext>
        {
            public DriveDbContext CreateDbContext(string[] args)
            {
                // Hardcoded connection string
                var connectionString = "Host=localhost;Port=5432;Database=Internship-7-Drive;Username=postgres;Password=password";

                // Configure DbContext options
                var optionsBuilder = new DbContextOptionsBuilder<DriveDbContext>();
                optionsBuilder.UseNpgsql(connectionString);

                return new DriveDbContext(optionsBuilder.Options);
            }
        }
    }
}
