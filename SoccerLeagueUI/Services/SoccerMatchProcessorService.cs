using Microsoft.Extensions.DependencyInjection;
using SoccerLeagueClassLibrary.BusinessLogic;
using SoccerLeagueClassLibrary.Repository;
using SoccerLeagueUI.Output;


namespace SoccerLeagueUI.Services;
public class SoccerMatchProcessorService : ISoccerMatchProcessorService
{
   private readonly IServiceProvider _serviceProvider;

        public SoccerMatchProcessorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ProcessSoccerMatches(List<string> inputLines)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var inputValidatorService = scope.ServiceProvider.GetRequiredService<IInputValidatorService>();
                var soccerMatches = inputValidatorService.GetSoccerMatches(inputLines);

                var soccerLeague = scope.ServiceProvider.GetRequiredService<ISoccerLeague>();
                var teamScores = soccerLeague.CalculateTeamScore(soccerMatches);

                var soccerLeagueRepository = scope.ServiceProvider.GetRequiredService<ISoccerLeagueRepository>();
                soccerLeagueRepository.SaveTeamScores(teamScores);

                var teamScorePrinter = scope.ServiceProvider.GetRequiredService<ITeamScorePrinter>();
                teamScorePrinter.PrintTeamScores(teamScores);
            }
        }

}
