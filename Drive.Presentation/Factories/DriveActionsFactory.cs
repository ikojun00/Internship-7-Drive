using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions;

namespace Internship_7_Drive.Factories
{
    public class DriveActionsFactory
    {
        public static DriveAction Create()
        {
            var actions = new List<IAction>
            {
                UserDriveActionsFactory.Create(),
                SharedDriveFactory.Create(),
                ProfileActionsFactory.Create(),
                new ExitMenuAction(),
            };

            var menuAction = new DriveAction(actions);
            return menuAction;

        }
    }
}
