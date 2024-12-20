using Drive.Data.Entities;
using Drive.Data.Entities.Models;
using Drive.Domain.Enums;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Drive.Domain.Repositories
{
    public class AuthenticationRepository : BaseRepository
    {
        public AuthenticationRepository(DriveDbContext dbContext) : base(dbContext) { }
        public (ResponseResultType Result, string Message, int? UserId) Login(string email, string password)
        {
            var user = DbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return (ResponseResultType.NotFound, "Invalid email or password", null);

            if (!VerifyPassword(password, user.PasswordHash))
                return (ResponseResultType.ValidationError, "Invalid email or password", null);

            return (ResponseResultType.Success, "Login successful", user.Id);
        }

        public (ResponseResultType Result, string Message) Register(string email, string password, string confirmPassword)
        {
            if (!ValidateEmail(email))
                return (ResponseResultType.ValidationError, "Invalid email format");

            if (password != confirmPassword)
                return (ResponseResultType.ValidationError, "Passwords do not match");

            if (DbContext.Users.Any(u => u.Email == email))
                return (ResponseResultType.AlreadyExists, "User with this email already exists");

            var user = new User
            {
                Email = email,
                PasswordHash = HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            DbContext.Users.Add(user);
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Registration successful")
                : (ResponseResultType.NoChanges, "Failed to register user");
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            var pattern = @"^[^@\s]+@[^@\s]{2,}\.[^@\s]{3,}$";
            return Regex.IsMatch(email, pattern);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
