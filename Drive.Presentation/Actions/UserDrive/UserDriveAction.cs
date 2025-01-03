using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveAction : BaseMenuAction
    {
        public UserDriveAction(IList<IAction> actions) : base(actions)
        {
            Name = "My drive";
        }

        public override void Open()
        {
            Console.WriteLine("My drive management");
            base.Open();
        }
    }
}
