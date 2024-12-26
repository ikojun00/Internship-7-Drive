using Drive.Domain.Enums;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveRenameAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "rename";

        public UserDriveRenameAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            var input = UserContext.CurrentName?.Split(" to ", StringSplitOptions.RemoveEmptyEntries);
            if (input?.Length != 2)
            {
                Console.WriteLine("Invalid command format. Use: rename [old name] to [new name]");
                return;
            }

            var currentName = input[0];
            var newName = input[1];

            Console.Write($"Renaming '{currentName}' into '{newName}'? (yes/no): ");
            var confirmation = Console.ReadLine()?.ToLower();

            if (confirmation != "yes")
            {
                Console.WriteLine("Renaming cancelled.");
                return;
            }

            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var folderResult = _driveRepository.RenameFolder(
                currentName,
                newName,
                currentFolderId == 0 ? null : currentFolderId,
                UserContext.CurrentUserId
            );

            if (folderResult.Result == ResponseResultType.NotFound)
            {
                var fileResult = _driveRepository.RenameFile(
                    currentName,
                    newName,
                    currentFolderId == 0 ? null : currentFolderId,
                    UserContext.CurrentUserId
                );

                Console.WriteLine(fileResult.Message);
            }
            else
            {
                Console.WriteLine(folderResult.Message);
            }

            UserContext.CurrentName = null;
        }
    }
}
