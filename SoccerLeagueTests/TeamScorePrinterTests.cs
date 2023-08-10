using System;
using System.Collections.Generic;
using Moq;
using SoccerLeagueClassLibrary.Models;
using SoccerLeagueTests;
using SoccerLeagueUI.Output;
using Xunit;

public class TeamScorePrinterTests
{
    [Fact]
    public void PrintTeamScores_ValidData_Success()
    {
        // Arrange
        var teamScores = new List<SoccerLeagueResult>
        {
            new SoccerLeagueResult { Team = "Spain", Points = 15 },
            new SoccerLeagueResult { Team = "Portugal", Points = 12 },
            new SoccerLeagueResult { Team = "Argentina", Points = 8 },
        };

        var expectedOutput =
            "Results" + Environment.NewLine +
            "1. Spain, 15 pts" + Environment.NewLine +
            "2. Portugal, 12 pts" + Environment.NewLine +
            "3. Argentina, 8 pts" + Environment.NewLine;

        // Act
        var printer = new TeamScorePrinter();
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            printer.PrintTeamScores(teamScores);
            var actualOutput = sw.ToString();

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

    }

    [Fact]
    public void PrintTeamScores_EmptyList_NoOutput()
    {
        // Arrange
        var teamScores = new List<SoccerLeagueResult>(); 

        var expectedOutput = "Results" + Environment.NewLine; 

        // Act
        var printer = new TeamScorePrinter();

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            printer.PrintTeamScores(teamScores);
            var actualOutput = sw.ToString();

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}


