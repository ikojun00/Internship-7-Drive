namespace Drive.Data.Entities.Models
{
    public class File : BaseEntity
    {
        public required string Name { get; set; }
        public required string Content { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;
        public int FolderId { get; set; }
        public Folder Folder { get; set; } = null!;
        public ICollection<User>? SharedWith { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
