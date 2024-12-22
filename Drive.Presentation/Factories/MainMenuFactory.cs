using Drive.Domain.Factories;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions;
using Internship_7_Drive.Actions.Authentication;
using Internship_7_Drive.Extensions;

namespace Internship_7_Drive.Factories
{
    public class MainMenuFactory
    {
        public static IList<IAction> CreateActions()
        {
            var actions = new List<IAction>
            {
                new AuthenticationLoginAction(RepositoryFactory.Create<UserRepository>()),
                new AuthenticationRegisterAction(RepositoryFactory.Create<UserRepository>()),
                new ExitMenuAction(),
            };

            actions.SetActionIndexes();

            return actions;
        }
    }
}
