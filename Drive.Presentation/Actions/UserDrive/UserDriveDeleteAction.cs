using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Helpers;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveDeleteAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "delete";

        public UserDriveDeleteAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            var input = UserContext.CurrentName?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (input?.Length != 2 || input[0] != "folder" && input[0] != "file")
            {
                Writer.WriteInvalidCommand("delete [folder/file] [name]");
                return;
            }

            var itemType = input[0];
            var name = input[1];

            Writer.WriteConfirmation(itemType, name, "Deleting");
            if (!Reader.TryReadConfirmation(out var confirmed) || !confirmed)
            {
                Writer.WriteCancellation(itemType, name, "Deleting");
                return;
            }

            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var (result, message) = itemType == "folder"
                ? _driveRepository.DeleteFolder(name, currentFolderId == 0 ? null : currentFolderId, UserContext.CurrentUserId)
                : _driveRepository.DeleteFile(name, currentFolderId == 0 ? null : currentFolderId, UserContext.CurrentUserId);

            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
