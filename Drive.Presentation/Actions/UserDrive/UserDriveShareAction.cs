using Drive.Domain.Enums;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveShareAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "share";

        public UserDriveShareAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            var input = UserContext.CurrentName?.Split(" with ", StringSplitOptions.RemoveEmptyEntries);

            if (input?.Length != 2)
            {
                Console.WriteLine("Invalid command format. Use: share [folder/file] [name] with [email]");
                return;
            }

            var inp = input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (inp?.Length != 2 || (inp[0] != "folder" && inp[0] != "file"))
            {
                Console.WriteLine("Invalid command format. Use: share [folder/file] [name] with [email]");
                return;
            }

            var itemType = inp[0];
            var name = inp[1];
            var email = input[1];

            Console.Write($"Sharing '{name}' with '{email}'? (yes/no): ");
            var confirmation = Console.ReadLine()?.ToLower();

            if (confirmation != "yes")
            {
                Console.WriteLine("Sharing cancelled.");
                return;
            }

            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var (result, message) = _driveRepository.ShareItem(
                name,
                email,
                itemType == "folder",
                currentFolderId,
                UserContext.CurrentUserId
            );

            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
