namespace SoccerLeagueTestss;

using SoccerLeagueClassLibrary.BusinessLogic;
using SoccerLeagueClassLibrary.Models;
using Xunit;

public class SoccerLeagueTests
{

    [Fact]
    public void Calculate_Match_Points_Succesfully()
    {

        // Arrange 
        var match = new SoccerMatch("South Africa", 2, "Spain", 5);
        var expectedteamAPoints = 0;
        var expectedteamBPoints = 3;
        //Act
        var soccerLeague = new SoccerLeague();
        var result = soccerLeague.CalculatePoints(match);
        //Assert
        Assert.Equal(expectedteamAPoints, result.teamAPoints);
        Assert.Equal(expectedteamBPoints, result.teamBPoints);
    }
    [Fact]
    public void Calculate_Match_Points_WithNegativeScores()
    {
        // Arrange 
        var match = new SoccerMatch("South Africa", -2, "Spain", -5);
        var expectedteamAPoints = 0;
        var expectedteamBPoints = 0;

        //Act
        var soccerLeague = new SoccerLeague();
        var result = soccerLeague.CalculatePoints(match);

        //Assert
        Assert.Equal(expectedteamAPoints, result.teamAPoints);
        Assert.Equal(expectedteamBPoints, result.teamBPoints);
    }


    [Fact]
    public void Calculate_Team_Score_Successfully()
    {
        // Arrange
        var soccerMatches = new List<SoccerMatch>
        {
            new SoccerMatch("South Africa", 2, "Spain", 5),
            new SoccerMatch("Argentina", 2, "Portugal", 5),
            new SoccerMatch("Spain", 4, "India", 0)
        };

        var soccerLeague = new SoccerLeague();

        var expectedSoccerLeagueResults = new List<SoccerLeagueResult>
        {
            new SoccerLeagueResult { Team = "South Africa", Points = 0 },
            new SoccerLeagueResult { Team = "Spain", Points = 3 },
            new SoccerLeagueResult { Team = "Argentina", Points = 0 },
            new SoccerLeagueResult { Team = "Portugal", Points = 1 },
            new SoccerLeagueResult { Team = "India", Points = 0 }
        };

        // Act
        var teamRankings = soccerLeague.CalculateTeamScore(soccerMatches);

        // Assert
        Assert.Equal(expectedSoccerLeagueResults.Count, teamRankings.Count);

    }
}
