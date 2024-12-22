using Internship_7_Drive.Abstractions;

namespace Internship_7_Drive.Actions.Profile
{
    public class ProfileAction : BaseMenuAction
    {
        public ProfileAction(IList<IAction> actions) : base(actions)
        {
            Name = "Profile menu";
        }

        public override void Open()
        {
            Console.WriteLine("Profile management");
            base.Open();
        }
    }
}
