namespace Scoreboard
{
    public class Team
    {
        public string Name { get;}

        public Team(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }
}
