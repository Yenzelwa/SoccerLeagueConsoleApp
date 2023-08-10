using SoccerLeagueClassLibrary.Models;

namespace SoccerLeagueUI.Output
{
    public class TeamScorePrinter : ITeamScorePrinter
    {
        public void PrintTeamScores(List<SoccerLeagueResult> teamScores)
        {
            var sortedScores = teamScores
                .OrderByDescending(team => team.Points)
                .ThenBy(team => team.Team)
                .ToList();

            Console.WriteLine("Results");
            int rank = 1;
            foreach (var team in sortedScores)
            {
                Console.WriteLine($"{rank}. {team.Team}, {team.Points} {(team.Points == 1 ? "pt" : "pts")}");
                rank++;
            }
        }

    }
}
