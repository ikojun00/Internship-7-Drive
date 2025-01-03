using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Helpers;

namespace Internship_7_Drive.Actions.Comment
{
    public class CommentUpdateAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;
        private readonly CommentRepository _commentRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "edit comment";

        public CommentUpdateAction(DriveRepository driveRepository, CommentRepository commentRepository)
        {
            _driveRepository = driveRepository;
            _commentRepository = commentRepository;
        }

        public void Open()
        {
            if (string.IsNullOrWhiteSpace(UserContext.CurrentName) || !int.TryParse(UserContext.CurrentName, out int commentId))
            {
                Writer.WriteInvalidCommand("edit comment [commentId]");
                return;
            }

            Console.WriteLine("Enter new comment content (press Enter on an empty line to finish):");
            var content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Comment content cannot be empty.");
                UserContext.CurrentName = null;
                return;
            }

            Console.Write($"Are you sure you want to edit comment {commentId}? (yes/no): ");

            if (!Reader.TryReadConfirmation(out var confirmed) || !confirmed)
            {
                Console.WriteLine("Comment edit cancelled.");
                UserContext.CurrentName = null;
                return;
            }

            var (result, message) = _commentRepository.EditComment(
                commentId,
                UserContext.CurrentUserId.Value,
                content
            );

            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
