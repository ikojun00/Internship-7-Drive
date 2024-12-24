using Drive.Data.Entities;
using Drive.Data.Entities.Models;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Domain.Repositories
{
    public class DriveRepository : BaseRepository
    {
        public DriveRepository(DriveDbContext dbContext) : base(dbContext) { }

        public Folder? GetTargetFolder(string? folderName, int? currentFolderId, int? userId)
        {
            return DbContext.Folders
                .FirstOrDefault(f => f.Name == folderName &&
                                    f.ParentFolderId == currentFolderId &&
                                    f.OwnerId == userId);
        }

        public List<Folder> GetUserFolders(int? userId, int? folderId)
        {
            return DbContext.Folders
                .Where(f => f.OwnerId == userId && f.ParentFolderId == folderId)
                .OrderBy(f => f.Name)
                .ToList();
        }

        public List<File> GetUserFilesInFolder(int? userId, int? folderId)
        {
            return DbContext.Files
                .Where(f => f.OwnerId == userId && f.FolderId == folderId)
                .OrderByDescending(f => f.UpdatedAt)
                .ToList();
        }

        public int? GetFolderId(string? folderName)
        {
            return DbContext.Folders
                .Where(f => f.Name == folderName)
                .Select(f => f.Id)
                .FirstOrDefault();
        }
    }
}
