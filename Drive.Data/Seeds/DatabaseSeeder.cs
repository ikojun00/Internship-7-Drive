using Drive.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Data.Seeds
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var users = new List<User>
        {
            new User
            {
                Id = 1,
                Email = "john.doe@gmail.com",
                PasswordHash = "oQnjaUetVt4dyhzEnw74rJrZp7GqDfQfs8TLc8H/Aeo=", // Password123!
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = 2,
                Email = "jane.smith@gmail.com",
                PasswordHash = "qErJCXpDKt4m+zyvP95FuukV3gEGrD1so8mCtoZtMcE=", //MyPassw0rd!
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = 3,
                Email = "bob.wilson@gmail.com",
                PasswordHash = "qFYB54kbXvV24+kUz/cJy2WVfl9ifGPtY5t3plLC1YY=", //SecureP@ssw0rd
                CreatedAt = DateTime.UtcNow
            }
        };

            var folders = new List<Folder>
        {
            new Folder
            {
                Id = 1,
                Name = "Documents",
                OwnerId = 1,
                CreatedAt = DateTime.UtcNow,
                ParentFolderId = null
            },
            new Folder
            {
                Id = 2,
                Name = "Images",
                OwnerId = 1,
                CreatedAt = DateTime.UtcNow,
                ParentFolderId = null
            },
            new Folder
            {
                Id = 3,
                Name = "Projects",
                OwnerId = 2,
                CreatedAt = DateTime.UtcNow,
                ParentFolderId = null
            },
            new Folder
            {
                Id = 4,
                Name = "Work Documents",
                OwnerId = 2,
                CreatedAt = DateTime.UtcNow,
                ParentFolderId = 3
            }
        };

            var files = new List<File>
        {
            new File
            {
                Id = 1,
                Name = "report.txt",
                Content = "This is a report content",
                OwnerId = 1,
                FolderId = 1,
                CreatedAt = DateTime.UtcNow
            },
            new File
            {
                Id = 2,
                Name = "profile.jpg",
                Content = "base64_encoded_image_content",
                OwnerId = 1,
                FolderId = 2,
                CreatedAt = DateTime.UtcNow
            },
            new File
            {
                Id = 3,
                Name = "project_plan.txt",
                Content = "Project timeline and milestones",
                OwnerId = 2,
                FolderId = 3,
                CreatedAt = DateTime.UtcNow
            },
            new File
            {
                Id = 4,
                Name = "meeting_notes.txt",
                Content = "Notes from team meeting",
                OwnerId = 2,
                FolderId = 4,
                CreatedAt = DateTime.UtcNow
            }
        };

            // Seed Comments
            var comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                Content = "Great report, but needs more details in section 3",
                FileId = 1,
                UserId = 2,
                CreatedAt = DateTime.UtcNow
            },
            new Comment
            {
                Id = 2,
                Content = "Updated the timeline section",
                FileId = 3,
                UserId = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Comment
            {
                Id = 3,
                Content = "Please review the changes",
                FileId = 1,
                UserId = 1,
                CreatedAt = DateTime.UtcNow
            }
        };

            var fileShares = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                { "SharedWithId", 2 },
                { "SharedFilesId", 1 }
            },
            new Dictionary<string, object>
            {
                { "SharedWithId", 1 },
                { "SharedFilesId", 3 }
            }
        };

            var folderShares = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                { "SharedWithId", 2 },
                { "SharedFoldersId", 1 }
            },
            new Dictionary<string, object>
            {
                { "SharedWithId", 1 },
                { "SharedFoldersId", 3 }
            }
        };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Folder>().HasData(folders);
            modelBuilder.Entity<File>().HasData(files);
            modelBuilder.Entity<Comment>().HasData(comments);

            modelBuilder.Entity<User>()
                .HasMany(e => e.SharedFiles)
                .WithMany(e => e.SharedWith)
                .UsingEntity(j => j.HasData(fileShares));

            modelBuilder.Entity<User>()
                .HasMany(e => e.SharedFolders)
                .WithMany(e => e.SharedWith)
                .UsingEntity(j => j.HasData(folderShares));
        }
    }
}
