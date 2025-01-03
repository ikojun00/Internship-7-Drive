using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.Profile
{
    public class ProfileChangeEmailAction : IAction
    {
        private readonly UserRepository _userRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Change email";

        public ProfileChangeEmailAction(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Open()
        {
            Console.Write("Enter new email: ");
            var newEmail = Console.ReadLine();
            var (result, message) = _userRepository.ChangeEmail(UserContext.CurrentUserId, newEmail);

            Console.WriteLine(message);
            Thread.Sleep(2000);
        }
    }
}
