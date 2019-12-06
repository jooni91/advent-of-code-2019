using MyAoC2019.Utilities;
using Xunit;

namespace MyAoC2019.Tests.Utilities
{
    public class PasswordCrackerTests
    {
        [Fact]
        public void GetPossibleCombinationsPartOne_ShouldReturn_ExpectedResult()
        {
            // Arrange
            var pwRange = new string[] { "111123", "135679" };
            var pwCracker = new PasswordCracker();

            // Act
            var result = pwCracker.GetPossibleCombinationsPartOne(pwRange);

            // Assert
            Assert.Equal(930, result);
        }

        [Fact]
        public void GetPossibleCombinationsPartTwo_ShouldReturn_ExpectedResult()
        {
            // Arrange
            var pwRange = new string[] { "111123", "135679" };
            var pwCracker = new PasswordCracker();

            // Act
            var result = pwCracker.GetPossibleCombinationsPartTwo(pwRange);

            // Assert
            Assert.Equal(721, result);
        }
    }
}
