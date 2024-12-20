using Drive.Domain.Enums;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Factories;

namespace Internship_7_Drive.Actions.Authentication
{
    public class AuthenticationLoginAction : IAction
    {
        private readonly AuthenticationRepository _authenticationRepository;
        // morat ce se ovo staviti u context zbog log-outa
        private static int? _currentUserId = null;
        private static DateTime? _lastFailedLogin = null;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Login";

        public AuthenticationLoginAction(AuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public void Open()
        {
            if (_lastFailedLogin.HasValue && DateTime.Now - _lastFailedLogin.Value < TimeSpan.FromSeconds(30))
            {
                Console.WriteLine($"Please wait {30 - (DateTime.Now - _lastFailedLogin.Value).TotalSeconds:F0} seconds before trying again");
                Thread.Sleep(2000);
                return;
            }

            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var (result, message, userId) = _authenticationRepository.Login(email!, password);

            if (result == ResponseResultType.Success)
            {
                _currentUserId = userId;
                _lastFailedLogin = null;
                Console.WriteLine(message);
                var driveMenu = DriveMenuFactory.Create();
                driveMenu.Open();
            }
            else
            {
                Console.WriteLine(message);
                _lastFailedLogin = DateTime.Now;
            }
            Thread.Sleep(2000);
        }
    }
}
