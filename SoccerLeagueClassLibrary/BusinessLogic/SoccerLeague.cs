
using SoccerLeagueClassLibrary.Models;
namespace SoccerLeagueClassLibrary.BusinessLogic;

public interface ISoccerLeague
{
    List<SoccerLeagueResult> CalculateTeamScore(List<SoccerMatch> soccerMatches);
}
public class SoccerLeague : ISoccerLeague
{
   
    public List<SoccerLeagueResult> SoccerLeagueResults { get; } = new List<SoccerLeagueResult>();

    public List<SoccerLeagueResult> CalculateTeamScore(List<SoccerMatch> soccerMatches)
    {
        foreach (var match in soccerMatches)
        {
            var (teamAPoints, teamBPoints) = CalculatePoints(match);

            UpdateTeamRanking(match.TeamA, teamAPoints);
            UpdateTeamRanking(match.TeamB, teamBPoints);
        }
        return SoccerLeagueResults;
    }

    public (int teamAPoints, int teamBPoints) CalculatePoints(SoccerMatch match)
    {
        int teamAPoints = 0;
        int teamBPoints = 0;

        if (match.ScoreA >= 0 && match.ScoreB >= 0)
        {
            switch (match.ScoreA.CompareTo(match.ScoreB))
            {
                case 1: // Team A wins
                    teamAPoints = 3;
                    break;

                case 0: // Draw
                    teamAPoints = 1;
                    teamBPoints = 1;
                    break;

                case -1: // Team B wins
                    teamBPoints = 3;
                    break;
            }
        }
        else
        {
            teamAPoints = 0;
            teamBPoints = 0;
        }

        return (teamAPoints, teamBPoints);
    }
    private void UpdateTeamRanking(string teamName, int points)
    {
        var existingTeam = SoccerLeagueResults.FirstOrDefault(x => x.Team == teamName);
        if (existingTeam != null)
        {
            existingTeam.Points += points;
        }
        else
        {
            SoccerLeagueResults.Add(new SoccerLeagueResult { Team = teamName, Points = points });
        }
    }

}


