﻿using Drive.Domain.Factories;
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
                new UserDriveAddAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveUpdateFileAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveChangeFolderAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveBackAction(),
                new UserDriveDeleteAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveRenameAction(RepositoryFactory.Create<DriveRepository>()),
                new UserDriveExitAction()
            };

            var menuAction = new UserDriveAction(actions);
            return menuAction;
        }
    }
}
