using FluentAssertions;
using Xunit;

namespace Scoreboard.Tests
{
    public class ScoreBoardTests
    {
        [Fact]
        public void GetSummary_StartSomeMatches_ReturnsOrderedByTotalScore()
        {
            using var scoreboard = new Scoreboard();
            var mexicoCanada = scoreboard.Start(new Team("Mexico"), new Team("Canada"))
                                         .UpdateScore(0, 5);

            var uruguayItaly = scoreboard.Start(new Team("Uruguay"), new Team("Italy"))
                .UpdateScore(6, 6);

            var spainBrazil = scoreboard.Start(new Team("Spain"), new Team("Brazil"))
                .UpdateScore(10, 2);

            var argentinaAustralia = scoreboard.Start(new Team("Argentina"), new Team("Australia"))
                .UpdateScore(3, 1);

            var germanyFrance = scoreboard.Start(new Team("Germany"), new Team("France"))
                .UpdateScore(2, 2);

            var summary = scoreboard.GetSummary();
            summary.Should().BeEquivalentTo(new[]
            {
                uruguayItaly,
                spainBrazil,
                mexicoCanada,
                argentinaAustralia,
                germanyFrance,
                
            }, options => options.WithStrictOrdering(),
                "Expecting matches to be returned in described order");
        }

        [Fact]
        public void GetSummary_StartAndFinishSomeMatches_ReturnsOrderedMatchesWithoutFinished()
        {
            using var scoreboard = new Scoreboard();
            var mexicoCanada = scoreboard.Start(new Team("Mexico"), new Team("Canada"))
                .UpdateScore(0, 5);

            var uruguayItaly = scoreboard.Start(new Team("Uruguay"), new Team("Italy"))
                .UpdateScore(6, 6)
                .Finish();

            var spainBrazil = scoreboard.Start(new Team("Spain"), new Team("Brazil"))
                .UpdateScore(10, 2);

            var argentinaAustralia = scoreboard.Start(new Team("Argentina"), new Team("Australia"))
                .UpdateScore(3, 1).Finish();

            var germanyFrance = scoreboard.Start(new Team("Germany"), new Team("France"))
                .UpdateScore(2, 2);

            var summary = scoreboard.GetSummary();
            summary.Should().BeEquivalentTo(new[]
            {
                spainBrazil,
                mexicoCanada,
                germanyFrance,

            },
                options => options.WithStrictOrdering(),
                "Expecting matches to be returned in described order");
        }
    }
}
