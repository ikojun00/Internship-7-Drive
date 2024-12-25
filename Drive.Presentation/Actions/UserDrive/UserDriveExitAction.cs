using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveExitAction : ICommandAction
    {
        public int MenuIndex { get; set; }
        public string Name { get; set; } = "exit";

        public UserDriveExitAction()
        {
        }

        public void Open()
        {
            UserContext.CurrentPath = null;
        }
    }
}
