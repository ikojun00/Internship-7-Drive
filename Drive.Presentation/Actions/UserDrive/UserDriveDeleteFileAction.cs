using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveDeleteFileAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "delete file";

        public UserDriveDeleteFileAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            Console.Write($"Jeste li sigurni da želite izbrisati datoteku '{UserContext.CurrentName}'? (da/ne): ");
            var confirmation = Console.ReadLine()?.ToLower();

            if (confirmation != "da")
            {
                Console.WriteLine("Brisanje datoteke je otkazano.");
                return;
            }

            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var (result, message) = _driveRepository.DeleteFile(
                UserContext.CurrentName,
                currentFolderId == 0 ? null : currentFolderId,
                UserContext.CurrentUserId
            );

            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
