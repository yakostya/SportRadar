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
    }
}
