using Drive.Domain.Factories;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions.UserDrive;
using Internship_7_Drive.Actions.SharedDrive;
using Internship_7_Drive.Actions.Comment;

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
                new CommentDisplayAction(RepositoryFactory.Create<DriveRepository>(), RepositoryFactory.Create<CommentRepository>()),
                new CommentAddAction(RepositoryFactory.Create<DriveRepository>(), RepositoryFactory.Create<CommentRepository>()),
                new CommentDeleteAction(RepositoryFactory.Create<DriveRepository>(), RepositoryFactory.Create<CommentRepository>()),
                new CommentUpdateAction(RepositoryFactory.Create<DriveRepository>(), RepositoryFactory.Create<CommentRepository>()),
                new UserDriveExitAction()
            };

            var menuAction = new SharedDriveAction(actions);
            return menuAction;
        }
    }
}
