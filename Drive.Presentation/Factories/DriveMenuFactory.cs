using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions;
using Internship_7_Drive.Actions.DriveMenu;

namespace Internship_7_Drive.Factories
{
    public class DriveMenuFactory
    {
        public static DriveMenuAction Create()
        {
            var actions = new List<IAction>
            {
                new ExitMenuAction(),
            };

            var menuAction = new DriveMenuAction(actions);
            return menuAction;

        }
    }
}
