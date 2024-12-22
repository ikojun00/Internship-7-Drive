using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.Profile
{
    public class ProfileChangePasswordAction : IAction
    {
        private readonly UserRepository _userRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Change password";

        public ProfileChangePasswordAction(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Open()
        {
            Console.Write("Enter current password: ");
            var currentPassword = Console.ReadLine();

            Console.Write("Enter new password: ");
            var newPassword = Console.ReadLine();

            Console.Write("Confirm new password: ");
            var confirmPassword = Console.ReadLine();

            if (newPassword != confirmPassword)
            {
                Console.WriteLine("New passwords do not match");
                Thread.Sleep(2000);
                return;
            }

            var (result, message) = _userRepository.ChangePassword(UserContext.CurrentUserId, currentPassword!, newPassword!);

            Console.WriteLine(message);
            Thread.Sleep(2000);
        }
    }
}
