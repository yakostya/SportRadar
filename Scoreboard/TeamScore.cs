namespace Scoreboard
{
    public class TeamScore
    {
        public int Score { get; private set; }
        public Team Team { get; }

        public TeamScore(Team team, int score)
        {
            Team = team ?? throw new ArgumentNullException(nameof(team));
            UpdateScore(score);
        }

        public void UpdateScore(int score)
        {
            if (score < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(score));
            }

            Score = score;
        }
    }
}
