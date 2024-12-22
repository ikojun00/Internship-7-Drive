using Drive.Domain.Factories;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions;
using Internship_7_Drive.Actions.Profile;

namespace Internship_7_Drive.Factories
{
    public class ProfileActionsFactory
    {
        public static ProfileAction Create()
        {
            var actions = new List<IAction>
            {
                new ProfileChangeEmailAction(RepositoryFactory.Create<UserRepository>()),
                new ProfileChangePasswordAction(RepositoryFactory.Create<UserRepository>()),
                new ExitMenuAction()
            };

            var menuAction = new ProfileAction(actions);
            return menuAction;
        }
    }
}
