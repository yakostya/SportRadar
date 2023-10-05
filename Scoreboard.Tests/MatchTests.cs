using FluentAssertions;
using Xunit;

namespace Scoreboard.Tests
{
    public class MatchTests
    {
        [Fact]
        public void Finish_GenerateEvent()
        {
            var match = Matches.MexicoCanada();
            var monitored = match.Monitor();

            match.Finish();
            monitored.Should().Raise(nameof(match.Finished), $"Expecting {nameof(match.Finished)} should be raised.");
        }

        [Fact]
        public void Finish_ShouldSetFinishTime()
        {
            var match = Matches.MexicoCanada();
            match.FinishTime.Should().Be(null);
            match.Finish();

            match.FinishTime.Should().NotBe(null);
        }

        [Fact]
        public void Start_ShouldSetStartTime()
        {
            var match = Matches.MexicoCanada();
            match.StartTime.Should().Be(null);
            match.Start();

            match.StartTime.Should().NotBe(null);
        }

        [Fact]
        public void UpdateScore_ShouldSetProperScores()
        {
            var match = Matches.MexicoCanada().Start();

            var homeScore = 10;
            var awayScore = 2;
            match.UpdateScore(homeScore, awayScore);

            match.Home.Score.Should().Be(homeScore);
            match.Away.Score.Should().Be(awayScore);
        }

        [Fact]
        public void Ctor_MatchCreationWithZeroScore()
        {
            var match = Matches.MexicoCanada();

            match.Home.Score.Should().Be(0);
            match.Away.Score.Should().Be(0);
        }

        [Fact]
        public void CompareTo_TwoMatchesWithSameScore_CompareTimesMostRecentlyStartedFirst()
        {
            var uruguayItaly = Matches.UruguayItaly();
            var mexicoCanada = Matches.MexicoCanada();

            uruguayItaly.Start();
            mexicoCanada.Start();

            uruguayItaly.UpdateScore(10, 10);
            mexicoCanada.UpdateScore(10, 10);

            uruguayItaly.CompareTo(mexicoCanada).Should().BeGreaterThan(0);
        }

        [Fact(Skip = "Temporary ignored, need to add interface for DateTimeOffset.Now();")]
        public void CompareTo_TwoMatchesWithSameScoreAndStartTime_ShouldBeInSamePosition()
        {
            var uruguayItaly = Matches.UruguayItaly();
            var mexicoCanada = Matches.MexicoCanada();

            uruguayItaly.Start();
            mexicoCanada.Start();

            uruguayItaly.UpdateScore(10, 10);
            mexicoCanada.UpdateScore(10, 10);

            uruguayItaly.CompareTo(mexicoCanada).Should().Be(0);
        }

        [Fact]
        public void CompareTo_TwoMatchesWithDifferentScore_WithBiggerScoreFirst()
        {
            var uruguayItaly = Matches.UruguayItaly();
            var mexicoCanada = Matches.MexicoCanada();

            uruguayItaly.Start();
            mexicoCanada.Start();

            uruguayItaly.UpdateScore(0, 5);
            mexicoCanada.UpdateScore(1, 1);

            uruguayItaly.CompareTo(mexicoCanada).Should().BeLessThan(0);
        }
    }
}
