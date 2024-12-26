using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveAddAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "add";

        public UserDriveAddAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            var input = UserContext.CurrentName?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (input?.Length != 2 || input[0] != "folder" && input[0] != "file")
            {
                Console.WriteLine("Invalid command format. Use: add [folder/file] [name]");
                return;
            }

            var itemType = input[0];
            var name = input[1];

            Console.Write($"Adding {itemType} '{name}'? (yes/no): ");
            var confirmation = Console.ReadLine()?.ToLower();

            if (confirmation != "yes")
            {
                Console.WriteLine($"Adding {itemType} '{name}' cancelled.");
                return;
            }

            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var (result, message) = itemType == "folder"
                ? _driveRepository.CreateFolder(name, currentFolderId == 0 ? null : currentFolderId, UserContext.CurrentUserId)
                : _driveRepository.CreateFile(name, currentFolderId == 0 ? null : currentFolderId, UserContext.CurrentUserId);


            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
