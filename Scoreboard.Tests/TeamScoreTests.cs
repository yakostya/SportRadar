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
    }
}
