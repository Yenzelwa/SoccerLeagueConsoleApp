using SoccerLeagueClassLibrary.BusinessLogic;

namespace SoccerLeagueUI.Services;
public interface IInputValidatorService
{
    List<SoccerMatch> GetSoccerMatches(List<string> inputLines);
    string GetInputChoice();
    List<string> HandleUserChoice(string choice);
    public  bool IsValid(string inputLine);
}