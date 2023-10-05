using FluentAssertions;
using Xunit;

namespace Scoreboard.Tests
{
    public class ScoreBoardTests
    {
        [Fact]
        public void GetSummary_StartSomeMatches_ReturnsOrderedByTotalScore()
        {
            var scoreboard = new Scoreboard();
            var match1 = scoreboard.Start(new Team("Mexico"), new Team("Canada"))
                                         .UpdateScore(0, 5);

            var match2 = scoreboard.Start(new Team("Spain"), new Team("Brazil"))
                .UpdateScore(10, 2);

            var match3 = scoreboard.Start(new Team("Germany"), new Team("France"))
                .UpdateScore(2, 2);

            var match4 = scoreboard.Start(new Team("Uruguay"), new Team("Italy"))
                .UpdateScore(6, 6);

            var match5 = scoreboard.Start(new Team("Argentina"), new Team("Australia"))
                .UpdateScore(3, 1);

            scoreboard.GetSummary().Should().BeEquivalentTo(new[]
            {
                match4,
                match2,
                match1,
                match5,
                match3
            }, "Expecting matches to be returned in described order");
        }
    }
}
