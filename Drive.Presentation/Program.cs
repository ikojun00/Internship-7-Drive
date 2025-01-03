using Internship_7_Drive.Factories;
using Internship_7_Drive.Extensions;

var mainMenuActions = MainMenuFactory.CreateActions();
mainMenuActions.PrintActionsAndOpen();