namespace Drive.Data.Entities.Models
{
    public class Comment : BaseEntity
    {
        public required string Content { get; set; }
        public int FileId { get; set; }
        public File File { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
