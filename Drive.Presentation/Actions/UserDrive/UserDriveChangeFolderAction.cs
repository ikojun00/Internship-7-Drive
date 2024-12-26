using Drive.Domain.Enums;
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
            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var (result, message, targetFolder) = _driveRepository.GetTargetFolder(
                UserContext.CurrentName,
                currentFolderId == 0 ? null : currentFolderId,
                UserContext.CurrentUserId
            );

            if (result != ResponseResultType.Success)
            {
                Console.WriteLine(message);
                return;
            }

            UserContext.CurrentPath += $"\\{UserContext.CurrentName}";
            Console.WriteLine($"Entered in folder '{UserContext.CurrentName}' successfully.");
            UserContext.CurrentName = null;
        }
    }
}
