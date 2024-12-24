using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveAction : BaseMenuAction
    {
        public UserDriveAction(IList<IAction> actions) : base(actions)
        {
            Name = "User Drive menu";
        }

        public override void Open()
        {
            Console.WriteLine("User Drive management");
            base.Open();
        }
    }
}
