namespace Internship_7_Drive.Actions
{
    public class UserContext
    {
        public static int? CurrentUserId { get; set; }
        public static string? CurrentPath { get; set; }
        public static DateTime? LastFailedLogin { get; set; }
        public static string? CurrentName { get; set;}
    }
}
