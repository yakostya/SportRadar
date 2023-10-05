namespace Scoreboard
{
    public class Match : IComparable<Match>
    {
        public TeamScore Home { get; }
        public TeamScore Away { get; }

        public DateTimeOffset? StartTime { get; private set; }
        public DateTimeOffset? FinishTime { get; private set; }

        public event EventHandler Finished = delegate { };

        public Match(Team home, Team away)
        {
            Home = new TeamScore { Team = home, Score = 0 };
            Away = new TeamScore { Team = away, Score = 0 };
        }

        public Match Start()
        {
            StartTime = DateTimeOffset.UtcNow;
            return this;
        }

        public Match UpdateScore(int home, int away)
        {
            Home.Score = home;
            Away.Score = away;

            return this;
        }

        public Match Finish()
        {
            FinishTime = DateTimeOffset.UtcNow;
            Finished.Invoke(this, EventArgs.Empty);

            return this;
        }

        public override string ToString()
            => $"{Home.Team.Name} {Home.Score} - {Away.Team.Name} {Away.Score}";

        public int CompareTo(Match? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            var totalScore = Home.Score + Away.Score;
            var otherTotalScore = other.Home.Score + other.Away.Score;

            if (totalScore == otherTotalScore)
            {
                return other.StartTime.GetValueOrDefault()
                    .CompareTo(StartTime.GetValueOrDefault());
            }

            return totalScore > otherTotalScore ? -1 : 1;
        }
    }
}
