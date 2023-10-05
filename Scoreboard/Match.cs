namespace Scoreboard
{
    public class Match
    {
        public TeamScore Home { get; set; }
        public TeamScore Away { get; set; }

        public Match UpdateScore(int home, int away)
        {
            throw new NotImplementedException();
        }
    }
}
