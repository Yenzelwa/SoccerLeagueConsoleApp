
using Microsoft.Extensions.Logging;
using SoccerLeagueClassLibrary.Models;
using SoccerLeagueDomain;

namespace SoccerLeagueRankingsClassLibrary.Repository;

public interface ISoccerLeagueRepository
{
    public void SaveTeamScores(List<SoccerLeagueResult> teamScores);
}
public class SoccerLeagueRepository : ISoccerLeagueRepository
{
    private readonly ILogger<SoccerLeagueRepository> _logger;
    private readonly SoccerLeagueDBContext _dbContext;

    public SoccerLeagueRepository(ILogger<SoccerLeagueRepository> logger, SoccerLeagueDBContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public void SaveTeamScores(List<SoccerLeagueResult> teamScores)
    {
        try
        {
            if (teamScores == null || teamScores.Count == 0)
            {
                _logger.LogError("teamScores is null");
                throw new ArgumentNullException(nameof(teamScores));
            }

            foreach (var team in teamScores)
            {
                var teamScore = new TeamScore
                {
                    Team = team.Team,
                    Points = team.Points
                };

                _dbContext.TeamScore.Add(teamScore);
            }

            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while saving team scores.");
        }

    }
}

