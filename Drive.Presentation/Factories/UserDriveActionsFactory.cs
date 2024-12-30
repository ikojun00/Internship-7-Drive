using Drive.Domain.Factories;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions.Comment;
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
                new UserDriveAddAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveUpdateFileAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveChangeFolderAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveBackAction(),
                new UserDriveDeleteAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveRenameAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveShareAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveUnshareAction(RepositoryFactory.Create<DriveRepository>()),
                new CommentDisplayAction(RepositoryFactory.Create<DriveRepository>(), RepositoryFactory.Create<CommentRepository>()),
                new CommentAddAction(RepositoryFactory.Create<DriveRepository>(), RepositoryFactory.Create<CommentRepository>()),
                new CommentDeleteAction(RepositoryFactory.Create<DriveRepository>(), RepositoryFactory.Create<CommentRepository>()),
                new CommentUpdateAction(RepositoryFactory.Create<DriveRepository>(), RepositoryFactory.Create<CommentRepository>()),
                new UserDriveExitAction()
            };

            var menuAction = new UserDriveAction(actions);
            return menuAction;
        }
    }
}
