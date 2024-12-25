using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveDeleteFolderAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "delete folder";

        public UserDriveDeleteFolderAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            Console.Write($"Jeste li sigurni da želite izbrisati mapu '{UserContext.CurrentName}'? (da/ne): ");
            var confirmation = Console.ReadLine()?.ToLower();

            if (confirmation != "da")
            {
                Console.WriteLine("Brisanje mape je otkazano.");
                return;
            }

            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var (result, message) = _driveRepository.DeleteFolder(
                UserContext.CurrentName,
                currentFolderId == 0 ? null : currentFolderId,
                UserContext.CurrentUserId
            );

            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
