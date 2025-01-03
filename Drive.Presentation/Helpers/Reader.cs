namespace Internship_7_Drive.Helpers
{
    public class Reader
    {
        public static bool TryReadConfirmation(out bool confirmed)
        {
            confirmed = false;
            var input = Console.ReadLine()?.ToLower();

            if (string.IsNullOrWhiteSpace(input))
                return false;

            confirmed = input == "yes";
            return true;
        }
    }
}
