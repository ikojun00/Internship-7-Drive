namespace Drive.Data.Entities.Models
{
    public class User : BaseEntity
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public ICollection<Folder>? OwnedFolders { get; set; }
        public ICollection<File>? OwnedFiles { get; set; }
        public ICollection<Folder>? SharedFolders { get; set; }
        public ICollection<File>? SharedFiles { get; set; }
    }
}
