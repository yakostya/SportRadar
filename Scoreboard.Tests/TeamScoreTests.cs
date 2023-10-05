using FluentAssertions;
using Xunit;

namespace Scoreboard.Tests
{
    public class TeamScoreTests
    {
        [Theory]
        [InlineData(null)]
        public void Ctor_InvalidValueAsTeam_ShouldThrowException(Team team)
        {
            var underTest = () => { var _ = new TeamScore(team, 0); };
            underTest.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NonNullTeam_ShouldNotThrowAnyException()
        {
            var underTest = () => { var _ = new TeamScore(new Team("Mexico"), 0); };
            underTest.Should().NotThrow();
        }

        [Fact]
        public void Ctor_ProvideInvalidScore_ThrowsException()
        {
            var underTest = () => { var _ = new TeamScore(new Team("Mexico"), -1); };
            underTest.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void UpdateScore_NegativeValue_ThrowsException(int value)
        {
            var underTest = () => { new TeamScore(new Team("Mexico"), 0).UpdateScore(value); };
            underTest.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
