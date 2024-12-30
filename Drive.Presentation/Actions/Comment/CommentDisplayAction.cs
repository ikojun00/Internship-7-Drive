using Drive.Domain.Enums;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.Comment
{
    public class CommentDisplayAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;
        private readonly CommentRepository _commentRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "show comments";

        public CommentDisplayAction(DriveRepository driveRepository, CommentRepository commentRepository)
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

            var (result, message, comments) = _commentRepository.GetFileComments(file.Id);

            if (comments.Count == 0)
            {
                Console.WriteLine("No comments found for this file.");
            }
            else
            {
                Console.WriteLine("\nComments:");
                Console.WriteLine("----------------------------------------");
                foreach (var comment in comments)
                {
                    Console.WriteLine($"ID: {comment.Id} - {comment.User.Email}");
                    Console.WriteLine($"Date: {comment.CreatedAt:g}");
                    if (comment.UpdatedAt.HasValue)
                    {
                        Console.WriteLine($"Edited: {comment.UpdatedAt:g}");
                    }
                    Console.WriteLine($"Content: {comment.Content}");
                    Console.WriteLine("----------------------------------------");
                }
            }

            UserContext.CurrentName = null;
        }
    }
}
