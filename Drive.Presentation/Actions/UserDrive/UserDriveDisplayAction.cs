using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveDisplayAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "display";

        public UserDriveDisplayAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            var pathParts = UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries) ?? [];
            var currentFolderName = pathParts.Length > 0 ? pathParts[^1] : null;

            var (folderIdResult, _, folderId) = _driveRepository.GetFolderId(currentFolderName);

            var (folderResult, _, folders) = _driveRepository.GetUserFolders(UserContext.CurrentUserId, folderId);
            var (fileResult, _, files) = _driveRepository.GetUserFilesInFolder(UserContext.CurrentUserId, folderId);

            if (folders.Any())
            {
                Console.WriteLine("\nFolders:");
                foreach (var folder in folders)
                {
                    Console.WriteLine($"{folder.Name}");
                }
            }

            if (files.Any())
            {
                Console.WriteLine("\nFiles:");
                foreach (var file in files)
                {
                    Console.WriteLine($"{file.Name} (Last modified: {file.UpdatedAt ?? file.CreatedAt})");
                }
            }

            if (!folders.Any() && !files.Any())
            {
                Console.WriteLine("Empty.");
            }
        }
    }
}
