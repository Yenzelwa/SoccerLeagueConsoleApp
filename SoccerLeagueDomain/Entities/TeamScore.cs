using System.ComponentModel.DataAnnotations;

namespace SoccerLeagueDomain;
public class TeamScore
{
    [Required]
    public int Id { get; set; }
     [Required]
    public string Team { get; set; }
     [Required]
    public int Points { get; set; }
}