using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoccerLeagueClassLibrary.Models;
using SoccerLeagueDomain;
using SoccerLeagueClassLibrary.Repository;
using Xunit;

namespace SoccerLeagueRankingsTests;

public class SoccerLeagueRepositoryTests
{
    [Fact]
    public void SaveTeamScores_ValidData_ShouldSaveSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<SoccerLeagueDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using (var context = new SoccerLeagueDBContext(options))
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<SoccerLeagueRepository>();

            var repository = new SoccerLeagueRepository(logger, context);
            var teamScores = new List<SoccerLeagueResult>
                {
                    new SoccerLeagueResult { Team = "TeamA", Points = 3 },
                    new SoccerLeagueResult { Team = "TeamB", Points = 1 }
                };

            // Act
            repository.SaveTeamScores(teamScores);

            // Assert
            var savedTeamA = context.TeamScore.SingleOrDefault(t => t.Team == "TeamA");
            var savedTeamB = context.TeamScore.SingleOrDefault(t => t.Team == "TeamB");

            Assert.NotNull(savedTeamA);
            Assert.NotNull(savedTeamB);
            Assert.Equal(3, savedTeamA.Points);
            Assert.Equal(1, savedTeamB.Points);
        }
    }
    

}

