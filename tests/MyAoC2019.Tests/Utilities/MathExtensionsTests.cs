using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MyAoC2019.Utilities;
using Xunit;

namespace MyAoC2019.Tests.Utilities
{
    public class MathExtensionsTests
    {
        [Theory]
        [ClassData(typeof(GeneratePermutationsTestData))]
        public void GeneratePermutations_ShouldReturn_ExpectedResults<T>(T[] objects, T[][] expectedResults)
        {
            // Arrange


            // Act
            var result = MathExtensions.GeneratePermutations(objects, null).ToArray();

            // Assert
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public void GeneratePermutations_ShouldReturn_ExpectedCount()
        {
            // Arrange
            var objects = new int[] { 0, 1, 2, 3, 4, 5 };

            // Act
            var result = MathExtensions.GeneratePermutations(objects, null).Count;

            // Assert
            Assert.Equal(720, result);
        }

        [Fact]
        public void GeneratePermutations_WhenLimiting_ShouldReturn_ExpectedCount()
        {
            // Arrange
            var objects = new int[] { 0, 1, 2, 3, 4, 5 };

            // Act
            var result = MathExtensions.GeneratePermutations(objects, 2).Count;

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GeneratePermutations_ShouldThrow_ArgumentOutOfRangeException()
        {
            // Arrange
            var objects = new int[257];

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => MathExtensions.GeneratePermutations(objects, null));
        }

        [Theory]
        [ClassData(typeof(GenerateIsPrimeTestData))]
        public void IsPrime_ShouldReturn_ExpectedValue(int number, bool expected)
        {
            // Arrange


            // Act
            var result = MathExtensions.IsPrime(number);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(GenerateManhattenDistanceTestData))]
        public void ManhattanDistance_ShouldReturn_ExpectedValue(Point pointA, Point pointB, int expected)
        {
            // Arrange


            // Act
            var result = MathExtensions.ManhattanDistance(pointA, pointB);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(30, 5, 5)]
        [InlineData(100, 30, 10)]
        [InlineData(12, 9, 3)]
        public void GreatestCommonDivider_ShouldReturn_ExpectedValue(int a, int b, int expected)
        {
            // Arrange


            // Act
            var result = MathExtensions.GreatestCommonDivider(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(30, 5, 30)]
        [InlineData(100, 30, 300)]
        [InlineData(12, 9, 36)]
        public void LowestCommonMultiple_ShouldReturn_ExpectedValue(int a, int b, int expected)
        {
            // Arrange


            // Act
            var result = MathExtensions.LowestCommonMultiple(a, b);

            // Assert
            Assert.Equal(expected, result);
        }
    }

    public class GeneratePermutationsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new int[] { 1, 0 }, new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 } } };
            yield return new object[] 
            { 
                new int[] { 0, 1, 2 }, new int[][] 
                { 
                    new int[] { 2, 0, 1 }, new int[] { 2, 1, 0 }, new int[] { 1, 2, 0 }, 
                    new int[] { 0, 2, 1 }, new int[] { 1, 0, 2 }, new int[] { 0, 1, 2 }
                } 
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class GenerateIsPrimeTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 0, false };
            yield return new object[] { 1, false };
            yield return new object[] { 2, true };
            yield return new object[] { 3, true };
            yield return new object[] { 4, false };
            yield return new object[] { 11, true };
            yield return new object[] { 21, false };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class GenerateManhattenDistanceTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(0, 0), new Point(4, 4), 8 };
            yield return new object[] { new Point(2, 1), new Point(3, 6), 6 };
            yield return new object[] { new Point(-5, 1), new Point(3, -9), 18 };
            yield return new object[] { new Point(-246, 764), new Point(67, -242), 1319 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
