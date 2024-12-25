using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    internal class UserDriveBackAction : ICommandAction
    {
        public int MenuIndex { get; set; }
        public string Name { get; set; } = "back";

        public void Open()
        {
            if (UserContext.CurrentPath == null) return;

            string[] pathParts = UserContext.CurrentPath.Split('\\', StringSplitOptions.RemoveEmptyEntries);
            if (pathParts.Length <= 1)
            {
                UserContext.CurrentPath = null;
                return;
            }

            UserContext.CurrentPath = string.Join('\\', pathParts.Take(pathParts.Length - 1));
        }
    }
}
