using Drive.Domain.Enums;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Helpers;

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
                Writer.WriteInvalidCommand("share [folder/file] [name] with [email]");
                return;
            }

            var inp = input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (inp?.Length != 2 || (inp[0] != "folder" && inp[0] != "file"))
            {
                Writer.WriteInvalidCommand("share [folder/file] [name] with [email]");
                return;
            }

            var itemType = inp[0];
            var name = inp[1];
            var email = input[1];

            Console.Write($"Sharing '{name}' with '{email}'? (yes/no): ");
            if (!Reader.TryReadConfirmation(out var confirmed) || !confirmed)
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
