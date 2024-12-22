using Drive.Domain.Enums;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Factories;

namespace Internship_7_Drive.Actions.Authentication
{
    public class AuthenticationLoginAction : IAction
    {
        private readonly UserRepository _userRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Login";

        public AuthenticationLoginAction(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Open()
        {
            if (UserContext.LastFailedLogin.HasValue && DateTime.Now - UserContext.LastFailedLogin.Value < TimeSpan.FromSeconds(30))
            {
                Console.WriteLine($"Please wait {30 - (DateTime.Now - UserContext.LastFailedLogin.Value).TotalSeconds:F0} seconds before trying again");
                Thread.Sleep(2000);
                return;
            }

            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var (result, message, userId) = _userRepository.Login(email, password);

            if (result == ResponseResultType.Success)
            {
                UserContext.CurrentUserId = userId;
                UserContext.LastFailedLogin = null;
                Console.WriteLine(message);
                var driveMenu = DriveActionsFactory.Create();
                driveMenu.Open();
            }
            else
            {
                Console.WriteLine(message);
                UserContext.LastFailedLogin = DateTime.Now;
            }
            Thread.Sleep(2000);
        }
    }
}
