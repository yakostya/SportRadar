using FluentAssertions;
using Xunit;

namespace Scoreboard.Tests
{
    public class MatchTests
    {

        [Fact]
        public void Start_InvalidCondition_ThrowsError()
        {
            var match1 = Matches.MexicoCanada();
            match1.Start();
            match1.Finish();
            var startOnAlreadyFinish = () => match1.Start();

            startOnAlreadyFinish.Should().Throw<InvalidOperationException>();

            var match2 = Matches.MexicoCanada();
            match2.Start();
            var startOnStartedMatch = () => match2.Start();

            startOnStartedMatch.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Finish_InvalidCondition_ThrowsError()
        {
            var match1 = Matches.MexicoCanada();
            var finishOnNotStartedMatch = () => match1.Finish();

            finishOnNotStartedMatch.Should().Throw<InvalidOperationException>();

            var match2 = Matches.MexicoCanada();
            match2.Start();
            match2.Finish();
            var finishOnAlreadyFinished = () => match1.Finish();
            finishOnAlreadyFinished.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Ctor_NullValues_ThrowsException()
        {
            var underTest = () => { var _ = new Match(null, new Team("Mexico")); };
            underTest.Should().Throw<ArgumentNullException>();

            underTest = () => { var _ = new Match(new Team("Mexico"), null); };
            underTest.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Finish_GenerateEvent()
        {
            var match = Matches.MexicoCanada()
                .Start();
            var monitored = match.Monitor();

            match.Finish();
            monitored.Should().Raise(nameof(match.Finished), $"Expecting {nameof(match.Finished)} should be raised.");
        }

        [Fact]
        public void Finish_ShouldSetFinishTime()
        {
            var match = Matches.MexicoCanada()
                .Start();
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
