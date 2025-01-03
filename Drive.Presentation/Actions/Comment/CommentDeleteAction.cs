using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Helpers;

namespace Internship_7_Drive.Actions.Comment
{
    public class CommentDeleteAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;
        private readonly CommentRepository _commentRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "remove comment";

        public CommentDeleteAction(DriveRepository driveRepository, CommentRepository commentRepository)
        {
            _driveRepository = driveRepository;
            _commentRepository = commentRepository;
        }

        public void Open()
        {
            if (string.IsNullOrWhiteSpace(UserContext.CurrentName) || !int.TryParse(UserContext.CurrentName, out int commentId))
            {
                Writer.WriteInvalidCommand("remove comment [commentId]");
                return;
            }

            Console.Write($"Are you sure you want to remove comment {commentId}? (yes/no): ");

            if (!Reader.TryReadConfirmation(out var confirmed) || !confirmed)
            {
                Console.WriteLine("Comment deletion cancelled.");
                UserContext.CurrentName = null;
                return;
            }

            var (result, message) = _commentRepository.DeleteComment(
                commentId,
                UserContext.CurrentUserId.Value
            );

            Console.WriteLine(message);
            UserContext.CurrentName = null;
        }
    }
}
