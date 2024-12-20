using Internship_7_Drive.Factories;
using Internship_7_Drive.Extensions;

var mainMenuActions = MainMenuFactory.CreateActions();
mainMenuActions.PrintActionsAndOpen();

/*
namespace Drive.Presentation
{
    public class Program
    {

        private static readonly AuthenticationRepository _userRepository = RepositoryFactory.Create<AuthenticationRepository>();
        private static int? _currentUserId = null;
        private static DateTime? _lastFailedLogin = null;

        public static void Main()
        {
            while (true)
            {
                if (_currentUserId.HasValue)
                    ShowMainMenu();
                else
                    ShowAuthenticationMenu();
            }
        }

        private static void ShowAuthenticationMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to DUMP Drive");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Select option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    HandleLogin();
                    break;
                case "2":
                    HandleRegistration();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    Thread.Sleep(1000);
                    break;
            }
        }

        private static void HandleLogin()
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
            var password = ReadPassword();

            var (result, message, userId) = _userRepository.Login(email!, password);

            if (result == ResponseResultType.Success)
            {
                _currentUserId = userId;
                _lastFailedLogin = null;
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine(message);
                _lastFailedLogin = DateTime.Now;
            }
            Thread.Sleep(2000);
        }

        private static void HandleRegistration()
        {
            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = ReadPassword();

            Console.Write("Confirm Password: ");
            var confirmPassword = ReadPassword();

            // Generate and show CAPTCHA
            var captcha = GenerateCaptcha();
            Console.WriteLine($"\nCAPTCHA: {captcha}");
            Console.Write("Enter CAPTCHA: ");
            var enteredCaptcha = Console.ReadLine();

            if (enteredCaptcha != captcha)
            {
                Console.WriteLine("Invalid CAPTCHA");
                Thread.Sleep(2000);
                return;
            }

            var (result, message) = _userRepository.Register(email!, password, confirmPassword);
            Console.WriteLine(message);
            Thread.Sleep(2000);
        }

        private static string GenerateCaptcha()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new StringBuilder();

            // Ensure at least one letter
            result.Append(chars[random.Next(26)]);
            // Ensure at least one number
            result.Append(chars[random.Next(26, chars.Length)]);

            // Add 4 more random characters
            for (int i = 0; i < 4; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            // Shuffle the string
            return new string(result.ToString().ToCharArray().OrderBy(x => random.Next()).ToArray());
        }

        private static string ReadPassword()
        {
            var password = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Length--;
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return password.ToString();
        }

        private static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. My Drive");
            Console.WriteLine("2. Shared with me");
            Console.WriteLine("3. Profile Settings");
            Console.WriteLine("4. Logout");
            Console.Write("Select option: ");

            switch (Console.ReadLine())
            {
                case "4":
                    _currentUserId = null;
                    break;
                default:
                    Console.WriteLine("Feature not implemented yet");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}*/