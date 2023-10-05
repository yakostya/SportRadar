namespace Scoreboard
{
    public class TeamScore
    {
        public int Score { get; set; }
        public Team Team { get; set; }

        public TeamScore(Team team, int score)
        {
            Team = team ?? throw new ArgumentNullException(nameof(team));
        }
    }
}
