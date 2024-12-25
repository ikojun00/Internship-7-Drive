using Internship_7_Drive.Abstractions;
using Internship_7_Drive.Actions;
using Internship_7_Drive.Actions.UserDrive;

namespace Internship_7_Drive.Extensions
{
    public static class ActionExtensions
    {
        public static void PrintActionsAndOpen(this IList<IAction> actions)
        {
            if (actions.Any(a => a is ICommandAction))
            {
                ProcessCommandActions(actions);
                return;
            }

            ProcessMenuActions(actions);
        }

        private static void ProcessMenuActions(IList<IAction> actions)
        {
            const string INVALID_INPUT_MSG = "Please type in number!";
            const string INVALID_ACTION_MSG = "Please select valid action!";

            var isExitSelected = false;
            do
            {
                PrintNumberedActions(actions);
                Console.Write("\nYour choice: ");

                var isValidInput = int.TryParse(Console.ReadLine(), out var actionIndex);
                if (!isValidInput)
                {
                    PrintErrorMessage(INVALID_INPUT_MSG);
                    continue;
                }

                var action = actions.FirstOrDefault(a => a.MenuIndex == actionIndex);
                if (action is null)
                {
                    PrintErrorMessage(INVALID_ACTION_MSG);
                    continue;
                }

                Console.Clear();
                action.Open();

                isExitSelected = action is ExitMenuAction;
            } while (!isExitSelected);
        }

        private static void ProcessCommandActions(IList<IAction> actions)
        {
            var isExitSelected = false;
            do
            {
                Console.Write(UserContext.CurrentPath + ">");

                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input == "help")
                {
                    PrintAvailableCommands(actions);
                    continue;
                }

                var action = actions.FirstOrDefault(a =>
                    input.StartsWith(a.Name));

                if (action is null)
                {
                    PrintErrorMessage("Unknown command. Type 'help' to see available commands.");
                    continue;
                }
                UserContext.CurrentName = input.Substring(action.Name.Length).Trim();
                action.Open();

                isExitSelected = action is UserDriveExitAction;
            } while (!isExitSelected);
        }

        public static void SetActionIndexes(this IList<IAction> actions)
        {
            var index = 0;
            foreach (var action in actions)
            {
                if (!(action is ICommandAction))
                {
                    action.MenuIndex = ++index;
                }
            }
        }

        private static void PrintNumberedActions(IList<IAction> actions)
        {
            foreach (var action in actions.Where(a => !(a is ICommandAction)))
            {
                Console.WriteLine($"{action.MenuIndex}. {action.Name}");
            }
        }

        private static void PrintAvailableCommands(IList<IAction> actions)
        {
            Console.WriteLine("\nAvailable commands:");
            foreach (var action in actions.Where(a => a is ICommandAction))
            {
                Console.WriteLine($"- {action.Name}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void PrintErrorMessage(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(1000);
        }
    }
}