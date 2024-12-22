using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using System.Text;

namespace Internship_7_Drive.Actions.Authentication
{
    public class AuthenticationRegisterAction : IAction
    {
        private readonly UserRepository _userRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Register";

        public AuthenticationRegisterAction(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Open()
        {
            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            Console.Write("Confirm Password: ");
            var confirmPassword = Console.ReadLine();

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

        private string GenerateCaptcha()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new StringBuilder();

            result.Append(chars[random.Next(26)]);
            result.Append(chars[random.Next(26, chars.Length)]);

            for (int i = 0; i < 4; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return new string(result.ToString().ToCharArray().OrderBy(x => random.Next()).ToArray());
        }
    }
}
