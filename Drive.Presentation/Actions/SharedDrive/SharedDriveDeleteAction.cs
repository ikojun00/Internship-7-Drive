using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Helpers;

namespace Internship_7_Drive.Actions.SharedDrive
{
    public class SharedDriveDeleteAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "delete";

        public SharedDriveDeleteAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public void Open()
        {
            var input = UserContext.CurrentName?.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (input?.Length != 2 || (input[0] != "folder" && input[0] != "file"))
            {
                Writer.WriteInvalidCommand("delete [folder/file] [name]");
                return;
            }

            var itemType = input[0];
            var name = input[1];

            Console.Write($"Remove shared {itemType} '{name}' from your shared items? (yes/no): ");
            if (!Reader.TryReadConfirmation(out var confirmed) || !confirmed)
            {
                Console.WriteLine("Operation cancelled.");
                return;
            }

            var (result, message) = _driveRepository.RemoveFromShared(
                name,
                itemType == "folder",
                UserContext.CurrentUserId
            );

            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
