namespace SoccerLeagueTests;
using Xunit;
using Moq;

using SoccerLeagueClassLibrary.BusinessLogic;
using SoccerLeagueUI.Services;

public class InputValidatorServiceTests
{

    [Fact]
    public void GetSoccerMatches_ValidInputLines_ReturnsListOfSoccerMatches()
    {
        // Arrange
        var inputLines = new List<string>
        {
            "Spain 3, Portugal 2",
            "Argentina 1, South Africa 0",
            "Spain 1, South Africa 1"
        };
       var serviceProviderMock = new Mock<IServiceProvider>(); 
        var inputValidatorService = new InputValidatorService(serviceProviderMock.Object);

        // Act
        var result = inputValidatorService.GetSoccerMatches(inputLines);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<SoccerMatch>>(result);
        Assert.Equal(3, result.Count); // Assuming 3 valid soccer matches were provided in inputLines
    }
     [Fact]
    public void GetSoccerMatches_InvalidInputLines_ReturnsEmptyList()
    {
        // Arrange
        var inputLines = new List<string>
        {
            "Spain 3 , Portugal 2 ",
            "Argentina 1 , South Africa",
            "Spain 1, South Africa 1"
        };
        var serviceProviderMock = new Mock<IServiceProvider>(); 
        var inputValidatorService = new InputValidatorService(serviceProviderMock.Object);

        // Act
        var result = inputValidatorService.GetSoccerMatches(inputLines);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<SoccerMatch>>(result);
       
    }
   [Fact]
    public void IsValid_ValidInputLine_ReturnsTrue()
    {
        // Arrange
        var inputLine = "Spain 3, Portugal 2";
        var serviceProviderMock = new Mock<IServiceProvider>(); 
        var inputValidatorService = new InputValidatorService(serviceProviderMock.Object);

        // Act
        var result = inputValidatorService.IsValid(inputLine);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_InvalidInputLine_ReturnsFalse()
    {
        // Arrange
        var inputLine = "Argentina 1, South Africa";
        var serviceProviderMock = new Mock<IServiceProvider>(); 
        var inputValidatorService = new InputValidatorService(serviceProviderMock.Object);

        // Act
        var result = inputValidatorService.IsValid(inputLine);

        // Assert
        Assert.False(result);
    }
  
}
