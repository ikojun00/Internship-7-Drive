using Drive.Domain.Factories;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions.UserDrive;

namespace Internship_7_Drive.Factories
{
    public class UserDriveActionsFactory
    {
        public static UserDriveAction Create()
        {
            var actions = new List<IAction>
            {
                new UserDriveDisplayAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveAddFileAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveAddFolderAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveChangeFolderAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveBackAction(),
                new UserDriveDeleteFileAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveDeleteFolderAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveExitAction()
            };

            var menuAction = new UserDriveAction(actions);
            return menuAction;
        }
    }
}
