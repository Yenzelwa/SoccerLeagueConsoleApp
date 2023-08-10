namespace SoccerLeagueClassLibrary.BusinessLogic;
public class SoccerMatch
{
    public string TeamA { get; set; }
    public int ScoreA { get; set; }

    public string TeamB { get; set; }
    public int ScoreB { get; set; }
    public SoccerMatch(string teamA, int scoreA, string teamB, int scoreB)
    {
        TeamA = teamA;
        ScoreA = scoreA;
        TeamB = teamB;
        ScoreB = scoreB;
    }

}
