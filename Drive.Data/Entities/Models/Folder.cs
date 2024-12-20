namespace Drive.Data.Entities.Models
{
    public class Folder : BaseEntity
    {
        public required string Name { get; set; }
        public int? ParentFolderId { get; set; }
        public Folder? ParentFolder { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;
        public ICollection<Folder>? SubFolders { get; set; }
        public ICollection<File>? Files { get; set; }
        public ICollection<User>? SharedWith { get; set; }
    }
}
