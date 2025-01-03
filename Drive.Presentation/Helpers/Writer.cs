namespace Internship_7_Drive.Helpers
{
    public class Writer
    {
        public static void WriteInvalidCommand(string expectedFormat)
        {
            Console.WriteLine($"Invalid command format. Use: {expectedFormat}");
        }

        public static void WriteConfirmation(string itemType, string name, string action)
        {
            Console.Write($"{action} {itemType} '{name}'? (yes/no): ");
        }

        public static void WriteCancellation(string itemType, string name, string action)
        {
            Console.WriteLine($"{action} {itemType} '{name}' cancelled.");
        }
    }
}
