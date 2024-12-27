using Drive.Data.Entities;
using Drive.Data.Entities.Models;
using Drive.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Domain.Repositories
{
    public class DriveRepository : BaseRepository
    {
        public DriveRepository(DriveDbContext dbContext) : base(dbContext) { }

        public (ResponseResultType Result, string Message, Folder? Folder) GetTargetFolder(string? folderName, int? currentFolderId, int? userId)
        {
            if (!ValidateString(folderName))
                return (ResponseResultType.ValidationError, "Folder name cannot be empty", null);

            var folder = DbContext.Folders
                .FirstOrDefault(f => f.Name == folderName &&
                                   f.ParentFolderId == currentFolderId &&
                                   (f.OwnerId == userId || f.SharedWith.Any(u => u.Id == userId)));

            return folder == null
                ? (ResponseResultType.NotFound, $"Folder '{folderName}' not found", null)
                : (ResponseResultType.Success, "Folder found successfully", folder);
        }

        public (ResponseResultType Result, string Message, List<Folder> Folders) GetUserFolders(int? userId, int? folderId)
        {
            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null", new List<Folder>());

            var folders = DbContext.Folders
                .Where(f => f.OwnerId == userId && f.ParentFolderId == folderId)
                .OrderBy(f => f.Name)
                .ToList();

            return (ResponseResultType.Success, "Folders retrieved successfully", folders);
        }

        public (ResponseResultType Result, string Message, List<File> Files) GetUserFilesInFolder(int? userId, int? folderId)
        {
            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null", new List<File>());

            var files = DbContext.Files
                .Where(f => f.OwnerId == userId && f.FolderId == folderId)
                .OrderByDescending(f => f.UpdatedAt)
                .ToList();

            return (ResponseResultType.Success, "Files retrieved successfully", files);
        }

        public (ResponseResultType Result, string Message, int? FolderId) GetFolderId(string? folderName)
        {
            if (!ValidateString(folderName))
                return (ResponseResultType.ValidationError, "Folder name cannot be empty", null);

            var folderId = DbContext.Folders
                .Where(f => f.Name == folderName)
                .Select(f => f.Id)
                .FirstOrDefault();

            return (ResponseResultType.Success, "Folder ID retrieved", folderId);
        }

        public (ResponseResultType Result, string Message) CreateFolder(string? folderName, int? parentFolderId, int? userId)
        {
            if (!ValidateString(folderName))
                return (ResponseResultType.ValidationError, "Folder name cannot be empty");

            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null");

            if (DbContext.Folders.Any(f => f.Name == folderName &&
                                         f.ParentFolderId == parentFolderId &&
                                         f.OwnerId == userId))
            {
                return (ResponseResultType.AlreadyExists, $"Folder '{folderName}' already exists in this location");
            }

            var folder = new Folder
            {
                Name = folderName,
                ParentFolderId = parentFolderId,
                OwnerId = (int)userId,
                CreatedAt = DateTime.UtcNow
            };

            DbContext.Folders.Add(folder);
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Folder created successfully")
                : (ResponseResultType.NoChanges, "Failed to create folder");
        }

        public (ResponseResultType Result, string Message) DeleteFolder(string folderName, int? parentFolderId, int? userId)
        {
            if (!ValidateString(folderName))
                return (ResponseResultType.ValidationError, "Folder name cannot be empty");

            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null");

            var folder = DbContext.Folders
                .FirstOrDefault(f => f.Name == folderName &&
                                    f.ParentFolderId == parentFolderId &&
                                    f.OwnerId == userId);

            if (folder == null)
                return (ResponseResultType.NotFound, $"Folder '{folderName}' not found");

            
            var hasSubfolders = DbContext.Folders.Any(f => f.ParentFolderId == folder.Id);
            var hasFiles = DbContext.Files.Any(f => f.FolderId == folder.Id);

            if (hasSubfolders || hasFiles)
                return (ResponseResultType.ValidationError, "Cannot delete folder because it's not empty");

            DbContext.Folders.Remove(folder);
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Folder deleted successfully")
                : (ResponseResultType.NoChanges, "Failed to delete folder");
            
        }

        public (ResponseResultType Result, string Message) CreateFile(string fileName, int? folderId, int? userId)
        {
            if (!ValidateString(fileName))
                return (ResponseResultType.ValidationError, "File name cannot be empty");

            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null");

            if (folderId == null)
                return (ResponseResultType.ValidationError, "File must be inside of folder");

            if (!fileName.Contains('.'))
                return (ResponseResultType.ValidationError, "File must have an extension (e.g. .txt, .doc)");

            if (DbContext.Files.Any(f => f.Name == fileName &&
                                       f.FolderId == folderId &&
                                       f.OwnerId == userId))
            {
                return (ResponseResultType.AlreadyExists, $"File '{fileName}' already exists in this location");
            }

            
            var file = new File
            {
                Name = fileName,
                FolderId = (int)folderId,
                OwnerId = (int)userId,
                Content = string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            DbContext.Files.Add(file);
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "File created successfully")
                : (ResponseResultType.NoChanges, "Failed to create file");
        }

        public (ResponseResultType Result, string Message) DeleteFile(string fileName, int? folderId, int? userId)
        {
            if (!ValidateString(fileName))
                return (ResponseResultType.ValidationError, "File name cannot be empty");

            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null");

            var file = DbContext.Files
                .FirstOrDefault(f => f.Name == fileName &&
                                    f.FolderId == folderId &&
                                    f.OwnerId == userId);

            if (file == null)
                return (ResponseResultType.NotFound, $"File '{fileName}' not found");

            DbContext.Files.Remove(file);
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "File deleted successfully")
                : (ResponseResultType.NoChanges, "Failed to delete file");
            
        }

        public (ResponseResultType Result, string Message) RenameFolder(string currentName, string newName, int? parentFolderId, int? userId)
        {
            if (!ValidateString(currentName) || !ValidateString(newName))
                return (ResponseResultType.ValidationError, "Folder names cannot be empty");

            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null");

            var folder = DbContext.Folders
                .FirstOrDefault(f => f.Name == currentName &&
                                    f.ParentFolderId == parentFolderId &&
                                    f.OwnerId == userId);

            if (folder == null)
                return (ResponseResultType.NotFound, $"Folder '{currentName}' not found");

            if (DbContext.Folders.Any(f => f.Name == newName &&
                                          f.ParentFolderId == parentFolderId &&
                                          f.OwnerId == userId))
            {
                return (ResponseResultType.AlreadyExists, $"Folder '{newName}' already exists in this location");
            }

            folder.Name = newName;
            folder.UpdatedAt = DateTime.UtcNow;
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Folder renamed successfully")
                : (ResponseResultType.NoChanges, "Failed to rename folder");
        }

        public (ResponseResultType Result, string Message) RenameFile(string currentName, string newName, int? folderId, int? userId)
        {
            if (!ValidateString(currentName) || !ValidateString(newName))
                return (ResponseResultType.ValidationError, "File names cannot be empty");

            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null");

            if (!newName.Contains('.'))
                return (ResponseResultType.ValidationError, "File must have an extension (e.g. .txt, .doc)");

            var file = DbContext.Files
                .FirstOrDefault(f => f.Name == currentName &&
                                    f.FolderId == folderId &&
                                    f.OwnerId == userId);

            if (file == null)
                return (ResponseResultType.NotFound, $"File '{currentName}' not found");

            if (DbContext.Files.Any(f => f.Name == newName &&
                                        f.FolderId == folderId &&
                                        f.OwnerId == userId))
            {
                return (ResponseResultType.AlreadyExists, $"File '{newName}' already exists in this location");
            }

            file.Name = newName;
            file.UpdatedAt = DateTime.UtcNow;
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "File renamed successfully")
                : (ResponseResultType.NoChanges, "Failed to rename file");
        }

        public (ResponseResultType Result, string Message, File? File) GetFile(string? fileName, int? folderId, int? userId)
        {
            if (!ValidateString(fileName))
                return (ResponseResultType.ValidationError, "File name cannot be empty", null);

            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null", null);

            // folderId == null is implemented for getting shared file, it works
            // because we can not add file without folderId in userDrive
            var file = DbContext.Files
                .Include(f => f.SharedWith)
                .FirstOrDefault(f => f.Name == fileName &&
                                    (folderId == null || f.FolderId == folderId) &&
                                    (f.OwnerId == userId || f.SharedWith.Any(u => u.Id == userId)));

            return file == null
                ? (ResponseResultType.NotFound, $"File '{fileName}' not found", null)
                : (ResponseResultType.Success, "File found successfully", file);
        }

        public (ResponseResultType Result, string Message) UpdateFileContent(int fileId, string content)
        {
            var file = DbContext.Files.Find(fileId);

            if (file == null)
                return (ResponseResultType.NotFound, "File not found");

            file.Content = content;
            file.UpdatedAt = DateTime.UtcNow;

            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "File updated successfully")
                : (ResponseResultType.NoChanges, "Failed to update file");
        }

        public (ResponseResultType Result, string Message) ShareItem(string itemName, string recipientEmail, bool isFolder, int? currentFolderId, int? ownerId)
        {
            if (!ValidateString(itemName) || !ValidateString(recipientEmail))
                return (ResponseResultType.ValidationError, "Item name or recipient email cannot be empty");

            if (ownerId == null)
                return (ResponseResultType.ValidationError, "Owner ID cannot be null");

            var recipient = DbContext.Users.FirstOrDefault(u => u.Email == recipientEmail);
            if (recipient == null)
                return (ResponseResultType.NotFound, "Recipient not found");

            if (recipient.Id == ownerId)
                return (ResponseResultType.ValidationError, "Cannot share with yourself");

            if (isFolder)
            {
                var folder = DbContext.Folders
                    .Include(f => f.SharedWith)
                    .FirstOrDefault(f => f.Name == itemName &&
                                       f.ParentFolderId == currentFolderId &&
                                       f.OwnerId == ownerId);

                if (folder == null)
                    return (ResponseResultType.NotFound, $"Folder '{itemName}' not found");

                if (folder.SharedWith.Any(u => u.Id == recipient.Id))
                    return (ResponseResultType.AlreadyExists, $"Folder '{itemName}' is already shared with {recipientEmail}");

                folder.SharedWith.Add(recipient);
            }
            else
            {
                var file = DbContext.Files
                    .Include(f => f.SharedWith)
                    .FirstOrDefault(f => f.Name == itemName &&
                                       f.FolderId == currentFolderId &&
                                       f.OwnerId == ownerId);

                if (file == null)
                    return (ResponseResultType.NotFound, $"File '{itemName}' not found");

                if (file.SharedWith.Any(u => u.Id == recipient.Id))
                    return (ResponseResultType.AlreadyExists, $"File '{itemName}' is already shared with {recipientEmail}");

                file.SharedWith.Add(recipient);
            }

            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, $"{(isFolder ? "Folder" : "File")} shared successfully with {recipientEmail}")
                : (ResponseResultType.NoChanges, $"Failed to share {(isFolder ? "folder" : "file")}");
        }

        public (ResponseResultType Result, string Message) StopSharing(string itemName, string recipientEmail, bool isFolder, int? currentFolderId, int? ownerId)
        {
            if (!ValidateString(itemName) || !ValidateString(recipientEmail))
                return (ResponseResultType.ValidationError, "Item name or recipient email cannot be empty");

            if (ownerId == null)
                return (ResponseResultType.ValidationError, "Owner ID cannot be null");

            var recipient = DbContext.Users.FirstOrDefault(u => u.Email == recipientEmail);
            if (recipient == null)
                return (ResponseResultType.NotFound, "Recipient not found");

            if (isFolder)
            {
                var folder = DbContext.Folders
                    .Include(f => f.SharedWith)
                    .FirstOrDefault(f => f.Name == itemName &&
                                       f.ParentFolderId == currentFolderId &&
                                       f.OwnerId == ownerId);

                if (folder == null)
                    return (ResponseResultType.NotFound, $"Folder '{itemName}' not found");

                if (!folder.SharedWith.Any(u => u.Id == recipient.Id))
                    return (ResponseResultType.NotFound, $"Folder '{itemName}' is not shared with {recipientEmail}");

                folder.SharedWith.Remove(recipient);
            }
            else
            {
                var file = DbContext.Files
                    .Include(f => f.SharedWith)
                    .FirstOrDefault(f => f.Name == itemName &&
                                       f.FolderId == currentFolderId &&
                                       f.OwnerId == ownerId);

                if (file == null)
                    return (ResponseResultType.NotFound, $"File '{itemName}' not found");

                if (!file.SharedWith.Any(u => u.Id == recipient.Id))
                    return (ResponseResultType.NotFound, $"File '{itemName}' is not shared with {recipientEmail}");

                file.SharedWith.Remove(recipient);
            }

            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, $"Stopped sharing {(isFolder ? "folder" : "file")} with {recipientEmail}")
                : (ResponseResultType.NoChanges, $"Failed to stop sharing {(isFolder ? "folder" : "file")}");
        }

        public (ResponseResultType Result, string Message, List<Folder> Folders) GetSharedFolders(int? userId, int? folderId)
        {
            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null", new List<Folder>());

            var folders = DbContext.Folders
                .Where(f => f.SharedWith.Any(u => u.Id == userId) && f.ParentFolderId == folderId)
                .OrderBy(f => f.Name)
                .ToList();

            return (ResponseResultType.Success, "Shared folders retrieved successfully", folders);
        }

        public (ResponseResultType Result, string Message, List<File> Files) GetSharedFiles(int? userId)
        {
            if (userId == null)
                return (ResponseResultType.ValidationError, "User ID cannot be null", new List<File>());

            var files = DbContext.Files
                .Where(f => f.SharedWith.Any(u => u.Id == userId))
                .OrderByDescending(f => f.UpdatedAt)
                .ToList();

            return (ResponseResultType.Success, "Shared files retrieved successfully", files);
        }

        private bool ValidateString(string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}