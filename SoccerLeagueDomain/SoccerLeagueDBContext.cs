using Microsoft.EntityFrameworkCore;

namespace SoccerLeagueDomain;

public class SoccerLeagueDBContext: DbContext
{
    public SoccerLeagueDBContext(DbContextOptions options) : base(options){}

    public DbSet<TeamScore> TeamScore {get; set;}
}
