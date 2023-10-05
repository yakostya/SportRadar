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
            if (home == null)
            {
                throw new ArgumentNullException(nameof(home));
            }

            if (away == null)
            {
                throw new ArgumentNullException(nameof(away));
            }

            Home = new TeamScore(home, 0);
            Away = new TeamScore(away, 0);
        }

        public Match Start()
        {
            if (StartTime.HasValue)
            {
                throw new InvalidOperationException($"Match is already started at {StartTime}. Can't start it again.");
            }

            if (FinishTime.HasValue)
            {
                throw new InvalidOperationException($"Match is already finished at {FinishTime}. Can't start it again.");
            }

            StartTime = DateTimeOffset.UtcNow;
            return this;
        }

        public Match UpdateScore(int home, int away)
        {
            if (!StartTime.HasValue)
            {
                throw new InvalidOperationException($"Match is not started yet. Please call {nameof(Start)} first.");
            }

            Home.UpdateScore(home);
            Away.UpdateScore(away);

            return this;
        }

        public Match Finish()
        {
            if (!StartTime.HasValue)
            {
                throw new InvalidOperationException($"Match is not started yet. Please call {nameof(Start)} first.");
            }

            if (FinishTime.HasValue)
            {
                throw new InvalidOperationException($"Match is already finished at {FinishTime}. Can't finish it again.");
            }

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
