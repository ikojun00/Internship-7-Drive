using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.SharedDrive
{
    public class SharedDriveAction : BaseMenuAction
    {
        public SharedDriveAction(IList<IAction> actions) : base(actions)
        {
            Name = "Shared files/folders";
        }

        public override void Open()
        {
            Console.WriteLine("Shared files/folders management");
            base.Open();
        }
    }
}
