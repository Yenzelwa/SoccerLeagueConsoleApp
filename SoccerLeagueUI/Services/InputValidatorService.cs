using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using SoccerLeagueClassLibrary.BusinessLogic;
using SoccerLeagueUI.InputHandlers;


namespace SoccerLeagueUI.Services
{

    public class InputValidatorService : IInputValidatorService
    {
        private readonly IServiceProvider _serviceProvider;

        public InputValidatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string GetInputChoice()
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Enter match data manually");
            Console.WriteLine("2. Upload match data from a file");
            Console.Write("Enter your choice (1 or 2): ");
            return Console.ReadLine();
        }

        public List<string> HandleUserChoice(string choice)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var inputLines = new List<string>();

                if (choice == "1")
                {
                    var manualInputHandler = scope.ServiceProvider.GetRequiredService<IManualInputHandler>();
                    inputLines = manualInputHandler.EnterData();
                }
                else if (choice == "2")
                {
                    var fileInputHandler = scope.ServiceProvider.GetRequiredService<IFileInputHandler>();
                    inputLines = fileInputHandler.ReadData();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select 1 or 2.");
                }

                return inputLines;
            }
        }

        public List<SoccerMatch> GetSoccerMatches(List<string> inputLines)
        {
            List<SoccerMatch> soccerMatches = new List<SoccerMatch>();

            foreach (var line in inputLines)
            {
                if (IsValid(line))
                {
                    SoccerMatch match = ParseMatch(line);
                    if (match != null)
                    {
                        soccerMatches.Add(match);
                    }
                }
            }

            return soccerMatches;
        }

        private  SoccerMatch? ParseMatch(string line)
        {
            string[] matchData = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (matchData.Length != 2)
            {
                return null; 
            }

            string teamAScore = matchData[0].Trim();
            string teamBScore = matchData[1].Trim();

            string[] teamAScoreData = ExtractTeamScoreData(teamAScore);
            string[] teamBScoreData = ExtractTeamScoreData(teamBScore);

            if (teamAScoreData.Length != 2 || teamBScoreData.Length != 2)
            {
                return null; 
            }

            string teamA = MyRegex().Replace(teamAScoreData[0], " ").Trim();
            int scoreA = int.Parse(teamAScoreData[1]);

            string teamB = MyRegex().Replace(teamBScoreData[0], " ").Trim();
            int scoreB = int.Parse(teamBScoreData[1]);

            return new SoccerMatch(teamA, scoreA, teamB, scoreB);
        }

        private  string[] ExtractTeamScoreData(string teamScore)
        {
            int lastSpaceIndex = teamScore.LastIndexOf(' ');
            if (lastSpaceIndex > 0)
            {
                string teamName = teamScore[..lastSpaceIndex].Trim();
                string score = teamScore[(lastSpaceIndex + 1)..].Trim();
                return new string[] { teamName, score };
            }

            return Array.Empty<string>(); 
        }

        public  bool IsValid(string inputLine)
        {
            // Matches lines in the format "TeamA ScoreA, TeamB ScoreB" with flexible spacing.
            string pattern = @"^\s*(.+?)\s+(\d+),\s+(.+?)\s+(\d+)\s*$";
            Regex regex = new Regex(pattern);
            bool isValid = regex.IsMatch(inputLine);
            return isValid;
        }

        private static Regex MyRegex()
        {
            return new Regex(@"[^a-zA-Z0-9]");
        }


    }
}
