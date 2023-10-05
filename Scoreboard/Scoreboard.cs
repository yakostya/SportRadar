namespace Scoreboard
{
    public class Scoreboard : IDisposable
    {
        private readonly List<Match> _matches = new();

        public Match Start(Team home, Team away)
        {
            var match = new Match(home, away)
                .Start();

            match.Finished += MatchOnFinished;

            _matches.Add(match);
            return match;
        }

        public IList<Match> GetSummary()
        {
            var matches = _matches.ToList();
            matches.Sort();
            return matches;
        }

        public void Dispose()
        {
            foreach (var match in _matches)
            {
                match.Finished -= MatchOnFinished;
            }

            _matches.Clear();
        }

        private void MatchOnFinished(object? sender, EventArgs e)
        {
            if (sender is Match match)
            {
                _matches.Remove(match);
            }
        }
    }
}
