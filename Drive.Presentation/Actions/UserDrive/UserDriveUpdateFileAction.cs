using Drive.Domain.Enums;
using Drive.Domain.Repositories;
using Internship_7_Drive.Abstractions;
using File = Drive.Data.Entities.Models.File;

namespace Internship_7_Drive.Actions.UserDrive
{
    public class UserDriveUpdateFileAction : ICommandAction
    {
        private readonly DriveRepository _driveRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "update file";

        private readonly List<string> _lines;
        private int _currentLine;
        private bool _isEditing;

        public UserDriveUpdateFileAction(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
            _lines = new List<string>();
            _currentLine = 0;
            _isEditing = true;
        }

        public void Open()
        {
            var (folderIdResult, _, currentFolderId) = _driveRepository.GetFolderId(
                UserContext.CurrentPath?.Split('\\', StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            );

            var (result, message, file) = _driveRepository.GetFile(
                UserContext.CurrentName,
                currentFolderId == 0 ? null : currentFolderId,
                UserContext.CurrentUserId
            );

            if (result != ResponseResultType.Success)
            {
                Console.WriteLine(message);
                return;
            }

            _lines.Clear();
            _lines.AddRange(file.Content.Split('\n'));
            _currentLine = _lines.Count;

            Console.WriteLine($"\nEditing file: {file.Name}");
            Console.WriteLine("Commands available:");
            Console.WriteLine(":help - Show this help message");
            Console.WriteLine(":save - Save changes and exit");
            Console.WriteLine(":quit - Exit without saving");
            Console.WriteLine("Press Enter on empty line to start new line");
            Console.WriteLine("Press Backspace on empty line to go to previous line");
            Console.WriteLine("\nCurrent content:");
            PrintContent();

            while (_isEditing)
            {
                Console.Write($"Line {_currentLine + 1}> ");
                var input = Console.ReadLine();

                if (input == null) continue;

                if (input.StartsWith(':'))
                {
                    HandleCommand(input, file);
                    continue;
                }

                if (string.IsNullOrEmpty(input) && _currentLine > 0)
                {
                    Console.Write("Go to previous line? (yes/no): ");
                    var goBack = Console.ReadLine()?.ToLower() == "yes";
                    if (goBack)
                    {
                        _currentLine--;
                        Console.WriteLine($"Current line: {_lines[_currentLine]}");
                    }
                    continue;
                }

                if (_currentLine < _lines.Count)
                {
                    _lines[_currentLine] = input;
                }
                else
                {
                    _lines.Add(input);
                }
                _currentLine++;
            }

            UserContext.CurrentName = null;
        }

        private void HandleCommand(string command, File file)
        {
            switch (command.ToLower())
            {
                case ":help":
                    Console.WriteLine("\nCommands available:");
                    Console.WriteLine(":help - Show this help message");
                    Console.WriteLine(":save - Save changes and exit");
                    Console.WriteLine(":quit - Exit without saving");
                    Console.WriteLine("Press Enter on empty line to start new line");
                    Console.WriteLine("Press Backspace on empty line to go to previous line\n");
                    break;

                case ":save":
                    Console.Write("Save updates? (yes/no): ");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        var content = string.Join('\n', _lines);
                        var (updateResult, updateMessage) = _driveRepository.UpdateFileContent(file.Id, content);
                        Console.WriteLine(updateMessage);
                        _isEditing = false;
                    }
                    break;

                case ":quit":
                    Console.Write("Exit without saving? (yes/no): ");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        _isEditing = false;
                    }
                    break;

                default:
                    Console.WriteLine("Unknown command. Type :help for available commands.");
                    break;
            }
        }

        private void PrintContent()
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                Console.WriteLine($"Line {i + 1}: {_lines[i]}");
            }
            Console.WriteLine();
        }
    }
}
