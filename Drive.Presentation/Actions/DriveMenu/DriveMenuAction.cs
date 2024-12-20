using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.DriveMenu
{
    public class DriveMenuAction : BaseMenuAction
    {
        public DriveMenuAction(IList<IAction> actions) : base(actions)
        {
            Name = "Drive menu";
        }

        public override void Open()
        {
            Console.WriteLine("Drive management");
            base.Open();
        }
    }
}
