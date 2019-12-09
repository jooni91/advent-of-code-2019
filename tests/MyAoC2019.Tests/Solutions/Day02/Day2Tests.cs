using Xunit;

namespace MyAoC2019.Tests.Solutions.Day02
{
    public class Day2Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day02.Day2
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("2894520", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day02.Day2
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("9342", result);
        }
    }
}
