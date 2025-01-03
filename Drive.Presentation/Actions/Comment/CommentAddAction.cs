using Drive.Domain.Enums;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.Comment
{
    public class CommentAddAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;
        private readonly CommentRepository _commentRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "comment";

        public CommentAddAction(DriveRepository driveRepository, CommentRepository commentRepository)
        {
            _driveRepository = driveRepository;
            _commentRepository = commentRepository;
        }

        public void Open()
        {
            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var (fileResult, fileMessage, file) = _driveRepository.GetFile(
                UserContext.CurrentName,
                currentFolderId,
                UserContext.CurrentUserId
            );

            if (fileResult != ResponseResultType.Success)
            {
                Console.WriteLine(fileMessage);
                UserContext.CurrentName = null;
                return;
            }

            Console.WriteLine("Enter your comment (press Enter to finish):");
            var content = Console.ReadLine();

            var (result, message) = _commentRepository.AddComment(
                file.Id,
                UserContext.CurrentUserId.Value,
                content
            );

            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
