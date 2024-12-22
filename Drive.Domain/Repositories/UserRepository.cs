using Drive.Data.Entities;
using Drive.Data.Entities.Models;
using Drive.Domain.Enums;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Drive.Domain.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(DriveDbContext dbContext) : base(dbContext) { }
        public (ResponseResultType Result, string Message, int? UserId) Login(string email, string password)
        {
            if(!ValidateString(email) || !ValidateString(password))
                return (ResponseResultType.ValidationError, "Email or password is empty", null);

            var user = DbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return (ResponseResultType.NotFound, "Invalid email or password", null);

            if (!VerifyPassword(password, user.PasswordHash))
                return (ResponseResultType.ValidationError, "Invalid email or password", null);

            return (ResponseResultType.Success, "Login successful", user.Id);
        }

        public (ResponseResultType Result, string Message) Register(string email, string password, string confirmPassword)
        {
            if (!ValidateString(email) || !ValidateString(password))
                return (ResponseResultType.ValidationError, "Email or password is empty");

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

        public (ResponseResultType Result, string Message) ChangeEmail(int? userId, string newEmail)
        {
            if (!ValidateString(newEmail))
                return (ResponseResultType.ValidationError, "Email or password is empty");

            if (!ValidateEmail(newEmail))
                return (ResponseResultType.ValidationError, "Invalid email format");

            if (DbContext.Users.Any(u => u.Email == newEmail && u.Id != userId))
                return (ResponseResultType.AlreadyExists, "Email already in use");

            var user = DbContext.Users.Find(userId);
            if (user == null)
                return (ResponseResultType.NotFound, "User not found");

            user.Email = newEmail;
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Email changed successfully")
                : (ResponseResultType.NoChanges, "Failed to change email");
        }

        public (ResponseResultType Result, string Message) ChangePassword(int? userId, string currentPassword, string newPassword)
        {
            if (!ValidateString(newPassword))
                return (ResponseResultType.ValidationError, "New password is empty");

            var user = DbContext.Users.Find(userId);
            if (user == null)
                return (ResponseResultType.NotFound, "User not found");

            if (!VerifyPassword(currentPassword, user.PasswordHash))
                return (ResponseResultType.ValidationError, "Current password is incorrect");

            user.PasswordHash = HashPassword(newPassword);
            var result = SaveChanges();

            return result == ResponseResultType.Success
                ? (ResponseResultType.Success, "Password changed successfully")
                : (ResponseResultType.NoChanges, "Failed to change password");
        }

        private bool ValidateString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        private bool ValidateEmail(string email)
        {
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
