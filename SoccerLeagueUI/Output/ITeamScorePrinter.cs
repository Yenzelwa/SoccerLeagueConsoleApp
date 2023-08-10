using SoccerLeagueClassLibrary.Models;

namespace SoccerLeagueUI.Output;
public interface ITeamScorePrinter
{
    void PrintTeamScores(List<SoccerLeagueResult> teamScores);
}
