using Drive.Domain.Factories;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions.UserDrive;
using Internship_7_Drive.Actions.SharedDrive;

namespace Internship_7_Drive.Factories
{
    public class SharedDriveFactory
    {
        public static SharedDriveAction Create()
        {
            var actions = new List<IAction>
            {
                new SharedDriveDisplayAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveChangeFolderAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveUpdateFileAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveBackAction(),
                new UserDriveExitAction()
            };

            var menuAction = new SharedDriveAction(actions);
            return menuAction;
        }
    }
}
