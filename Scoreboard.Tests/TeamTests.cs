using FluentAssertions;
using Xunit;

namespace Scoreboard.Tests
{
    public class TeamTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Ctor_InvalidValueInParameter_ShouldThrowException(string name)
        {
            var underTest = () => { var _ = new Team(name); };
            underTest.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("Mexico")]
        [InlineData("Canada")]
        public void Ctor_NonEmptyValues_ShouldCreateTeamWithProvidedName(string name)
        {
            var team = new Team(name);
            team.Name.Should().BeEquivalentTo(name);
        }
    }
}
