using Microsoft.Extensions.Logging;

namespace SoccerLeagueUI.Services
{
    public class SoccerLeagueService : ISoccerLeagueService
    {
        private readonly ILogger<SoccerLeagueService> _logger;
        private readonly ISoccerMatchProcessorService _soccerMatchProcessorService;
        private readonly IInputValidatorService  _inputValidatorService;

        public SoccerLeagueService(ILogger<SoccerLeagueService> logger,
        ISoccerMatchProcessorService soccerMatchProcessorService,
        IInputValidatorService inputValidatorService)
        {
            _logger = logger;
            _soccerMatchProcessorService = soccerMatchProcessorService;
            _inputValidatorService = inputValidatorService;
        }

        public void Run()
        {
            try
            {
                string choice = _inputValidatorService.GetInputChoice();

                var inputLines = _inputValidatorService.HandleUserChoice(choice);

                if (inputLines.Count == 0)
                {
                    Console.WriteLine("No match data entered.");
                    return;
                }

                _soccerMatchProcessorService.ProcessSoccerMatches(inputLines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {ErrorMessage}", ex.Message);
            }
        }

       
    }
}
