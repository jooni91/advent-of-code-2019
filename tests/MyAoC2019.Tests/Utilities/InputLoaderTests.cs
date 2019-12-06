using System.Linq;
using MyAoC2019.Utilities;
using Xunit;

namespace MyAoC2019.Tests.Utilities
{
    public class InputLoaderTests
    {
        [Fact]
        public void LoadInputsFromFileAsString_ShouldReturn_ExpectedResult()
        {
            // Arrange
            var expectedResult = "3,53,24,0,847,242,89\r\n3-53-24-0-847-242-89";

            // Act
            var result = InputLoader.LoadInputsFromFileAsString("InputFiles/InputLoaderTestFile.txt");

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void SplitInputs_DefaultSeparator_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResult = new string[] { "3", "53", "24", "0", "847", "242", "89\r\n3-53-24-0-847-242-89" };

            // Act
            var result = InputLoader.LoadInputsFromFileAsString("InputFiles/InputLoaderTestFile.txt").SplitInputs();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void SplitInputs_DefaultSeparator_ReturnsExpectedResultWithSeperator()
        {
            // Arrange
            var expectedResult = new string[] { "3", ",", "53", ",", "24", ",", "0", ",", "847", ",", "242", ",", "89" };

            // Act
            var result = InputLoader.LoadInputsFromFileAsString("InputFiles/InputLoaderTestFile.txt")
                .ReadInputLines().SplitInputs(true).ToArray();

            // Assert
            Assert.Equal(expectedResult, result[0]);
        }

        [Fact]
        public void SplitInputs_CustomSeperator_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResult = new string[] { "3", "53", "24", "0", "847", "242", "89\r\n3","53","24","0","847","242","89" };

            // Act
            var result = InputLoader.LoadInputsFromFileAsString("InputFiles/InputLoaderTestFile.txt").SplitInputs(',', '-');

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void SplitInputs_CustomSeperator_ReturnsExpectedResultWithSeperator()
        {
            // Arrange
            var expectedResult = new string[] { "3", "-", "53", "-", "24", "-", "0", "-", "847", "-", "242", "-", "89" };

            // Act
            var result = InputLoader.LoadInputsFromFileAsString("InputFiles/InputLoaderTestFile.txt")
                .ReadInputLines().SplitInputs(true, '-').ToArray();

            // Assert
            Assert.Equal(expectedResult, result[1]);
        }

        [Fact]
        public void ReadInputLines_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResult = new string[] { "3,53,24,0,847,242,89", "3-53-24-0-847-242-89" };

            // Act
            var result = InputLoader.LoadInputsFromFileAsString("InputFiles/InputLoaderTestFile.txt").ReadInputLines().ToArray();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ConvertInputsToIntegers_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResult = new int[] { 3, 53, 24, 0, 847, 242, 89, 3, 53, 24, 0, 847, 242, 89 };
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/InputLoaderTestFile.txt").ReadInputLines().ToArray();
            var splittedInput = input[0].SplitInputs(',', '-').Concat(input[1].SplitInputs(',', '-')).ToArray();

            // Act
            var result = splittedInput.ConvertInputsToIntegers().ToArray();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
