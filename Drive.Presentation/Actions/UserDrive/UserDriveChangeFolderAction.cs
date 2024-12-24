using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveChangeFolderAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "change folder";

        public UserDriveChangeFolderAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            if (string.IsNullOrWhiteSpace(UserContext.CurrentName))
            {
                Console.WriteLine("Folder name cannot be empty.");
                return;
            }

            var currentFolderId = _driveRepository.GetFolderId(UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault());
            var targetFolder = _driveRepository.GetTargetFolder(UserContext.CurrentName, currentFolderId == 0 ? null : currentFolderId, UserContext.CurrentUserId);

            if (targetFolder == null)
            {
                Console.WriteLine($"Mapa '{UserContext.CurrentName}' nije pronađena.");
                return;
            }

            UserContext.CurrentPath += $"\\{UserContext.CurrentName}";
            Console.WriteLine($"Uspješno ste ušli u mapu '{UserContext.CurrentName}'");
            UserContext.CurrentName = null;
        }
    }
}
