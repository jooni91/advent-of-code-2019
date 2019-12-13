using Xunit;

namespace MyAoC2019.Tests.Solutions.Day13
{
    public class Day13Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day13.Day13
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("309", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day13.Day13
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("15410", result);
        }
    }
}
