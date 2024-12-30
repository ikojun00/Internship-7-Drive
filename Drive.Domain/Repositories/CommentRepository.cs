using Drive.Data.Entities;
using Drive.Data.Entities.Models;
using Drive.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Drive.Domain.Repositories
{
    public class CommentRepository : BaseRepository
    {
        public CommentRepository(DriveDbContext dbContext) : base(dbContext) { }

        public (ResponseResultType Result, string Message, List<Comment> Comments) GetFileComments(int fileId)
        {
            var comments = DbContext.Comments
                .Include(c => c.User)
                .Where(c => c.FileId == fileId)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            return (ResponseResultType.Success, "Comments retrieved successfully", comments);
        }

        public (ResponseResultType Result, string Message) AddComment(int fileId, int userId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return (ResponseResultType.ValidationError, "Comment content cannot be empty");

            var file = DbContext.Files
                .Include(f => f.SharedWith)
                .FirstOrDefault(f => f.Id == fileId);

            if (file == null)
                return (ResponseResultType.NotFound, "File not found");

            if (file.OwnerId != userId && !file.SharedWith.Any(u => u.Id == userId))
                return (ResponseResultType.ValidationError, "You don't have permission to comment on this file");

            var comment = new Comment
            {
                FileId = fileId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            DbContext.Comments.Add(comment);
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Comment added successfully")
                : (ResponseResultType.NoChanges, "Failed to add comment");
        }
        
        public (ResponseResultType Result, string Message) EditComment(int commentId, int userId, string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                return (ResponseResultType.ValidationError, "Comment content cannot be empty");

            var comment = DbContext.Comments.FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
                return (ResponseResultType.NotFound, "Comment not found");

            if (comment.UserId != userId)
                return (ResponseResultType.ValidationError, "You can only edit your own comments");

            comment.Content = newContent;
            comment.UpdatedAt = DateTime.UtcNow;

            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Comment updated successfully")
                : (ResponseResultType.NoChanges, "Failed to update comment");
        }

        public (ResponseResultType Result, string Message) DeleteComment(int commentId, int userId)
        {
            var comment = DbContext.Comments.FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
                return (ResponseResultType.NotFound, "Comment not found");

            var file = DbContext.Files.FirstOrDefault(f => f.Id == comment.FileId);
            if (comment.UserId != userId)
                return (ResponseResultType.ValidationError, "You can only delete your own comments");

            DbContext.Comments.Remove(comment);
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Comment deleted successfully")
                : (ResponseResultType.NoChanges, "Failed to delete comment");
        }
    }
}