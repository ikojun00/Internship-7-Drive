using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions
{
    public class DriveAction : BaseMenuAction
    {
        public DriveAction(IList<IAction> actions) : base(actions)
        {
            Name = "Drive";
        }

        public override void Open()
        {
            Console.WriteLine("Drive management");
            base.Open();
        }
    }
}
