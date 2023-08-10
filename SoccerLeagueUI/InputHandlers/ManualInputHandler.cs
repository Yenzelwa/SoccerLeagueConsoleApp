using Microsoft.Extensions.Logging;

namespace SoccerLeagueUI.InputHandlers
{
    public class ManualInputHandler : IManualInputHandler
    {
        private readonly ILogger<ManualInputHandler> _logger;

        public ManualInputHandler(ILogger<ManualInputHandler> logger)
        {
            _logger = logger;
        }
        public List<string> EnterData()
        {
            _logger.LogInformation("Manual input handling started.");
            var inputLines = new List<string>();
            DisplayInstructions();

            try
            {
                CollectMatchData(inputLines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while entering match data: {ErrorMessage}", ex.Message);
                Console.Write($"An error occurred while entering match data: {ex.Message}");
            }

            return inputLines;
        }

        private static void DisplayInstructions()
        {
            Console.WriteLine("Enter soccer match data in the format: TeamA ScoreA, TeamB ScoreB");
            Console.WriteLine("Example: Spain 3, Portugal 2");
        }

        private void CollectMatchData(List<string> inputLines)
        {
            while (true)
            {
                Console.Write("Enter match data or 'q' to quit: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "q")
                    break;

                _logger.LogInformation("Match data entered: {MatchData}", input);
                inputLines.Add(input);
            }
        }
    }
}
